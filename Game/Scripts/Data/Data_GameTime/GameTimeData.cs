using System;
using UnityEngine;
public enum Day
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday,
}
public enum Month
{
    January,
    Febuary,
    March,
    April,
    May,
    June,
    July,
    August,
    September,
    October,
    November,
    December,
}
#region
/// <summary>
/// <br> Months and days and in game time stuff.</br>
/// </summary>
#endregion
[CreateAssetMenu (menuName = "Time/GameTimeData")]
public class GameTimeData : Data
{
    [Header("Settings")]
    [SerializeField] private Transform _sun;
    [SerializeField] private Transform _moon;
    [SerializeField] private float dayLength = 120f;
  //  [SerializeField] private Vector3 sunInitialRotation = new Vector3(50f, -30f, 0f);

    [Header("Data")]
   
    [Header("Debug")]
    [Range(0, 24)] public int timeOfDay;
    //   public Volume volume;
    //    public HDRPVolumeProfileSettings volumeProfileSettings;

    public DayNightCycle DayNightCycle { get; private set; } = new DayNightCycle();
    public GameTimeEvents Events { get; private set; } = new GameTimeEvents();

    public Day Day;
    public Month Month; //MAYBE

    [Range(0f, 24f)] public int Hour;


    public int Year; // MAYBE
    private void OnEnable()
    {
        Debug.Log("ENABLE");
        Events.OnDayChanged += OnDayChanged;
        Events.OnMonthChanged += OnMonthChanged;
    }
    private void OnDisable()
    {
        Debug.Log("DISABLE");
        Events.OnDayChanged -= OnDayChanged;
        Events.OnMonthChanged -= OnMonthChanged;
    }
    private void OnValidate()
    {
        /*
        if (_dayNightCycle == null)
        {
            _dayNightCycle = new DayNightCycle(_sun, _moon);
            Debug.LogWarning("The dayNightCycle class is null. Creating new. ");
            return;
        }

        _dayNightCycle.UpdateSun(_sun, Hour);
        */
    }
    public void IncrementHour(int value)
    {
        int newTimeOfDay = timeOfDay + value;
        if (newTimeOfDay >= 24)
        {
            timeOfDay = 0;
        }
        else
        {
            timeOfDay = newTimeOfDay;
        }
        Hour += value;
    }
    private void OnDayChanged(Day day)
    {

    }
    private void OnMonthChanged(Month month)
    {

    }

    

}



public class GameTimeEvents : IEvent
{
    public Action<Day> OnDayChanged;
    public Action<Month> OnMonthChanged;
    public Action<int> OnYearChanged;
    public void ChangeDay(Day newDay)
    {
        
        OnDayChanged?.Invoke(newDay);

    }

    public void ChangeMonth(Month newMonth)
    {
        OnMonthChanged?.Invoke(newMonth);

    }
    /*
    #region
    /// <summary>
    /// <br> Adds +1 to the Year count. <br> 
    /// <br> Not implemented yet. MAYBE. </br>
    /// </summary>
    #endregion
    public void ChangeYear(int )
    {
        
        OnYearChanged?.Invoke(Year);

    }
    */
}
