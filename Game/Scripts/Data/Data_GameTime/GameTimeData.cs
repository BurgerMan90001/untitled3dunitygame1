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
public class GameTimeData : ScriptableObject, IDataPersistence
{
    [Header("Settings")]
   
    [SerializeField] private float dayLength = 120f;
  //  [SerializeField] private Vector3 sunInitialRotation = new Vector3(50f, -30f, 0f);

    [Header("Data")]
   
    [Header("Debug")]


    private Light _sun;
    private Light _moon;

    //   public Volume volume;
    //    public HDRPVolumeProfileSettings volumeProfileSettings;

    public DayNightCycle DayNightCycle { get; private set; } = new DayNightCycle();
    public GameTimeEvents Events { get; private set; } = new GameTimeEvents();

    public Day Day;
    public Month Month;
    [Range(0f, 24f)] public int Hour;

    public int Year; // MAYBE
    /*
    public void Initilize(Light sun, Light moon)
    {
        _sun = sun;
        _moon = moon;

        _initilized = true;
    }
    */
    private void OnEnable()
    {
        
        Events.OnDayChanged += OnDayChanged;
        Events.OnMonthChanged += OnMonthChanged;
    }
    private void OnDisable()
    {

        Events.OnDayChanged -= OnDayChanged;
        Events.OnMonthChanged -= OnMonthChanged;
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
    private void OnDayChanged(Day day)
    {

    }
    private void OnMonthChanged(Month month)
    {

    }

    public void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }
}



