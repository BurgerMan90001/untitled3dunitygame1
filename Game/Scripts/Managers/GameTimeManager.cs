using System;
using UnityEngine;
public class GameTimeManager : Manager
{
    [Header("Data")]
    [SerializeField] private GameTimeData _gameTimeData;

    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;
    private void Awake()
    {

    }
    private void Start()
    {

    }
    private void OnEnable()
    {

    }

    private void OnValidate()
    {
        //    _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }
    private void Update()
    {
        _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }

    public override void Initialize()
    {
        Debug.Log("GameTimeManager Initializeed");
        throw new NotImplementedException();
    }
}
