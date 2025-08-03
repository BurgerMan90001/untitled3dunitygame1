using UnityEngine;

public class DayNightCycle
{

    private const int SUNSET_TIME = 9; // 9 PM
    private const int SUNRISE_TIME = 21; // 9 AM
    private const int NOON_TIME = 0; // 12 PM
    private const int MIDNIGHT_TIME = 12; // 12 AM

    public string twelveHourTime;

    public int TotalTime;

    /*
    private float currentTime;

    private float timeMultiplier;
    */


    private GameTimeEvents _gameTimeEvents;

    private int lastEventHour = -1;


    public DayNightCycle(GameTimeEvents gameTimeEvents)
    {
        _gameTimeEvents = gameTimeEvents;
        /*
        _moon = moon;
        _sun = sun;
        */
    }
    /*
    private void Start()
    {
        if (_sun == null)
        {
            Debug.LogError("Sun Light is not assigned.");
            enabled = false;
            return;
        }

        currentTime = timeOfDay / 24f * dayLength;
        timeMultiplier = 24f / dayLength;
    }
    */



    public void UpdateSun(Light sun, int time, Vector3 sunInitialRotation)
    {

        float sunRotation = time / 24f * 360f;
        sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);

    }

    public void ShowTime(int currentHour)
    {
        if (currentHour == 0)
        {
            twelveHourTime = 12 + " AM";
        }
        else if (currentHour < 12)
        {
            twelveHourTime = currentHour + " PM";
        }
        else if (currentHour == 12)
        {
            twelveHourTime = currentHour + " AM";
        }

        else
        {
            twelveHourTime = currentHour - 12 + " AM";
        }

    }

    /*
    private void Update()
    {
        if (_sun == null) return;

        currentTime = Mathf.Repeat(currentTime + Time.deltaTime * timeMultiplier, dayLength);
        timeOfDay = Mathf.FloorToInt(currentTime / dayLength * 24);

        float sunRotation = (timeOfDay / 24f) * 360f;
        _sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);
        //    sun.intensity
        //     moon.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation +180f, sunInitialRotation.y, sunInitialRotation.z);
        //   RenderSettings.skybox.
        //    RenderSettings.skybox.SetFloat("_Exposure", 1);

        //    TriggerEvents(); // Trigger events at specific times

    }
    
    */
}

