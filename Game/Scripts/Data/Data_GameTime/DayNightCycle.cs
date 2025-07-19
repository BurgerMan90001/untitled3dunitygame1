using System;
using UnityEngine;

public class DayNightCycle
{
    
    private Vector3 sunInitialRotation = new Vector3(50f, -30f, 0f);


    public event Action OnSunrise;
    public event Action OnNoon;
    public event Action OnSunset;
    public event Action OnMidnight;

    /*
    private Light _sun;
    private Light _moon;
    */

    public string twelveHourTime;

    public int TotalTime;

    private float currentTime;
    private float timeMultiplier;


    private const int SUNSET_TIME = 9; // 9 PM
    private const int SUNRISE_TIME = 21; // 9 AM
    private const int NOON_TIME = 0; // 12 PM
    private const int MIDNIGHT_TIME = 12; // 12 AM

    private int lastEventHour = -1;


    public DayNightCycle()
    {
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
    
    

    public void UpdateSun(Transform sun, int time)
    {
       
        float sunRotation = (time / 24f) * 360f;
        sun.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);


        //    moon.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation + 180f, sunInitialRotation.y, sunInitialRotation.z);
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
    public void TriggerEvents(int currentHour)
    {

        if (currentHour != lastEventHour)
        {
            switch (currentHour)
            {
                case SUNRISE_TIME:
                    Debug.Log("SUNRISE");
                    OnSunrise?.Invoke();
                   // _sun.enabled = true;
                    break;
                case SUNSET_TIME:
                    OnSunset?.Invoke();
                    Debug.Log("SUNSET");
                //    _sun.enabled = false;
                    break;

                case NOON_TIME:
                    OnNoon?.Invoke();
                    Debug.Log("NOON");
                    break;

                case MIDNIGHT_TIME:
                    OnMidnight?.Invoke();
                    Debug.Log("MIDNIGHT");
                    break;

            }

            lastEventHour = currentHour;
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

