using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicBG : MonoBehaviour
{
    public GameObject BGMusic;
    private AudioSource audioSource;
    private GameObject[] audioPrefabs;
    private void Awake()
    {
        audioPrefabs = GameObject.FindGameObjectsWithTag("Sound");
        if (audioPrefabs.Length == 0)
        {
            BGMusic = Instantiate(BGMusic);
            BGMusic.name = "BGMusic";
            DontDestroyOnLoad(BGMusic.gameObject);
        }
        else
        {
            BGMusic = GameObject.Find("BGMusic");
        }
    }

    void Start()
    {
        audioSource = BGMusic.GetComponent<AudioSource>();
    }
    private void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("music");
    }

}