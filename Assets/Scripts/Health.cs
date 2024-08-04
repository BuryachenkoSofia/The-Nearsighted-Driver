using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float speed;
    public AudioClip coinSound;
    private AudioSource audioSource;
    private void Awake() {
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
            if(other.GetComponent<Player>().health<5){
                other.GetComponent<Player>().health+=1; 
                other.GetComponent<Player>().mySwitch(other.GetComponent<Player>().health);
            }
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
