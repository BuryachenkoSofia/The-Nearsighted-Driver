using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public int health = 1;
    public float coins = 0;
    public GameObject heart0, heart1, heart2, heart3, heart4;
    public PostProcessVolume ppVolume;
    public GameObject panelDead;
    private Vector2 targetPos;
    public DistanceCounter distanceCounter;
    public TMP_Text coinsTMP;
    private float speed = 20;
    private float[] lanes = { -6f, -3f, 0f, 3f, 6f };
    private int currentLaneIndex = 2;
    public List<Sprite> sprites = new List<Sprite>(8);
    public Image glassesBarFill;
    private float glassesTimeLeft = 0f;
    private float glassesTimeMax = 0f;
    public bool glasses = false;

    public AudioClip spawnSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        gameObject.SetActive(true);

        Time.timeScale = 1f;
        targetPos = new Vector2(lanes[currentLaneIndex], transform.position.y);
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();


        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);
        coins = PlayerPrefs.GetFloat("coins");
        health = FindObjectOfType<CarShop>().cars[PlayerPrefs.GetInt("equipped")].hp_lvl + 1;
        healthSwitch(health);
        coinsTMP.text = "Coins: " + coins;
        glasses = false;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[PlayerPrefs.GetInt("equipped")];
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, targetPos) < 0.01f)
        {
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                MoveToNextLane();
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                MoveToPreviousLane();
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (health <= 0)
        {
            Dead();
        }
        ppVolume.enabled = !glasses;
        if (glasses)
        {
            glassesTimeLeft -= Time.deltaTime;

            if (glassesTimeLeft <= 0f)
            {
                glassesTimeLeft = 0f;
                glasses = false;
            }

            glassesBarFill.fillAmount = glassesTimeLeft / glassesTimeMax;
        }
    }

    private void MoveToNextLane()
    {
        if (currentLaneIndex < lanes.Length - 1)
        {
            currentLaneIndex++;
            targetPos = new Vector3(lanes[currentLaneIndex], transform.position.y, -1.0f);
        }
    }

    private void MoveToPreviousLane()
    {
        if (currentLaneIndex > 0)
        {
            currentLaneIndex--;
            targetPos = new Vector3(lanes[currentLaneIndex], transform.position.y, -1.0f);
        }
    }


    public void Dead()
    {
        Time.timeScale = 0f;
        CoinAdd(distanceCounter.distance * 10f);
        panelDead.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Continue()
    {
        if (coins >= 10)
        {
            coins -= 10;
            PlayerPrefs.SetFloat("coins", coins);
            coinsTMP.text = "Coins: " + coins;
            health = 3;
            healthSwitch(health);
            panelDead.SetActive(false);
            gameObject.SetActive(true);
            PlaySpawnSound();
            Time.timeScale = 1f;
        }
    }

    public void healthSwitch(int health)
    {
        if (health < 0) health = 0;
        switch (health)
        {
            case 0:
                heart0.SetActive(false);
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                heart4.SetActive(false);
                break;
            case 1:
                heart0.SetActive(true);
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                heart4.SetActive(false);
                break;
            case 2:
                heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
                heart4.SetActive(false);
                break;
            case 3:
                heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
                heart4.SetActive(false);
                break;
            case 4:
                heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                heart4.SetActive(false);
                break;
            case 5:
                heart0.SetActive(true);
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
                heart4.SetActive(true);
                break;
        }
    }

    public void CoinAdd(float c)
    {
        coins += c;
        PlayerPrefs.SetFloat("coins", coins);
        coinsTMP.text = "Coins: " + coins;
    }

    public void ActivateGlasses()
    {
        glasses = true;
        glassesTimeMax = (FindObjectOfType<CarShop>().cars[PlayerPrefs.GetInt("equipped")].glasses_lvl +1)*5f;
        glassesTimeLeft = glassesTimeMax;
    }

    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}