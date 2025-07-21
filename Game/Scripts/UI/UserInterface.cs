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
public class UserInterface : MonoBehaviour, ISingleton
{
    [Header("Data")]
    [SerializeField] private Inventory _inventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private DataPersistenceData _dataPersistenceData;
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private InputData _inputData;

    [SerializeField] private CombatData _combatData;


    [Header("First Shown Interface")]
    public UserInterfaceType InitalShownUserInterface;

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;

    [Header("Debug")]
    [SerializeField] private bool _showHoveredOnElement = false;


    private VisualElement _currentElement;

    private UIDocument _uiDocument;
    public VisualElement Root { get; private set; }// the base visual element that will uxmls be added on to


    //  private UXMLFileHandler _uxmlFileHandler;

    // private UserInterfaceToggler _interfaceToggler;

    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private List<IUserInterface> _userInterfaces = new List<IUserInterface>();


    public static UserInterface Instance;

    private UxmlFileHandler _uxmlFileHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Debug.LogWarning("There was another UserInterface instance in the scene. Destroying duplicate.");

            Destroy(gameObject);
        }


        _uiDocument = GetComponent<UIDocument>();

        Root = _uiDocument.rootVisualElement;
        Root.style.flexGrow = 1;



        _uxmlFileHandler = new UxmlFileHandler(Root, _uxmlAssetLabelReference, _userInterfaceData);
        //    _interfaceToggler = new UserInterfaceToggler(_uxmlFileHandler);

        _uiMainMenu = new UI_MainMenu(_dataPersistenceData, _userInterfaceData);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceData, _userInterfaceData);
        _uiDialogue = new UI_Dialogue(_userInterfaceData, _dialogueData);
        _uiInventory = new UI_Inventory(_inventory);

        _userInterfaces.Add(_uiMainMenu);
        _userInterfaces.Add(_uiSaveSlotsMenu);
        _userInterfaces.Add(_uiDialogue);
        _userInterfaces.Add(_uiInventory);

        DontDestroyOnLoad(this);

    }



    private async void OnEnable() // the userinterface game object will be enabled by the main manager
    {

        await _uxmlFileHandler.LoadInterfacesAsync(); // load the user interfaces asynchronously. visual element configuration is done after this.


        QueryAllElements();
        RegisterAllInterfaces();

        //    _root.RegisterCallback<MouseMoveEvent>(OnMouseMove);

        _uiInventory.UpdateInterface();

        //    _userInterfaceData.OnToggleUserInterface += _interfaceToggler.SwitchToUserInterface;


        //   _inventory.OnInventoryChanged += _uiInventory.UpdateInterface; // whenever the inventory changes, update the inventory ui
        //    _interfaceToggler.SwtichToUserInterface(InitalShownUserInterface);



    }
    private void OnDisable()
    {

        //   _userInterfaceData.OnToggleUserInterface -= _interfaceToggler.SwitchToUserInterface;
        _inventory.OnInventoryChanged -= _uiInventory.UpdateInterface;

        //   _root.UnregisterCallback<MouseMoveEvent>(OnMouseMove);

        UnregisterAllInterfaces();


        _uxmlFileHandler?.ReleaseInterfaces();

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



