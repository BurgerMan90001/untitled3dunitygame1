using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/GameTimeEvents")]
public class GameTimeEvents : Event
{
    // special hours
    public const int SunsetTime = 9; // 9 PM
    public const int SunriseTime = 21; // 9 AM
    public const int NoonTime = 0; // 12 PM
    public const int MidnightTime = 12; // 12 AM
    // special hour events
    public event Action OnSunset;
    public event Action OnSunrise;
    public event Action OnNoon;
    public event Action OnMidnight;

    public Action<int> OnHourChanged;
    public Action<Day> OnDayChanged;
    public Action<Month> OnMonthChanged;
    public Action<int> OnYearChanged;

    public void ChangeHour(int hour)
    {

        if (InvokeSpecialHour(hour))
        {
            Debug.Log($" SPECIAL HOUR {hour}");
            return;
        }

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

    private bool InvokeSpecialHour(int hour)
    {
        switch (hour)
        {
            case SunriseTime:
                SetSunrise();
                return true;
            case NoonTime:
                SetNoon();
                return true;
            case SunsetTime:
                SetSunset();
                return true;
            case MidnightTime:
                SetMidnight();
                return true;
            default:
                return false; // Return false if no special hour event was invoked

        }
    }
    public void SetSunrise()
    {
        OnSunrise?.Invoke();
    }
    public void SetNoon()
    {
        OnNoon?.Invoke();
    }
    public void SetSunset()
    {
        OnSunset?.Invoke();
    }
    public void SetMidnight()
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

// MAYBE
/*
[System.Serializable]
public class SpecialHour
{
    public event Action OnHourEvent;
    public int Hour;

    public void Invoke()
    {
        OnHourEvent?.Invoke();
    }

}
*/
