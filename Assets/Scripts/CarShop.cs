using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CarShop : MonoBehaviour
{
    public enum CarColor
    {
        Black, Brown, Green, Orange, Purple, Red, White, Yellow
    }
    public struct Car
    {
        public CarColor color;
        public int hp_lvl, coin_lvl, glasses_lvl, gem_lvl;
        public int price;
    };

    public List<Car> cars = new List<Car>
    {
        new Car { color = CarColor.Brown,  hp_lvl = 0, coin_lvl = 0, glasses_lvl = 0, gem_lvl = 0, price = 0 },
        new Car { color = CarColor.Black,  hp_lvl = 4, coin_lvl = 1, glasses_lvl = 0, gem_lvl = 0, price = 1000 },
        new Car { color = CarColor.White,  hp_lvl = 1, coin_lvl = 1, glasses_lvl = 4, gem_lvl = 1, price = 1400 },
        new Car { color = CarColor.Yellow, hp_lvl = 0, coin_lvl = 4, glasses_lvl = 0, gem_lvl = 4, price = 1600 },
        new Car { color = CarColor.Purple, hp_lvl = 4, coin_lvl = 0, glasses_lvl = 4, gem_lvl = 0, price = 1600 },
        new Car { color = CarColor.Black,  hp_lvl = 3, coin_lvl = 2, glasses_lvl = 3, gem_lvl = 2, price = 2000 },
        new Car { color = CarColor.Orange, hp_lvl = 2, coin_lvl = 3, glasses_lvl = 2, gem_lvl = 3, price = 2000 },
        new Car { color = CarColor.Red,    hp_lvl = 4, coin_lvl = 4, glasses_lvl = 4, gem_lvl = 4, price = 3200 },
    };


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
