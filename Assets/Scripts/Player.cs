using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public int health = 1; 
    public float coins = 0; 
    public GameObject heart0, heart1, heart2, heart3, heart4;
    public PostProcessVolume ppVolume;
    public GameObject panelDead;
    private Vector2 targetPos;

    public Text coinsText;
    private float speed = 20;
    private float[] lanes = {-6f, -3f, 0f, 3f, 6f}; 
    private int currentLaneIndex = 2; 
    public bool glasses = false;
    private Coroutine glassesCoroutine;
    public AudioClip spawnSound;
    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        gameObject.SetActive(true);

        Time.timeScale = 1f;
        targetPos = new Vector2(lanes[currentLaneIndex], transform.position.y);
        ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();

        if (!PlayerPrefs.HasKey("health") || PlayerPrefs.GetInt("health") == 0) PlayerPrefs.SetInt("health", 1);
        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);
        coins = PlayerPrefs.GetFloat("coins");
        health = PlayerPrefs.GetInt("health");
        mySwitch(health);
        coinsText.text = ": " + coins;
        glasses = false;
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

        if(health <= 0){
            Dead();
        }
        ppVolume.enabled = !glasses;
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


    public void Dead(){
        Time.timeScale = 0f;
        panelDead.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Continue(){
        if(coins>=10){
            coins-=10;
            PlayerPrefs.SetFloat("coins", coins);
            coinsText.text = ": " + coins;
            health=3;
            mySwitch(health);
            panelDead.SetActive(false);
            gameObject.SetActive(true);
            PlaySpawnSound();
            Time.timeScale = 1f;
        }
    }

    public void mySwitch(int health)
    {
        if(health<0)health=0;
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

    public void CoinAdd(float c){
        coins += c;
        PlayerPrefs.SetFloat("coins", coins);
        coinsText.text = ": " + coins;
    }


    public void ActivateGlasses()
    {
        glasses = true;
        if (glassesCoroutine != null)
        {
            StopCoroutine(glassesCoroutine);
        }
        PlayerPrefs.SetFloat("glasses",(PlayerPrefs.GetInt("glasses_lvl")+1)*5f);
        glassesCoroutine = StartCoroutine(RemoveGlassesAfterTime(PlayerPrefs.GetFloat("glasses")));
    }

    private IEnumerator RemoveGlassesAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        glasses = false;
        glassesCoroutine = null;
    }

    private void PlaySpawnSound() {
        if (audioSource != null && spawnSound != null) {
            audioSource.PlayOneShot(spawnSound);
        }
    }
}