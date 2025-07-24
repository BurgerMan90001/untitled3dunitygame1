using MyBox;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameDebug : MonoBehaviour
{

    private static GameDebug _Instance;
    public static GameDebug Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<GameDebug>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    [Header("Debug Scene")]
    [SerializeField] private bool _enableDebuging;


    [Header("Stuff")]


    [Header("Debug Settings")]
    [ConditionalField(nameof(_enableDebuging))][SerializeField] private bool _lockCursor = true;

    [Header("Debug Interface")]
    [ConditionalField(nameof(_enableDebuging))][SerializeField] private bool _showDebugInterface;
    [ConditionalField(nameof(_enableDebuging))][SerializeField] private UserInterfaceType _debugInterface;




    /*
    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPoint = new Vector3(-20.49013f, 71f, -32.76805f);


    [Header("Debug Info")]
    public List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private double _buttonDurationThreshold = 0.30d; // Threshold for button hold duration
    private bool buttonHeld = false;
    private bool show = false;
    */


    private void Awake()
    {


    }
    private void Start()
    {
        if (_enableDebuging)
        {
            if (_lockCursor)
            {
                GameCursor.Lock();
            }

        }


        //    ActivatePrefabs(true);
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }



    public void Debug2(InputAction.CallbackContext ctx)
    {

    }


}
