
using System.Collections.Generic;
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
    [SerializeField] private bool _debugScene = true;

    [Header("Debug Settings")]
    [SerializeField] private bool _lockCursor = true;

    [Header("Debug Interface")]
    [SerializeField] private UserInterfaceType _loadedInterface;
    [SerializeField] private bool _showInterface;



    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPoint = new Vector3(-20.49013f, 71f, -32.76805f);


    [Header("Debug Info")]
    public List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private double _buttonDurationThreshold = 0.30d; // Threshold for button hold duration
    private bool buttonHeld = false;
    private bool show = false;


    private void Awake()
    {

        if (_lockCursor)
        {
            GameCursor.Lock();


        }
    }
    private void Start()
    {
        if (_debugScene)
        {


        }
        //    ActivatePrefabs(true);
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    /*

    private void InstantiatePrefabs(bool active, bool setEnabled)
    {
        if (active)
        {
            foreach (var prefab in _instantiatedPrefabsOnPlaySceneWithoutLoading)
            {

                var gameObject = Instantiate(prefab);
                if (gameObject.TryGetComponent(out UserInterface userInterface))
                {
                    userInterface.InitalShownUserInterface = _loadedInterface;

                }
                gameObject.SetActive(setEnabled);

                instantiatedPrefabs.Add(gameObject);
            }
        }
    }
    private void ActivatePrefabs(bool active)
    {
        foreach (var prefab in instantiatedPrefabs)
        {
            prefab.SetActive(true);
        }
    }
    */
    public void DropItem(InputAction.CallbackContext ctx) //int itemIndex
    {
        if (ctx.started)
        {
            buttonHeld = true;
        }
        else if (ctx.canceled)
        {
            buttonHeld = false;
        }


    }

    public void Debug2(InputAction.CallbackContext ctx)
    {
        if (show)
        {

        }
        else
        {

        }
    }
    private void Update()
    {

    }

}
