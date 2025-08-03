using System;
using UnityEngine;
using UnityEngine.Rendering;
#region
/// <summary>
/// <br> Months and days and in game time stuff.</br>
/// </summary>
#endregion



public class GameTimeManager : MonoBehaviour
{
    [Header("Time")]
    public Day Day;
    public Month Month;
    [Range(0f, 24f)] public int Hour;
    public int Year; // MAYBE

    [Header("Events")]
    [SerializeField] private GameTimeEvents _events;

    [Header("Lights")]
    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    [Header("Volumes")]
    [SerializeField] private Volume _dayGlobalVolume;

    [Header("Settings")]
    [SerializeField] private Vector3 _sunInitialRotation = new Vector3(50f, -30f, 0f);
    [SerializeField] private float dayLength = 120f;

    private DayNightCycle _dayNightCycle;

    private void Awake()
    {
        _dayNightCycle = new DayNightCycle(_events);
    }
    private void OnEnable()
    {
        _events.OnDayChanged += OnDayChanged;
        _events.OnMonthChanged += OnMonthChanged;
    }
    private void OnDestroy()
    {
        _events.OnDayChanged -= OnDayChanged;
        _events.OnMonthChanged -= OnMonthChanged;
    }

    private void OnDayChanged(Day day)
    {
        Day = day;
        Debug.Log("DAY CHANGED");

    }
    private void OnMonthChanged(Month month)
    {
        Month = month;
        Debug.Log("MONTH CHANGED");
    }



    public void IncrementHour(int value)
    {
        int newTimeOfDay = Hour + value;
        if (newTimeOfDay >= 24)
        {
            Hour = 0;
        }
        else
        {
            Hour = newTimeOfDay;
        }
        Hour += value;
    }

    private void OnValidate()
    {
        Debug.Log("ASDASdasd");
        UpdateSun(_sun, Hour, _sunInitialRotation);
        //    _dayNightCycle.UpdateSun(_sun, Hour, _sunInitialRotation);
    }
    private void Update()
    {
        //    _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }

    public void UpdateSun(Light sun, int time, Vector3 sunInitialRotation)
    {

        float sunRotation = time / 24f * 360f;
        sun.transform.rotation = Quaternion.Euler(sunInitialRotation.x + sunRotation, sunInitialRotation.y, sunInitialRotation.z);

    }
}
