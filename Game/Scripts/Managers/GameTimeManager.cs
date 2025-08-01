using System;
using UnityEngine;
using UnityEngine.Rendering;
#region
/// <summary>
/// <br> Months and days and in game time stuff.</br>
/// </summary>
#endregion



//TODO FIX GAMETIME DAYNIGHT CYCLE
public class GameTimeManager : MonoBehaviour
{

    public Volume test;
    [Header("Events")]
    [SerializeField] private GameTimeEvents _events;
    private DayNightCycle _dayNightCycle;
    [Header("Time")]
    public Day Day;
    public Month Month;
    [Range(0f, 24f)] public int Hour;

    public int Year; // MAYBE

    [Header("Lights")]
    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    [Header("Settings")]

    [SerializeField] private float dayLength = 120f;

    private void Awake()
    {
        _dayNightCycle = new DayNightCycle();
    }
    private void Start()
    {

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
        //    _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }
    private void Update()
    {
        //    _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }


}
