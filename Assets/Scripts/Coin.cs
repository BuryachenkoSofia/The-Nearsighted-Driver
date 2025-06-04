using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private GameObject particles;

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
            this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
            other.GetComponent<Player>().coins10++;
            PlayCoinSound();
            other.GetComponent<Player>().CoinAdd(1f + FindAnyObjectByType<CarShop>().cars[PlayerPrefs.GetInt("equipped")].coin_lvl * 0.25f);
            Instantiate(particles, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
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