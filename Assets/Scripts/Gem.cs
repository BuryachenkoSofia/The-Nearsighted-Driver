using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public float speed;
    public AudioClip coinSound;
    private AudioSource audioSource;
    public GameObject particles;

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
            other.GetComponent<Player>().gems5++;
            PlayCoinSound();
            Instantiate(particles, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
            other.GetComponent<Player>().CoinAdd((FindObjectOfType<CarShop>().cars[PlayerPrefs.GetInt("equipped")].gem_lvl + 1) * 5f);
            StartCoroutine(DestroyAfterSound());
        }
        if (other.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
        }
    }
    private void PlayCoinSound()
    {
        if (audioSource != null && coinSound != null)
        {
            audioSource.PlayOneShot(coinSound);
        }
    }
    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(coinSound.length);
        Destroy(gameObject);
    }
}
