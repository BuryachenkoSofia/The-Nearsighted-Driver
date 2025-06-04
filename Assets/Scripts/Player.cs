using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    private float speed = 20f;
    private float[] lanes = { -6f, -3f, 0f, 3f, 6f };
    private int currentLaneIndex = 2;
    private Vector2 targetPos;
    private float glassesTimeLeft = 0f, glassesTimeMax = 0f;
    private float shieldTimeLeft = 0f, shieldTimeMax = 0f;
    private AudioSource audioSource;
    private PostProcessVolume ppVolume;

    [HideInInspector] public int health = 1;
    [HideInInspector] public float coins = 0;
    [HideInInspector] public bool glasses = false, shield = false;
    [HideInInspector] public int hearts10 = 0, coins10 = 0, gems5 = 0, glasses10 = 0, truck10 = 0, let10 = 0, police5 = 0, bomb5 = 0, shield5 = 0;

    [SerializeField] private GameObject heart0, heart1, heart2, heart3, heart4;
    [SerializeField] private GoalsScript goalsScript;
    [SerializeField] private CarShop carShop;
    [SerializeField] private DistanceCounter distanceCounter;
    [SerializeField] private GameObject panelDead;
    [SerializeField] private TMP_Text coinsTMP;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>(8);
    [SerializeField] private Image glassesBarFill, shieldBarFill;
    [SerializeField] private AudioClip spawnSound, shieldSound;
    [SerializeField] private GameObject protectiveField;
    [SerializeField] private GameObject enemyParticles, shieldParticles;

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
        speed = PlayerPrefs.GetFloat("speed");

        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);
        coins = PlayerPrefs.GetFloat("coins");
        health = carShop.cars[PlayerPrefs.GetInt("equipped")].hp_lvl + 1;
        healthSwitch(health);
        coinsTMP.text = "Coins: " + coins;
        glasses = false;
        shield = false;
        protectiveField.SetActive(false);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprites[PlayerPrefs.GetInt("equipped")];
    }

    private void Update()
    {
        if (hearts10 >= 10) goalsScript.GoalAchieved(0);
        if (coins10 >= 10) goalsScript.GoalAchieved(1);
        if (gems5 >= 5) goalsScript.GoalAchieved(2);
        if (glasses10 >= 10) goalsScript.GoalAchieved(3);
        if (truck10 >= 10) goalsScript.GoalAchieved(11);
        if (let10 >= 10) goalsScript.GoalAchieved(12);
        if (police5 >= 5) goalsScript.GoalAchieved(13);
        if (bomb5 >= 5) goalsScript.GoalAchieved(16);
        if (shield5 >= 5) goalsScript.GoalAchieved(17);

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
        protectiveField.SetActive(shield);
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
        if (shield)
        {
            shieldTimeLeft -= Time.deltaTime;

            if (shieldTimeLeft <= 0f)
            {
                shieldTimeLeft = 0f;
                audioSource.PlayOneShot(shieldSound);
                Instantiate(shieldParticles, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -5), Quaternion.identity);

                shield = false;
            }

            shieldBarFill.fillAmount = shieldTimeLeft / shieldTimeMax;
        }
    }

    public void MoveToNextLane()
    {
        if (currentLaneIndex < lanes.Length - 1)
        {
            currentLaneIndex++;
            targetPos = new Vector3(lanes[currentLaneIndex], transform.position.y, -1.0f);
        }
    }

    public void MoveToPreviousLane()
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
        if (distanceCounter.distance >= 1f) goalsScript.GoalAchieved(4);
        if (distanceCounter.distance >= 2f) goalsScript.GoalAchieved(5);
        if (distanceCounter.distance >= 3f) goalsScript.GoalAchieved(6);
        if (distanceCounter.distance >= 4f) goalsScript.GoalAchieved(7);
        if (distanceCounter.distance >= 5f) goalsScript.GoalAchieved(8);

        glassesBarFill.fillAmount = 0;
        shieldBarFill.fillAmount = 0;
        panelDead.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Menu()
    {
        CoinAdd(distanceCounter.distance * 10f);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        CoinAdd(distanceCounter.distance * 10f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Continue()
    {
        if (coins >= 25)
        {
            coins -= 25;
            PlayerPrefs.SetFloat("coins", coins);
            coinsTMP.text = "Coins: " + coins;
            health = carShop.cars[PlayerPrefs.GetInt("equipped")].hp_lvl + 1;
            healthSwitch(health);
            panelDead.SetActive(false);
            gameObject.SetActive(true);
            DestroyAll();
            PlaySpawnSound();
            Time.timeScale = 1f;
        }
    }

    public void DestroyAll()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Instantiate(enemyParticles, new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5), Quaternion.identity);
            Destroy(enemy);
        }
    }

    public void Shield()
    {
        shield = true;
        shieldTimeMax = (carShop.cars[PlayerPrefs.GetInt("equipped")].shield_lvl + 1) * 2f;
        shieldTimeLeft = shieldTimeMax;
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
        glassesTimeMax = (carShop.cars[PlayerPrefs.GetInt("equipped")].glasses_lvl + 1) * 5f;
        glassesTimeLeft = glassesTimeMax;
    }

    private void PlaySpawnSound()
    {
        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
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

}