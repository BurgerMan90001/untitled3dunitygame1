using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, ISingleton
{
    [Header("Dependancies")]
    [SerializeField] private MovementStateManager _movementStateManager;

    private RigidbodyTrigger _rigidbodyTrigger;

    private Rigidbody _rigidBody;

    private static Player Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        } else
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
        if (!SceneManager.GetActiveScene().name.Equals("Main Game"))
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        SceneLoadingManager.OnSceneLoaded += SetupPlayer;
    }

  
    private void OnDisable()
    {
        SceneLoadingManager.OnSceneLoaded -= SetupPlayer;
    }



    private void SetupPlayer(UserInterfaceType _, bool _1)
    {
        gameObject.transform.position = SceneLoadingManager.SpawnPoint;
        Debug.Log(gameObject.transform.position);
        /*
        if (scene.name == "Main Game")
        {
            gameObject.SetActive(true);
        }
       */
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


