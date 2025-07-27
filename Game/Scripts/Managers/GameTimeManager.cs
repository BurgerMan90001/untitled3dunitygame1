using UnityEngine;
//TODO FIX GAMETIME DAYNIGHT CYCLE
public class GameTimeManager : MonoBehaviour, IGameTimeManager
{
    private GameTimeData _gameTimeData;
    private GameTimeEvents _gameTimeEvents;

    [Header("Lights")]
    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;

    public void Initialise(GameTimeEvents gameTimeEvents)
    {
        _gameTimeEvents = gameTimeEvents;
    }
    private void Awake()
    {
        _gameTimeData = GetComponent<GameTimeData>();
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
        //    _gameTimeData.DayNightCycle.UpdateSun(_sun, _gameTimeData.Hour);
    }




}
