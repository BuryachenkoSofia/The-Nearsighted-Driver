using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private float speed;
    [SerializeField] private Slider sliderSpeed, sliderMusic;
    [SerializeField] private TMP_Text textSpeed, textMusic;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("speed"))
        {
            PlayerPrefs.SetFloat("speed", 17f);
        }
        sliderSpeed.value = PlayerPrefs.GetFloat("speed");

        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetFloat("music", 1f);
        }
        sliderMusic.value = PlayerPrefs.GetFloat("music");
    }

    private void Update()
    {
        textSpeed.text = "Lane Change Speed: " + Mathf.Round(sliderSpeed.value * 10f) / 10f;
        PlayerPrefs.SetFloat("speed", sliderSpeed.value);

        textMusic.text = "Music Value: " + Mathf.Round(sliderMusic.value * 100f);
        PlayerPrefs.SetFloat("music", sliderMusic.value);
    }
}