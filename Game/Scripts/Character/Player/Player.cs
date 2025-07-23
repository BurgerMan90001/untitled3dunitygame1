
using UnityEngine;
// TODO POSSIBLY NOT MAKE SINGLE TON FOR MULTIPLE PEAOPLE
public class Player : MonoBehaviour
{
    /*
    private static Player _Instance;
    public static Player Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<Player>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
    */
    [Header("Dependancies")]
    [SerializeField] private MovementStateManager _movementStateManager;

    [Header("Settings")]
    [SerializeField] private bool _enableRigidbodyTrigger;

    [Header("Debug")]
    [SerializeField] private bool _debugMode;

    private RigidbodyTrigger _rigidbodyTrigger;

    private Rigidbody _rigidBody;


    private void Awake()
    {


        _rigidBody = GetComponent<Rigidbody>();


        _rigidbodyTrigger = new RigidbodyTrigger(_rigidBody, _movementStateManager);

    }
    private void Start()
    {
        if (!_enableRigidbodyTrigger)
        {

        }
    }
    private void OnEnable()
    {
        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;
    }


    private void OnDisable()
    {
        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(SceneLoadingSettings settings)
    {
        transform.position = settings.PlayerSpawnPoint;
        if (_debugMode)
        {

            Debug.Log($" Spawned at : {settings.PlayerSpawnPoint}");

        }
    }

    private void OnTriggerStay(Collider other)
    {
        _rigidbodyTrigger.OnTriggerStay(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        _rigidbodyTrigger.OnTriggerEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        _rigidbodyTrigger.OnTriggerExit(other);

    }
}


