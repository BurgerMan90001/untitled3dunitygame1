
using UnityEngine;
// TODO POSSIBLY NOT MAKE SINGLE TON FOR MULTIPLE PEAOPLE
public class Player : MonoBehaviour, ISingleton
{
    [Header("Dependancies")]
    [SerializeField] private MovementStateManager _movementStateManager;

    [Header("Settings")]
    [SerializeField] private bool _enableRigidbodyTrigger;

    [Header("Debug")]
    [SerializeField] private bool _debugMode;

    private RigidbodyTrigger _rigidbodyTrigger;

    private Rigidbody _rigidBody;

    private static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Debug.LogWarning("There was another player instance, destroying duplicate.");
            Destroy(gameObject);
        }

        _rigidBody = GetComponent<Rigidbody>();


        _rigidbodyTrigger = new RigidbodyTrigger(_rigidBody, _movementStateManager);

        DontDestroyOnLoad(gameObject);
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


