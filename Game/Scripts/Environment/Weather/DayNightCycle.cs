using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.HighDefinition;

public class DayNightCycle : MonoBehaviour
{
    [Header("Sun Settings")]
    public Light sun;
//    [SerializeField] Light moon;
    [SerializeField] private float dayLength = 120f;
    [SerializeField] private Vector3 sunInitialRotation = new Vector3(50f, -30f, 0f);

    [Header("Events")]
    public Action OnSunrise;
    public Action OnNoon;
    public Action OnSunset;
    public Action OnMidnight;

    [Header("Debug")]
    [Range(0f, 24f)]
    public float timeOfDay;
    public string twelveHourTime;
    

    private float currentTime;
    private float timeMultiplier;

    // Constants for event times
    private const float SunriseTime = 6f; // 6 AM
    private const float NoonTime = 12f; // 12 PM
    private const float SunsetTime = 18f; // 6 PM
    private const float MidnightTime = 0f; // 12 AM

    // Track last triggered hour to avoid repeated invocations
    private int lastEventHour = -1;

    private void Start()
    {
        if (sun == null)
        {
            Debug.LogError("Sun Light is not assigned.");
            enabled = false;
            return;
        }

        currentTime = timeOfDay / 24f * dayLength;
        timeMultiplier = 24f / dayLength;
    }

    private void Update()
    {
        if (sun == null) return;

        currentTime = Mathf.Repeat(currentTime + Time.deltaTime * timeMultiplier, dayLength);
        timeOfDay = currentTime / dayLength * 24f;

        float sunRotation = (timeOfDay / 24f) * 360f;
        sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);
        //    sun.intensity
        //     moon.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation +180f, sunInitialRotation.y, sunInitialRotation.z);
        //   RenderSettings.skybox.
        //    RenderSettings.skybox.SetFloat("_Exposure", 1);

    //    TriggerEvents(); // Trigger events at specific times
        
    }
    private void UpdateSun()
    {
        if (sun == null) return;

        currentTime = Mathf.Repeat(currentTime + Time.deltaTime * timeMultiplier, dayLength);
        timeOfDay = currentTime / dayLength * 24f;

        float sunRotation = (timeOfDay / 24f) * 360f;
        sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);
        //    sun.intensity
        //     moon.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation +180f, sunInitialRotation.y, sunInitialRotation.z);
        //   RenderSettings.skybox.
        //    RenderSettings.skybox.SetFloat("_Exposure", 1);

        TriggerEvents(); // Trigger events at specific times
    }
    private void CheckCurrentTime()
    {

    }
    private void TriggerEvents()
    {
        int currentHour = Mathf.FloorToInt(timeOfDay);

        if (currentHour != lastEventHour)
        {
            if (currentHour == (int)SunriseTime)
                OnSunrise.Invoke();
            else if (currentHour == (int)NoonTime)
                OnNoon.Invoke();
            else if (currentHour == (int)SunsetTime)
                OnSunset.Invoke();
            else if (currentHour == (int)MidnightTime)
                OnMidnight.Invoke();

            lastEventHour = currentHour;
        }
    }
    private void ShowTime()
    {
        if (timeOfDay < 12) 
        {
            twelveHourTime = Mathf.FloorToInt(timeOfDay) + " AM";
        } else
        {
            twelveHourTime = Mathf.FloorToInt(timeOfDay -12) + " PM";
        }
    }
    private void OnValidate()
    {
        // Update the sun's rotation in the editor
        if (sun != null )
        {
            float sunRotation = (timeOfDay / 24f) * 360f;
            sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);
            ShowTime();
        //    moon.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation + 180f, sunInitialRotation.y, sunInitialRotation.z);

        }
    }
}

