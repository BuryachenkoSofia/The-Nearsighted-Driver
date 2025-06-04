using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private AudioClip enemySound;
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
            PlayEnemySound();
            Instantiate(particles, new Vector3(transform.position.x, transform.position.y, -5), Quaternion.identity);
            if (damage == 1) other.GetComponent<Player>().let10++;
            if (damage == 2) other.GetComponent<Player>().truck10++;
            if (damage == 3) other.GetComponent<Player>().police5++;
            if (!other.GetComponent<Player>().shield)
            {
                other.GetComponent<Player>().health -= damage;
                other.GetComponent<Player>().healthSwitch(other.GetComponent<Player>().health);
            }
            StartCoroutine(DestroyAfterSound());
        }
        if (other.CompareTag("DestroyEnemy"))
        {
            Destroy(gameObject);
        }
    }

    private void PlayEnemySound()
    {
        if (audioSource != null && enemySound != null)
        {
            audioSource.PlayOneShot(enemySound);
        }
    }
    
    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(enemySound.length);
        Destroy(gameObject);
    }
}