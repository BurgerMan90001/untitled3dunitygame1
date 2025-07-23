using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;
// TODO SHORTEN AND OPTIMIZE EVERYTHING
#region
/// <summary>
/// <br> All user interfaces are children of this class' game object. </br>
/// <br> Enabled by Main Manager. </br>
/// </summary>
#endregion
public class UserInterface : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Inventory _inventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private DataPersistenceData _dataPersistenceData;

    [SerializeField] private InputData _inputData;
    [SerializeField] private CombatData _combatData;


    [Header("Events")]
    [SerializeField] private DialogueEvents _dialogueEvents;

    [Header("First Shown Interface")]
    public UserInterfaceType InitalShownUserInterface;

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;

    [Header("Debug")]
    [SerializeField] private bool _debugMode;
    [SerializeField] private bool _showHoveredOnElement = false;


    private VisualElement _currentElement;

    private UIDocument _uiDocument;
    public VisualElement Root { get; private set; }// the base visual element that will uxmls be added on to


    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private List<IUserInterface> _userInterfaces = new List<IUserInterface>();

    private UxmlFileHandler _uxmlFileHandler;

    private static UserInterface _Instance;
    public static UserInterface Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<UserInterface>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    private void Awake()
    {



        _uiDocument = GetComponent<UIDocument>();

        Root = _uiDocument.rootVisualElement;
        Root.style.flexGrow = 1;


        _uxmlFileHandler = new UxmlFileHandler(Root);

        _uiMainMenu = new UI_MainMenu(_dataPersistenceData, _userInterfaceData);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceData, _userInterfaceData);
        _uiDialogue = new UI_Dialogue(_userInterfaceData, _dialogueEvents);
        _uiInventory = new UI_Inventory(_inventory);

        _userInterfaces.Add(_uiMainMenu);
        _userInterfaces.Add(_uiSaveSlotsMenu);
        _userInterfaces.Add(_uiDialogue);
        _userInterfaces.Add(_uiInventory);


    }

    private async void Start()
    {
        Debug.Log("START");
        _userInterfaceData.UserInterfaceElements = await _uxmlFileHandler.LoadInterfacesAsync(_uxmlAssetLabelReference); // load the user interfaces asynchronously. visual element configuration is done after this.

        QueryAllElements();
        RegisterAllInterfaces();

        _uiInventory.UpdateInterface();

        ShowInitialInterface();
    }

    private void OnEnable() // the userinterface game object will be enabled by the main manager
    {
        _inventory.OnInventoryChanged += OnInventoryChanged;


        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;


    }

    private void OnDisable()
    {


    }
    private void OnDestroy()
    {
        _inventory.OnInventoryChanged -= OnInventoryChanged;

        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;

        UnregisterAllInterfaces();

        _uxmlFileHandler?.ReleaseInterfaces();


    }

    private void OnSceneLoadComplete(SceneLoadingSettings settings)
    {
        _userInterfaceData.SwitchToUserInterface(settings.UserInterface);

    }

    private void OnInventoryChanged()
    {
        _uiInventory.UpdateInterface();
    }
    private void ShowInitialInterface()
    {
        _userInterfaceData.SwitchToUserInterface(InitalShownUserInterface);

    }
    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (_showHoveredOnElement)
        {
            ShowHoveredOnElement(evt);
        }
    }
    private void ShowHoveredOnElement(MouseMoveEvent evt)
    {
        var newElement = evt.target as VisualElement;

        if (newElement != _currentElement)
        {
            if (_currentElement != null)
            {
                Debug.Log($"Left: {_currentElement.name}");
            }
            _currentElement = newElement;


            if (_currentElement != null)
            {
                Debug.Log($"Entered: {_currentElement.name}");
            }
        }

    }

    private void QueryAllElements()
    {
        if (_debugMode)
        {
            Debug.Log("QUERAY");
        }

        _userInterfaces.ForEach(ui => ui.QueryElements(Root));
    }
    private void RegisterAllInterfaces()
    {
        if (_debugMode)
        {
            Debug.Log("REGISTER");
        }

        _userInterfaces.ForEach(ui => ui.Register(Root));

    }
    private void UnregisterAllInterfaces()
    {
        if (_debugMode)
        {
            Debug.Log("UNREGISTER");
        }

        _userInterfaces.ForEach(ui => ui?.Unregister());
    }


}



