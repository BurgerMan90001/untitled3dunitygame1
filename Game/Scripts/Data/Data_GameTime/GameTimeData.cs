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

/// <summary>
/// <br> Months and days and in game time stuff.</br>
/// </summary>
[CreateAssetMenu (menuName = "Time/GameTimeData")]
public class GameTimeData : ScriptableObject
{
    [SerializeField] private GameObject _sun;
    [SerializeField] private GameObject _moon;
    

 //   public Volume volume;
//    public HDRPVolumeProfileSettings volumeProfileSettings;

    
    public Day Day;
    public Month Month;

    public float Time;

    public int Year; // MAYBE

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
    /// <summary>
    /// <br> Adds +1 to the Year count. <br> 
    /// <br> Not implemented yet. MAYBE. </br>
    /// </summary>
    public void ChangeYear() 
    {
        Year++;
        OnYearChanged?.Invoke(Year);

    }

}



