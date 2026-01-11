using UnityEngine;

public enum WeatherState
{
    Clear,
    Rain,
    Snow,
    Sleet
};

public class Weather : MonoBehaviour
{

    public WeatherState state = WeatherState.Clear;
    [SerializeField] private ParticleSystem rain, snow;
    private float stateTimer;

    private float rainChance = 0.15f;
    private float snowChance = 0.10f;
    private float sleetTransitionChance = 0.2f;

    private float minDryTime = 10f;
    private float maxDryTime = 60f;

    private float minRainTime = 10f;
    private float maxRainTime = 30f;

    private float minSnowTime = 10;
    private float maxSnowTime = 30f;

    private float minSleetTime = 10f;
    private float maxSleetTime = 30f;

    private void Start()
    {
        SetClear();
    }
    private void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f) NextState();
    }

    void NextState()
    {
        switch (state)
        {
            case WeatherState.Clear:
                TryStartWeather();
                break;

            case WeatherState.Rain:
                TryTransitionFromRain();
                break;

            case WeatherState.Snow:
                TryTransitionFromSnow();
                break;

            case WeatherState.Sleet:
                TryTransitionFromSleet();
                break;
        }
    }

    private void TryStartWeather()
    {
        float rnd = Random.value;
        if (rnd < rainChance)
        {
            SetRain();
        }
        else if (rnd < rainChance + snowChance)
        {
            SetSnow();
        }
    }

    private void TryTransitionFromRain()
    {
        float rnd = Random.value;
        if (rnd < sleetTransitionChance)
        {
            SetSleet();
        }
        else
        {
            SetClear();
        }
    }

    private void TryTransitionFromSnow()
    {
        float rnd = Random.value;
        if (rnd < sleetTransitionChance)
        {
            SetSleet();
        }
        else
        {
            SetClear();
        }
    }

    private void TryTransitionFromSleet()
    {
        float rnd = Random.value;
        if (rnd < rainChance)
        {
            SetRain();
        }
        else if (rnd < rainChance + snowChance)
        {
            SetSnow();
        }
        else
        {
            SetClear();
        }
    }

    private void SetClear()
    {
        state = WeatherState.Clear;
        rain.gameObject.SetActive(false);
        snow.gameObject.SetActive(false);
        stateTimer = Random.Range(minDryTime, maxDryTime);
    }

    private void SetRain()
    {
        state = WeatherState.Rain;
        rain.gameObject.SetActive(true);
        snow.gameObject.SetActive(false);
        stateTimer = Random.Range(minRainTime, maxRainTime);
    }

    private void SetSnow()
    {
        state = WeatherState.Snow;
        rain.gameObject.SetActive(false);
        snow.gameObject.SetActive(true);
        stateTimer = Random.Range(minSnowTime, maxSnowTime);
    }

    private void SetSleet()
    {
        state = WeatherState.Sleet;
        rain.gameObject.SetActive(true);
        snow.gameObject.SetActive(true);
        stateTimer = Random.Range(minSleetTime, maxSleetTime);
    }
}