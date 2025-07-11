
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
    [SerializeField] private DynamicInventory _dynamicInventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    
    [SerializeField] private DataPersistenceManager _dataPersistenceManager;



    [Header("Inventory Settings")]
    [SerializeField] private bool _pauseOnInventory = true; // pause when the inventory is opened

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;
    [SerializeField] private bool _showLoadingResults = false;

    [Header("First Shown Interface")]
    [SerializeField] private UserInterfaces _initalShownUserInterface;


    [Header("Settings")]
    [SerializeField] private AssetLabelReference _sceneLabelReference;
 //   [SerializeField] private UserInterfaces[] _userInterfacesToBeLoaded;
    [SerializeField] private Vector2 TooltipSize = new Vector2(200, 200); // default size for the tooltip, can be changed later


    

    private VisualElement _root;

    private UXMLFileHandler _uxmlFileHandler;
    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality
    
    public UserInterfaces ShownUserInterface { get; private set; }

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

        _mainMenu = new MainMenu(_root, _dataPersistenceManager, _uxmlFileHandler);

        _uiDialogue = new UI_Dialogue();
        _uiInventory = new UI_Inventory(_dynamicInventory, _root);

    }
    


    private async void OnEnable() // the userinterface game object will be enabled by the main manager
    {
        // load the user interfaces asynchronously. visual element configuration is done after this.
        await _uxmlFileHandler.LoadInterfacesAsync(_showLoadingResults); 

        
        _uiInventory.RegisterEvents(); // register the tooltip events for the inventory
        _mainMenu.RegisterEvents();

        SceneLoadingManager.OnSceneLoaded += ToggleUserInterface;
        
      //  DialogueEvents.OnEnterDialogue += ToggleUserInterface;


        _dynamicInventory.OnInventoryChanged += _uiInventory.AssignItemInstancesToVisualElements; // whenever the inventory changes, update the inventory ui


        ToggleUserInterface(_initalShownUserInterface);
        
    }
    private void OnDisable()
    {
        
    }
    
    private void OnDestroy()
    {
        
        _uxmlFileHandler?.ReleaseInterfaces();
        _dynamicInventory.OnInventoryChanged -= _uiInventory.AssignItemInstancesToVisualElements;
        _uiInventory?.UnregisterEvents();
    }
    private void ToggleDialogueInterface()
    {
        
    }
    public void ToggleUserInterface(UserInterfaces userInterface)
    {
        
        VisualElement elementToBeShown = _uxmlFileHandler.GetVisualElement(userInterface);

        
        VisualElement toggledUserInterfaceElement = _uxmlFileHandler.GetVisualElement(ShownUserInterface);

        if (toggledUserInterfaceElement == null)
        {
 
            elementToBeShown.style.display = DisplayStyle.Flex;
            ShownUserInterface = userInterface;
        } else
        {
            toggledUserInterfaceElement.style.display = DisplayStyle.None;
            elementToBeShown.style.display = DisplayStyle.Flex;
            ShownUserInterface = userInterface;
        }
        
      

    }
    public void ToggleInventoryInterface(bool toggled)
    {
        ToggleUserInterface(UserInterfaces.Inventory);
        /*
        if (toggled)
        {
            
            ToggleUserInterface(UserInterfaces.Inventory);
        }
        else
        {
            
            ToggleUserInterface(UserInterfaces.HUD);
        }
        */
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
public static class ToggleUserInterface
{
    public static void Toggle(UserInterfaces userInterface)
    {

    }
}

public enum UserInterfaces
{
    None,
    HUD,
    Loading,
    Inventory,
    Dialogue,
    Settings,
    PauseMenu,
    MainMenu,
    SaveSlotsMenu,

}

public class UserInterfaceType
{

    public string UserInterfaceName;

    public UserInterfaceType(string userInterfaceName)
    {
        UserInterfaceName = userInterfaceName;
    }
}
