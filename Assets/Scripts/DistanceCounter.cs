using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class DistanceCounter : MonoBehaviour
{
    public TMP_Text distanceText;
    public float distance;
    public Spawner spawnerCs;
    void Start()
    {
        distance = 0;
        distanceText.text = "Distance: " + distance;
    }

    void Update()
    {
        distance += Time.deltaTime * 50f;
        float value = Mathf.Round((distance * (1f / spawnerCs.startTimeBtwSpawn) / 1000f)*10f) / 10f;
        distanceText.text = "Distance: " + value + " km";
        if (value > PlayerPrefs.GetFloat("maxDistance"))
        {
            PlayerPrefs.SetFloat("maxDistance", value);
        }
    }
}
