using UnityEngine;

public class GameTimeManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private GameTimeData _gameTimeData;


    [SerializeField] private Light _sun;
    [SerializeField] private Light _moon;
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }
}
