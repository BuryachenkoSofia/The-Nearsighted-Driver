using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyVariants;
    private float timeBtwSpawn;
    public float startTimeBtwSpawn = 2f;
    private float decreaseTime = 0.05f;
    private float minTime = 0.65f;

    void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            Spawn();
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
    public void Spawn()
    {
        int rand = Random.Range(0, enemyVariants.Length);
        Instantiate(enemyVariants[rand], transform.position, Quaternion.identity);
    }
}
