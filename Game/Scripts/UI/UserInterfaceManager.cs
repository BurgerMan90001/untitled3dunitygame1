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
public class UserInterfaceManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Inventory _inventory; // the dynamic inventory scriptable object that will be used to manage the inventory
                                                   //    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;
    [Header("Events")]
    [SerializeField] private DataPersistenceEvents _dataPersistenceEvents;
    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;
    [SerializeField] private DialogueEvents _dialogueEvents;


    [Header("First Shown Interface")]
    public UserInterfaceType InitalShownUserInterface;

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;

    [Header("Debug")]
    [SerializeField] private bool _debugMode;
    [SerializeField] private bool _showHoveredOnElement = false;

    private VisualElement _currentElement;

    private UserInterfaceToggler _userInterfaceToggler;

    private UIDocument _uiDocument;
    public VisualElement Root { get; private set; } // the base visual element that will uxmls be added on to


    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private readonly List<IUserInterface> _userInterfaces = new List<IUserInterface>();

    private UxmlFileHandler _uxmlFileHandler;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();

        _uiMainMenu = new UI_MainMenu(_dataPersistenceEvents, _userInterfaceEvents);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceEvents, _userInterfaceEvents);
        _uiDialogue = new UI_Dialogue(_userInterfaceEvents, _dialogueEvents);
        _uiInventory = new UI_Inventory(_inventory);

        _userInterfaces.Add(_uiMainMenu);
        _userInterfaces.Add(_uiSaveSlotsMenu);
        _userInterfaces.Add(_uiDialogue);
        _userInterfaces.Add(_uiInventory);


        Root = _uiDocument.rootVisualElement;
        Root.style.flexGrow = 1;


        _userInterfaceToggler = GetComponent<UserInterfaceToggler>();

        _uxmlFileHandler = new UxmlFileHandler(Root);

        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        await _uxmlFileHandler.LoadInterfacesAsync(_uxmlAssetLabelReference, _userInterfaceToggler.UserInterfaceElements); // load the user interfaces asynchronously. visual element configuration is done after this.


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

    private void OnDestroy()
    {
        _inventory.OnInventoryChanged -= OnInventoryChanged;

        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;

        UnregisterAllInterfaces();

        _uxmlFileHandler?.ReleaseInterfaces();


    }

    private void OnSceneLoadComplete(SceneLoadingSettings settings)
    {
        _userInterfaceToggler.SwitchToUserInterface(settings.UserInterface);

    }





    private void OnInventoryChanged()
    {
        _uiInventory.UpdateInterface();
    }
    private void ShowInitialInterface()
    {
        _userInterfaceToggler.SwitchToUserInterface(InitalShownUserInterface);

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


        _userInterfaces.ForEach(ui => ui.QueryElements(Root));
    }
    private void RegisterAllInterfaces()
    {


        _userInterfaces.ForEach(ui => ui.Register(Root));

    }
    private void UnregisterAllInterfaces()
    {


        _userInterfaces.ForEach(ui => ui?.Unregister());
    }

}



