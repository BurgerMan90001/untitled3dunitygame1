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
public class UserInterface : Manager
{
    [Header("Data")]
    [SerializeField] private Inventory _inventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;


    [SerializeField] private GameInput _gameInput;
    [SerializeField] private CombatData _combatData;


    [Header("Events")]
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private DataPersistenceEvents _dataPersistenceEvents;

    [Header("First Shown Interface")]
    public UserInterfaceType InitalShownUserInterface;

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;

    [Header("Debug")]
    [SerializeField] private bool _debugMode;
    [SerializeField] private bool _showHoveredOnElement = false;



    private VisualElement _currentElement;

    private UserInterfaceData _userInterfaceData;

    private UIDocument _uiDocument;
    public VisualElement Root { get; private set; }// the base visual element that will uxmls be added on to




    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private readonly List<IUserInterface> _userInterfaces = new List<IUserInterface>();

    private UxmlFileHandler _uxmlFileHandler;

    /*
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
    */
    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();



        Root = _uiDocument.rootVisualElement;
        Root.style.flexGrow = 1;


        _userInterfaceData = new UserInterfaceData();

        _uxmlFileHandler = new UxmlFileHandler(Root);

        _uiMainMenu = new UI_MainMenu(_dataPersistenceEvents, _userInterfaceEvents);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceEvents, _userInterfaceEvents);
        _uiDialogue = new UI_Dialogue(_userInterfaceEvents, _dialogueEvents);
        _uiInventory = new UI_Inventory(_inventory);

        _userInterfaces.Add(_uiMainMenu);
        _userInterfaces.Add(_uiSaveSlotsMenu);
        _userInterfaces.Add(_uiDialogue);
        _userInterfaces.Add(_uiInventory);


    }

    private async void Start()
    {

        Debug.Log("START");
        await _uxmlFileHandler.LoadInterfacesAsync(_uxmlAssetLabelReference, _userInterfaceData.UserInterfaceElements); // load the user interfaces asynchronously. visual element configuration is done after this.

        _userInterfaceData.Test = 123123;

        QueryAllElements();
        RegisterAllInterfaces();

        _uiInventory.UpdateInterface();

        ShowInitialInterface();

        Debug.LogError(_userInterfaceData.Test);

    }

    private void OnEnable() // the userinterface game object will be enabled by the main manager
    {
        _inventory.OnInventoryChanged += OnInventoryChanged;

        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;

        _userInterfaceEvents.OnShowInterface += ShowInterface;
        _userInterfaceEvents.OnHideRecentInterface += HideRecentInterface;


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
        SwitchToUserInterface(settings.UserInterface);

    }
    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {
        if (_userInterfaceData.ShownInterfaces == null)
        {
            Debug.LogError("Shown interfaces is null.");
            return;
        }
        HideRecentInterface();
        ShowInterface(userInterface);

    }



    public void ShowInterface(UserInterfaceType userInterface)
    {
        if (userInterface == UserInterfaceType.None)
        {
            Debug.LogWarning($"Can't show {UserInterfaceType.None}");
            return;
        }

        _userInterfaceData.ShownInterfaces.Push(userInterface);
        VisualElement elementToBeShown = _userInterfaceData.UserInterfaceElements[userInterface];
        elementToBeShown.style.display = DisplayStyle.Flex;

        //  ShownInterfacesStack = ShownInterfaces.ToList();


    }
    /// <summary>
    /// <br> Hides the most recently shown interface. Does nothing if there is none. </br>
    /// </summary>
    public void HideRecentInterface()
    {

        if (_userInterfaceData.ShownInterfaces.TryPop(out UserInterfaceType userInterface))
        {
            VisualElement elementToBeHiden = _userInterfaceData.UserInterfaceElements[userInterface];
            elementToBeHiden.style.display = DisplayStyle.None;

            //   ShownInterfacesStack = ShownInterfaces.ToList<UserInterfaceType>();
        }

    }



    private void OnInventoryChanged()
    {
        _uiInventory.UpdateInterface();
    }
    private void ShowInitialInterface()
    {
        SwitchToUserInterface(InitalShownUserInterface);

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

    public override void Instantiate()
    {
        Debug.Log("User interface manager instantiated.");
        throw new System.NotImplementedException();
    }
}



