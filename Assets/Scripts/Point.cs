using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject enemyCar, enemyLet, coin, health, glasses, police, gem;
    void Start()
    {
        int rand = Random.Range(0, 100);
        if(rand<10 ){ //10%
            float newZ = -1.0f;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
            Instantiate(glasses, position, Quaternion.identity);
        }
        else if(rand<20){ //10%
            float newZ = -1.0f;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
            Instantiate(coin, position, Quaternion.identity);
        }
        else if(rand<25){ //5%
            float newZ = -1.0f;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
            Instantiate(health, position, Quaternion.identity);
        }
        else if(rand<27){ // 2%
            float newZ = -1.0f;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
            Instantiate(police, position, Quaternion.identity);
        }
        else if(rand<28){ // 1%
            float newZ = -1.0f;
            Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
            Instantiate(gem, position, Quaternion.identity);
        }
        else{ //72%
            int rand1 = Random.Range(0, 3);
            if(rand1 == 0){ //24%
                float newZ = -1.0f;
                Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
                Instantiate(enemyLet, position, Quaternion.identity);
            }
            else{ //48%
                float newZ = -1.0f;
                Vector3 position = new Vector3(transform.position.x, transform.position.y, newZ);
                Instantiate(enemyCar, position, Quaternion.identity);
            }
            
        }
        Destroy(gameObject);
    }

}