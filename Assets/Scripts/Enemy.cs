using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy : MonoBehaviour
{
    public int damage;
    public float speed;
    public AudioClip enemySound;
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
            if (damage == 1) other.GetComponent<Player>().let10++;
            if (damage == 2) other.GetComponent<Player>().truck10++;
            if (damage == 3) other.GetComponent<Player>().police5++;
            PlayEnemySound();
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