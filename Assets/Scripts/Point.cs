using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private GameObject enemyCar, enemyLet, coin, health, glasses, police, gem, bomb, shield;

    private void Start()
    {
        int rand = Random.Range(0, 100);
        if (rand < 9)
        { //8% glasses
            InstantiateObj(glasses);
        }
        else if (rand < 18)
        { //8% coin
            InstantiateObj(coin);
        }
        else if (rand < 19)
        { //1% bomb
            InstantiateObj(bomb);
        }
        else if (rand < 20)
        { //1% shield
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