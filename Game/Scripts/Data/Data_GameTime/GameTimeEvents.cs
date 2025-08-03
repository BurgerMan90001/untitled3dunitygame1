using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/GameTimeEvents")]
public class GameTimeEvents : Event
{
    public event Action OnSunrise;
    public event Action OnNoon;
    public event Action OnSunset;
    public event Action OnMidnight;

    public Action<int> OnHourChanged;
    public Action<Day> OnDayChanged;
    public Action<Month> OnMonthChanged;
    public Action<int> OnYearChanged;

    public void ChangeHour(int hour)
    {
        OnHourChanged?.Invoke(hour);
    }
    public void ChangeDay(Day newDay)
    {
        OnDayChanged?.Invoke(newDay);
    }
    public void ChangeMonth(Month newMonth)
    {
        OnMonthChanged?.Invoke(newMonth);
    }
    public void Sunrise()
    {
        OnSunrise?.Invoke();
    }
    public void Noon()
    {
        OnNoon?.Invoke();
    }
    public void Sunset()
    {
        OnSunset?.Invoke();
    }
    public void Midnight()
    {
        OnMidnight?.Invoke();
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
