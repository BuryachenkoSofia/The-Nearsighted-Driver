using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DistanceCounter : MonoBehaviour
{
    public TMP_Text distanceText;
    public float distance, value;
    public Spawner spawnerCs;
    void Start()
    {
        distance = 0;
        distanceText.text = "Distance: " + distance;
    }

    void Update()
    {
        value += Time.deltaTime * 50f;
        distance = Mathf.Round((value * (1f / spawnerCs.startTimeBtwSpawn) / 1000f)*10f) / 10f;
        distanceText.text = "Distance: " + distance + " km";
        if (distance > PlayerPrefs.GetFloat("maxDistance"))
        {
            PlayerPrefs.SetFloat("maxDistance", distance);
        }
    }
}
