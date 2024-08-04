using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Text Coins;
    public Button HP_update_btn, coin_update_btn, glasses_update_btn, gem_update_btn;
    public Text HP_price_text, coin_price_text, glasses_price_text, gem_price_text;
    private float HP_cost = 10f, coin_cost = 10f, glasses_cost = 10f, gem_cost = 10f;

    void Start()
    {

        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);

        if (!PlayerPrefs.HasKey("pumpingСoins") || PlayerPrefs.GetFloat("pumpingСoins") == 0) PlayerPrefs.SetFloat("pumpingСoins", 1f);
        if (!PlayerPrefs.HasKey("health") || PlayerPrefs.GetInt("health") == 0) PlayerPrefs.SetInt("health", 1);
        if (!PlayerPrefs.HasKey("gem") || PlayerPrefs.GetFloat("gem") == 0) PlayerPrefs.SetFloat("gem", 5f);

        if (!PlayerPrefs.HasKey("HP_lvl")) PlayerPrefs.SetInt("HP_lvl", 0);
        if (!PlayerPrefs.HasKey("coin_lvl")) PlayerPrefs.SetInt("coin_lvl", 0);
        if (!PlayerPrefs.HasKey("glasses_lvl")) PlayerPrefs.SetFloat("glasses", 0);
        if (!PlayerPrefs.HasKey("gem_lvl")) PlayerPrefs.SetInt("gem_lvl", 0);
    }
    void Update()
    {
        Coins.text = ": " + PlayerPrefs.GetFloat("coins");
        switch (PlayerPrefs.GetInt("HP_lvl"))
        {
            case 0:
                HP_price_text.text = ": 10";
                HP_cost = 10f;
                break;
            case 1:
                HP_price_text.text = ": 25";
                HP_cost = 25f;
                break;
            case 2:
                HP_price_text.text = ": 50";
                HP_cost = 50f;
                break;
            case 3:
                HP_price_text.text = ": 100";
                HP_cost = 100f;
                break;
            case 4:
                HP_price_text.text = ": X";
                HP_update_btn.interactable = false;
                break;
        }
        switch (PlayerPrefs.GetInt("coin_lvl"))
        {
            case 0:
                coin_price_text.text = ": 10";
                coin_cost = 10f;
                break;
            case 1:
                coin_price_text.text = ": 25";
                coin_cost = 25f;
                break;
            case 2:
                coin_price_text.text = ": 50";
                coin_cost = 50f;
                break;
            case 3:
                coin_price_text.text = ": 100";
                coin_cost = 100f;
                break;
            case 4:
                coin_price_text.text = ": X";
                coin_update_btn.interactable = false;
                break;
        }
        switch (PlayerPrefs.GetInt("glasses_lvl"))
        {
            case 0:
                glasses_price_text.text = ": 10";
                glasses_cost = 10f;
                break;
            case 1:
                glasses_price_text.text = ": 25";
                glasses_cost = 25f;
                break;
            case 2:
                glasses_price_text.text = ": 50";
                glasses_cost = 50f;
                break;
            case 3:
                glasses_price_text.text = ": 100";
                glasses_cost = 100f;
                break;
            case 4:
                glasses_price_text.text = ": X";
                glasses_update_btn.interactable = false;
                break;
        }
        switch (PlayerPrefs.GetInt("gem_lvl"))
        {
            case 0:
                gem_price_text.text = ": 10";
                gem_cost = 10f;
                break;
            case 1:
                gem_price_text.text = ": 25";
                gem_cost = 25f;
                break;
            case 2:
                gem_price_text.text = ": 50";
                gem_cost = 50f;
                break;
            case 3:
                gem_price_text.text = ": 100";
                gem_cost = 100f;
                break;
            case 4:
                gem_price_text.text = ": X";
                gem_update_btn.interactable = false;
                break;
        }
    }
    public void HP_update_down()
    {
        if (PlayerPrefs.GetFloat("coins") >= HP_cost && PlayerPrefs.GetInt("HP_lvl") < 4)
        {
            PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - HP_cost);
            PlayerPrefs.SetInt("HP_lvl", PlayerPrefs.GetInt("HP_lvl") + 1);
            PlayerPrefs.SetInt("health", PlayerPrefs.GetInt("health") + 1);
        }
    }
    public void coin_update_down()
    {
        if (PlayerPrefs.GetFloat("coins") >= coin_cost && PlayerPrefs.GetInt("coin_lvl") < 4)
        {
            PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - coin_cost);
            PlayerPrefs.SetInt("coin_lvl", PlayerPrefs.GetInt("coin_lvl") + 1);
            PlayerPrefs.SetFloat("pumpingСoins", PlayerPrefs.GetFloat("pumpingСoins") + 0.25f);
        }
    }
    public void glasses_update_down()
    {
        if (PlayerPrefs.GetFloat("coins") >= glasses_cost && PlayerPrefs.GetInt("glasses_lvl") < 4)
        {
            PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - glasses_cost);
            PlayerPrefs.SetInt("glasses_lvl", PlayerPrefs.GetInt("glasses_lvl") + 1);
        }
    }
    public void gem_update_down()
    {
        if (PlayerPrefs.GetFloat("coins") >= gem_cost && PlayerPrefs.GetInt("gem_lvl") < 4)
        {
            PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - gem_cost);
            PlayerPrefs.SetInt("gem_lvl", PlayerPrefs.GetInt("gem_lvl") + 1);
            PlayerPrefs.SetFloat("gem", PlayerPrefs.GetFloat("gem") + 5f);
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        HP_update_btn.interactable = true;
        coin_update_btn.interactable = true;
        glasses_update_btn.interactable = true;

        if (!PlayerPrefs.HasKey("pumpingСoins") || PlayerPrefs.GetFloat("pumpingСoins") == 0) PlayerPrefs.SetFloat("pumpingСoins", 1f);
        if (!PlayerPrefs.HasKey("health") || PlayerPrefs.GetInt("health") == 0) PlayerPrefs.SetInt("health", 1);
        if (!PlayerPrefs.HasKey("gem") || PlayerPrefs.GetFloat("gem") == 0) PlayerPrefs.SetFloat("gem", 5f);
    }
}