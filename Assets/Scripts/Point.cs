using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject enemyCar, enemyLet, coin, health, glasses, police, gem, bomb, shield;
    void Start()
    {
        int rand = Random.Range(0, 100);
        if (rand < 8)
        { //8% glasses
            InstantiateObj(glasses);
        }
        else if (rand < 16)
        { //8% coin
            InstantiateObj(shield);
        }
        else if (rand < 18)
        {
            InstantiateObj(bomb);
        }
        else if (rand < 20)
        {
            InstantiateObj(shield);
        }
        else if (rand < 25)
        { //5% health
            InstantiateObj(health);
        }
        else if (rand < 27)
        { // 2% police
            InstantiateObj(police);
        }
        else if (rand < 28)
        { // 1% gem
            InstantiateObj(gem);
        }
        else
        { //72%
            int rand1 = Random.Range(0, 3);
            if (rand1 == 0)
            { //24% let
                InstantiateObj(enemyLet);
            }
            else
            { //48% truck
                InstantiateObj(enemyCar);
            }

        }
        Destroy(gameObject);
    }
    private void InstantiateObj(GameObject obj)
    {
        float newZ = -1.0f;
        Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
        Instantiate(obj, position, Quaternion.identity);
    }
}