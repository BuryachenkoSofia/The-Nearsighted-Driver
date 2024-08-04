using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public GameObject audioPrefab;

    public void PlaySound()
    {
        if (audioPrefab != null)
        {
            GameObject audioInstance = Instantiate(audioPrefab);
            AudioSource audioSource = audioInstance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
                Destroy(audioInstance, audioSource.clip.length);
            }
        }
    }
}
