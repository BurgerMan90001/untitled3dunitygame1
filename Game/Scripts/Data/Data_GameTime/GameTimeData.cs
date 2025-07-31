using System;

public class GameTimeData : Data
{

    //  [SerializeField] private Vector3 sunInitialRotation = new Vector3(50f, -30f, 0f);



    //   public Volume volume;
    //    public HDRPVolumeProfileSettings volumeProfileSettings;

    //      public DayNightCycle DayNightCycle { get; private set; } = new DayNightCycle();
    // public GameTimeEvents Events { get; private set; } = new GameTimeEvents();



    /*
    public void Initilize(Light sun, Light moon)
    {
        _sun = sun;
        _moon = moon;

        _initilized = true;
    }
    */
    /*
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
    
    */

    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }
}
/*
private void OnDayChanged(Day day)
{

}
private void OnMonthChanged(Month month)
{

}
*/





