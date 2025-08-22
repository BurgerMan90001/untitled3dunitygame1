using UnityEngine;

public class DayNightCycle
{

    public string TwelveHourTime;



    private GameTimeEvents _gameTimeEvents;

    //  private int lastEventHour = -1;


    public DayNightCycle(GameTimeEvents gameTimeEvents)
    {
        _gameTimeEvents = gameTimeEvents;

    }



    public void UpdateSun(Light sun, int time, Vector3 sunInitialRotation)
    {

        float sunRotation = time / 24f * 360f;
        sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);

    }

    public void ShowTime(int currentHour)
    {
        if (currentHour == 0)
        {
            TwelveHourTime = 12 + " AM";
        }
        else if (currentHour < 12)
        {
            TwelveHourTime = currentHour + " PM";
        }
        else if (currentHour == 12)
        {
            TwelveHourTime = currentHour + " AM";
        }

        else
        {
            TwelveHourTime = currentHour - 12 + " AM";
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

