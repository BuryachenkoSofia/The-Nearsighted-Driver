using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Coin : MonoBehaviour
{
    public float speed;
    public AudioClip coinSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * 0.01f);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCoinSound();
            other.GetComponent<Player>().CoinAdd(1f+FindObjectOfType<CarShop>().cars[PlayerPrefs.GetInt("equipped")].coin_lvl*0.25f);
            StartCoroutine(DestroyAfterSound());
        }
        if(other.CompareTag("DestroyEnemy")){
            Destroy(gameObject);
        }
    }

    private void PlayCoinSound() {
        if (audioSource != null && coinSound != null) {
            audioSource.PlayOneShot(coinSound);
        }
    }
    private IEnumerator DestroyAfterSound() {
        yield return new WaitForSeconds(coinSound.length);
        Destroy(gameObject);
    }
}