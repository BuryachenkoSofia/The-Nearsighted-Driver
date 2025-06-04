using UnityEngine;

public class PlayMusicBG : MonoBehaviour
{
    private AudioSource audioSource;
    private GameObject[] audioPrefabs;
    [SerializeField] private GameObject BGMusic;

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

    private void Start()
    {
        audioSource = BGMusic.GetComponent<AudioSource>();
    }

    private void Update()
    {
        audioSource.volume = PlayerPrefs.GetFloat("music");
    }
}