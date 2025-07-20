using System;
using UnityEngine;

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
