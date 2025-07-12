
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

#region
/// <summary>
/// <br> All user interfaces are children of this class' game object. </br>
/// <br> Enabled by Main Manager. </br>
/// </summary>
#endregion
public class UserInterface : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private UIDocument _UIDocument; // the base UIDocument that will uxmls be added on to
    [SerializeField] private InputManager _inputManager;


    [Header("Data")]
    [SerializeField] private DynamicInventory _dynamicInventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private DataPersistenceData _dataPersistenceData;


    [Header("Inventory Settings")]
    [SerializeField] private bool _pauseOnInventory = true; // pause when the inventory is opened

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;
    
    [Header("First Shown Interface")]
    public UserInterfaces InitalShownUserInterface;


    [Header("Settings")]
    [SerializeField] private AssetLabelReference _sceneLabelReference;
    [SerializeField] private Vector2 TooltipSize = new Vector2(200, 200); // default size for the tooltip, can be changed later

    private VisualElement _root;

    private UXMLFileHandler _uxmlFileHandler;
    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality
    

    private UIDocument _uiDocument;


    private MainMenu _mainMenu;

    private static UserInterface _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Debug.LogWarning("There was another UserInterface instance in the scene. Destroying duplicate.");
            Destroy(gameObject);
        }


        _root = _UIDocument.rootVisualElement;
        _root.style.flexGrow = 1;

        
        _uxmlFileHandler = new UXMLFileHandler(_root, _uxmlAssetLabelReference);

        _mainMenu = new MainMenu(_root, _uxmlFileHandler, _dataPersistenceData);

        _uiDialogue = new UI_Dialogue();
        _uiInventory = new UI_Inventory(_dynamicInventory, _root);

    }
    


    private async void OnEnable() // the userinterface game object will be enabled by the main manager
    {
        // load the user interfaces asynchronously. visual element configuration is done after this.
        await _uxmlFileHandler.LoadInterfacesAsync(); 

        
        _uiInventory.RegisterEvents(); // register the tooltip events for the inventory
        _mainMenu.RegisterEvents();

        SceneLoadingManager.OnSceneLoaded += ToggleUserInterface;

        _userInterfaceData.OnToggleUserInterface += ToggleUserInterface;

        _dynamicInventory.OnInventoryChanged += _uiInventory.AssignItemInstancesToVisualElements; // whenever the inventory changes, update the inventory ui

        ToggleUserInterface(InitalShownUserInterface);
        
        
    }
    private void OnDisable()
    {
        SceneLoadingManager.OnSceneLoaded -= ToggleUserInterface;

        _userInterfaceData.OnToggleUserInterface -= ToggleUserInterface;

        _uxmlFileHandler?.ReleaseInterfaces();
        _dynamicInventory.OnInventoryChanged -= _uiInventory.AssignItemInstancesToVisualElements;
        _uiInventory?.UnregisterEvents();
    }
    
    private void OnDestroy()
    {

        
    }
    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the UserInterfaces value. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="inputActionMap"> Set as null to leave the action map unchanged </param>
    /// Set as null to leave the action map unchanged
    #endregion
    private void ToggleUserInterface(UserInterfaces userInterface)
    {
        VisualElement elementToBeShown = _uxmlFileHandler.UserInterfaceElements[userInterface];

        if (elementToBeShown.style.display == DisplayStyle.Flex)
        {
            elementToBeShown.style.display = DisplayStyle.None;

        }
        else
        {
            elementToBeShown.style.display = DisplayStyle.Flex;
        }
    }
    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the UserInterfaces value.</br>
    /// <br> Also switches to the inputActionMap set. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="inputActionMap"></param>
    #endregion
    private void ToggleUserInterface(UserInterfaces userInterface, string inputActionMap)
    {
        
        ToggleUserInterface(userInterface);
        
        
        if (inputActionMap != null)
        {
            _inputManager.SwitchToActionMap(inputActionMap);
            
        } else
        {
            Debug.LogWarning("The switched to inputActionMap is null.");
        }


    }
    
    public void TogglePauseGame(bool pauseGame)
    {
        if (pauseGame) //then unPause game and bring up HUD
        {
            Time.timeScale = 1.0f;
            pauseGame = false; //unPause
            AudioListener.pause = true;

            //  auidoSource.ignoreListenerPause = true; to ignore
        }
        else // then pause game and bring up inventory
        {
            Time.timeScale = 0f;
            pauseGame = true;
            AudioListener.pause = false;

        }
    }

}




