using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float speed;
    private float coinsAdd = 5f;
    public AudioClip coinSound;
    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * 0.01f);
        coinsAdd = PlayerPrefs.GetFloat("gem");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayCoinSound();
            other.GetComponent<Player>().CoinAdd(coinsAdd);
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
