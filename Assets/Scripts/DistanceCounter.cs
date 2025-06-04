using UnityEngine;
using TMPro;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField] private Spawner spawnerCs;
    [SerializeField] private TMP_Text distanceText;
    public float distance, value;

    private void Start()
    {
        distance = 0;
        distanceText.text = "Distance: " + distance;
    }

    private void Update()
    {
        value += Time.deltaTime * 50f;
        distance = Mathf.Round((value * (1f / spawnerCs.startTimeBtwSpawn) / 1000f) * 10f) / 10f;
        distanceText.text = "Distance: " + distance + " km";
        if (distance > PlayerPrefs.GetFloat("maxDistance"))
        {
            PlayerPrefs.SetFloat("maxDistance", distance);
        }
    }
}
