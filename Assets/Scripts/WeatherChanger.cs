using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DigitalRuby.RainMaker;
public class WeatherChanger : MonoBehaviour
{
    [SerializeField] GameObject rain;
    [SerializeField] GameObject sky;

    private void Awake()
    {
        if (rain != null)
        {
            rain.GetComponent<RainScript2D>().RainIntensity = 0;
            rain.transform.GetChild(0).GetComponent<ParticleSystemRenderer>().sortingOrder = -103;
            rain.transform.GetChild(1).GetComponent<ParticleSystemRenderer>().sortingOrder = -102;
            rain.transform.GetChild(2).GetComponent<ParticleSystemRenderer>().sortingOrder = -101;
        }

        Invoke("StartRain", 40);
        Invoke("ChangeWeather", 40);
    }

    private void StartRain()
    {
        if (rain != null)
        {
            rain.GetComponent<RainScript2D>().RainIntensity = 0.6f;
            Invoke("StopRain", 20);
        }
    }

    private void StopRain()
    {
        rain.GetComponent<RainScript2D>().RainIntensity = 0f;
        Invoke("StartRain", 40);
    }

    private void ChangeWeather()
    {
        int random = Random.Range(0, 5);

        if(random == 0)
            sky.GetComponent<SpriteRenderer>().DOColor(new Color(0.6745098f, 0.8235294f, 1, 1), 5);
        if(random == 1)
            sky.GetComponent<SpriteRenderer>().DOColor(new Color(0.381319f, 0.3838249f, 0.3867925f, 5), 1);
        if(random == 2)
            sky.GetComponent<SpriteRenderer>().DOColor(new Color(0.1603774f, 0.1603774f, 0.1603774f, 5), 1);
        if(random == 3)
            sky.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 1f, 1f, 1), 1);
        if(random == 4)
            sky.GetComponent<SpriteRenderer>().DOColor(new Color(1f, 0.7018241f, 0.4669811f, 1), 5);

        Invoke("ChangeWeather", 60);
    }
}
