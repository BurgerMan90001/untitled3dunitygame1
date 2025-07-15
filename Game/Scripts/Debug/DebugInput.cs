
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInput : MonoBehaviour, ISingleton
{
    [Header("Dependancies")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject prefab;
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [Header("UserInteface Settings")]
    

    [Header("Settings")]


    [Header("Debug Scene")]
    [SerializeField] private bool _debugScene = true;
    [SerializeField] private bool _lockCursor = true;

    [Header("Interface")]
    [SerializeField] private UserInterfaces _loadedInterface;
    [SerializeField] private bool _showInterface;


    [Header("Managers")]
    [SerializeField] private bool _instantiatedPrefabs;
    [SerializeField] private bool _setPrefabsActive;
    [SerializeField] private List<GameObject> _instantiatedPrefabsOnPlaySceneWithoutLoading;

    [Header("Spawn")]
    [SerializeField] private Vector3 spawnPoint = new Vector3(-20.49013f, 71f, -32.76805f);


    [Header("Debug Info")]
    public List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private double _buttonDurationThreshold = 0.30d; // Threshold for button hold duration
    private bool buttonHeld = false;
    private bool show = false;

    public static DebugInput Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Debug.LogWarning("There is another DebugInput in scene. Destroying duplicate.");
        }
        if (_lockCursor)
        {
            LockCursor(true);



            //     LoadDebugInterface(_loadedInterface);
        }
    }
    private void Start()
    {

    //    ActivatePrefabs(true);
    }
    private void OnEnable()
    {
        if (_debugScene)
        {
            if (_showInterface)
            {
                _userInterfaceData.ToggleUserInterface(_loadedInterface, true);
            } else
            {
                Debug.LogWarning("The user inteface will not be toggled. Enable showInterface.");
            }
            InstantiatePrefabs(_instantiatedPrefabs, _setPrefabsActive);
        }
    }
    private void OnDisable()
    {
        
    }
    private void LockCursor(bool active)
    {
        if (active) // lock cursor
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else // unlock cursor 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
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
    private void LoadDebugInterface(UserInterfaces userInterface)
    {
        if (gameObject.TryGetComponent(out UserInterface component))
        {
            component.InitalShownUserInterface = _loadedInterface;
        }
    }
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
        
        //    GameObject droppedItem = new GameObject();// Creates a new object and gives it the item data
        //    droppedItem.AddComponent<Rigidbody>();
        //    droppedItem.AddComponent<InstanceItemContainer>();

        // Removes the item from the inventory
     //   inventory.items.RemoveAt(itemIndex);

        // Updates the inventory again
    //    UpdateInventory();
    }

    public void Debug2(InputAction.CallbackContext ctx)
    {
        if (show)
        {

        } else
        {

        }
    }
    private void Update()
    {
        if (buttonHeld)
        {
            GameObject spawnedObject = Instantiate(prefab, spawnPoint, Quaternion.identity);
            
            
        }
    }
    
}
