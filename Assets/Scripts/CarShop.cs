using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class CarShop : MonoBehaviour
{
    public enum CarColor
    {
        Black, Brown, Green, Orange, Purple, Red, White, Yellow
    }

    [System.Serializable]
    public class Car
    {
        public CarShop.CarColor color;
        public int hp_lvl, coin_lvl, glasses_lvl, gem_lvl, shield_lvl;
        public int price;
        public Button button;
        public TMP_Text text;
    }

    public List<Car> cars = new List<Car>(8);
    [SerializeField] private TMP_Text coinsTMP, maxDistanceTMP;

    private void Awake()
    {
        while (cars.Count < 8) cars.Add(new Car());

        cars[0].color = CarColor.Brown;
        cars[0].price = 0;
        cars[0].hp_lvl = 0;
        cars[0].coin_lvl = 0;
        cars[0].glasses_lvl = 0;
        cars[0].gem_lvl = 0;
        cars[0].shield_lvl = 0;

        cars[1].color = CarColor.Black;
        cars[1].price = 800;
        cars[1].hp_lvl = 4;
        cars[1].coin_lvl = 0;
        cars[1].glasses_lvl = 0;
        cars[1].gem_lvl = 0;
        cars[1].shield_lvl = 4;

        cars[2].color = CarColor.White;
        cars[2].price = 800;
        cars[2].hp_lvl = 1;
        cars[2].coin_lvl = 1;
        cars[2].glasses_lvl = 4;
        cars[2].gem_lvl = 1;
        cars[2].shield_lvl = 1;

        cars[3].color = CarColor.Yellow;
        cars[3].price = 800;
        cars[3].hp_lvl = 0;
        cars[3].coin_lvl = 4;
        cars[3].glasses_lvl = 0;
        cars[3].gem_lvl = 4;
        cars[3].shield_lvl = 0;

        cars[4].color = CarColor.Purple;
        cars[4].price = 1000;
        cars[4].hp_lvl = 4;
        cars[4].coin_lvl = 0;
        cars[4].glasses_lvl = 4;
        cars[4].gem_lvl = 0;
        cars[4].shield_lvl = 2;

        cars[5].color = CarColor.Green;
        cars[5].price = 1300;
        cars[5].hp_lvl = 3;
        cars[5].coin_lvl = 2;
        cars[5].glasses_lvl = 3;
        cars[5].gem_lvl = 2;
        cars[5].shield_lvl = 3;

        cars[6].color = CarColor.Orange;
        cars[6].price = 1200;
        cars[6].hp_lvl = 2;
        cars[6].coin_lvl = 3;
        cars[6].glasses_lvl = 2;
        cars[6].gem_lvl = 3;
        cars[6].shield_lvl = 2;

        cars[7].color = CarColor.Red;
        cars[7].price = 2000;
        cars[7].hp_lvl = 4;
        cars[7].coin_lvl = 4;
        cars[7].glasses_lvl = 4;
        cars[7].gem_lvl = 4;
        cars[7].shield_lvl = 4;

        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);
        if (!PlayerPrefs.HasKey("maxDistance")) PlayerPrefs.SetFloat("maxDistance", 0);
        if (!PlayerPrefs.HasKey("cars")) PlayerPrefs.SetString("cars", "10000000");
        if (!PlayerPrefs.HasKey("equipped")) PlayerPrefs.SetInt("equipped", 0);

        if (SceneManager.GetActiveScene().buildIndex != 0) return;

        for (int i = 0; i < 8; ++i)
        {
            if (PlayerPrefs.GetString("cars")[i] == '1')
            {
                cars[i].text.text = "Equip";
            }
            else
            {
                cars[i].text.text = "Buy";
            }
        }
        cars[PlayerPrefs.GetInt("equipped")].text.text = "Equipped";

        for (int i = 0; i < 8; ++i)
        {
            int index = i;
            cars[index].button.onClick.AddListener(() => OnCarButtonClicked(index));
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;

        coinsTMP.text = "Coins: " + PlayerPrefs.GetFloat("coins");
        maxDistanceTMP.text = "Max Distance: " + PlayerPrefs.GetFloat("maxDistance") + " km";
    }

    private void OnCarButtonClicked(int index)
    {
        if (cars[index].text.text == "Equip")
        {
            EquipCar(index);
        }
        else if (cars[index].text.text == "Buy")
        {
            if (PlayerPrefs.GetFloat("coins") >= cars[index].price)
            {
                PlayerPrefs.SetFloat("coins", PlayerPrefs.GetFloat("coins") - cars[index].price);
                cars[index].text.text = "Equip";

                string n = PlayerPrefs.GetString("cars");
                char[] chars = PlayerPrefs.GetString("cars").ToCharArray();
                chars[index] = '1';
                n = new string(chars);
                PlayerPrefs.SetString("cars", n);
                EquipCar(index);
            }
        }
    }

    private void EquipCar(int index)
    {
        PlayerPrefs.SetInt("equipped", index);

        for (int i = 0; i < 8; ++i)
        {
            if (PlayerPrefs.GetString("cars")[i] == '1')
            {
                cars[i].text.text = "Equip";
            }
            else
            {
                cars[i].text.text = "Buy";
            }
        }
        cars[index].text.text = "Equipped";
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetFloat("coins", 0);
        if (!PlayerPrefs.HasKey("maxDistance")) PlayerPrefs.SetFloat("maxDistance", 0);
        if (!PlayerPrefs.HasKey("cars")) PlayerPrefs.SetString("cars", "10000000");

        if (!PlayerPrefs.HasKey("equipped")) PlayerPrefs.SetInt("equipped", 0);

        for (int i = 0; i < 8; ++i)
        {
            if (PlayerPrefs.GetString("cars")[i] == '1')
            {
                cars[i].text.text = "Equip";
            }
            else
            {
                cars[i].text.text = "Buy";
            }
        }
        cars[PlayerPrefs.GetInt("equipped")].text.text = "Equipped";

        for (int i = 0; i < 8; ++i)
        {
            int index = i;
            cars[index].button.onClick.AddListener(() => OnCarButtonClicked(index));
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}