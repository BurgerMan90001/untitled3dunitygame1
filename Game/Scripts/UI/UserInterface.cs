
using System;
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
public class UserInterface : MonoBehaviour, ISingleton
{
    [Header("Dependencies")]
 

    private VisualElement currentElement;


    [Header("Data")]
    //[SerializeField] private List<Data> _data;
    [SerializeField] private DynamicInventory _dynamicInventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private DataPersistenceData _dataPersistenceData;
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private InputData _inputData;


    [Header("Inventory Settings")]
    [SerializeField] private bool _pauseOnInventory = true; // pause when the inventory is opened

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;
    
    [Header("First Shown Interface")]
    public UserInterfaceType InitalShownUserInterface;

    [Header("Settings")]
    [SerializeField] private AssetLabelReference _sceneLabelReference;

    [Header("Debug")]
    [SerializeField] private bool _showHoveredOnElement = false;


    private UIDocument _uiDocument;
    private VisualElement _root; // the base that will uxmls be added on to


    private UXMLFileHandler _uxmlFileHandler;

    private UserInterfaceToggler _interfaceToggler;

    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private List<IUserInterface> _userInterfaces = new List<IUserInterface>();


    public static UserInterface Instance;

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

        _root = _uiDocument.rootVisualElement;
        _root.style.flexGrow = 1;
        
        

        _uxmlFileHandler = new UXMLFileHandler(_root, _uxmlAssetLabelReference);
        _interfaceToggler = new UserInterfaceToggler(_uxmlFileHandler);

        _uiMainMenu = new UI_MainMenu(_dataPersistenceData, _interfaceToggler);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceData, _interfaceToggler);
        _uiDialogue = new UI_Dialogue(_userInterfaceData, _dialogueData);
        _uiInventory = new UI_Inventory(_dynamicInventory, _dynamicInventory.OnInventoryChanged);

        _userInterfaces.Add(_uiMainMenu);
        _userInterfaces.Add(_uiSaveSlotsMenu);
        _userInterfaces.Add(_uiDialogue);
        _userInterfaces.Add(_uiInventory);

        DontDestroyOnLoad(this);

    }
    


    private async void OnEnable() // the userinterface game object will be enabled by the main manager
    {
        try
        {
            await _uxmlFileHandler.LoadInterfacesAsync(); // load the user interfaces asynchronously. visual element configuration is done after this.


            QueryAllElements();


            RegisterAllInterfaces();
            
            _root.RegisterCallback<MouseMoveEvent>(OnMouseMove);
            /*
            _interfaceToggler.Register(SceneLoadingManager.OnSceneLoaded);
            _interfaceToggler.Register(_userInterfaceData.OnToggleUserInterface);
            */
            
            

            SceneLoadingManager.OnSceneLoaded += _interfaceToggler.ToggleUserInterface;
            _userInterfaceData.OnToggleUserInterface += _interfaceToggler.ToggleUserInterface;
            _dynamicInventory.OnInventoryChanged += _uiInventory.UpdateInterface; // whenever the inventory changes, update the inventory ui
            
            _interfaceToggler.ToggleUserInterface(InitalShownUserInterface, true);

        } catch (Exception e)
        {
            Debug.LogError($"UI initialization failed: {e.Message}");
        }
        
    }
    private void OnDisable()
    {
        
        SceneLoadingManager.OnSceneLoaded -= _interfaceToggler.ToggleUserInterface;
        _userInterfaceData.OnToggleUserInterface -= _interfaceToggler.ToggleUserInterface;
        _dynamicInventory.OnInventoryChanged -= _uiInventory.UpdateInterface;
        /*
        _interfaceToggler.Unregister(SceneLoadingManager.OnSceneLoaded);
        _interfaceToggler.Unregister(_userInterfaceData.OnToggleUserInterface);
        */
        _root.UnregisterCallback<MouseMoveEvent>(OnMouseMove);
        
        UnregisterAllInterfaces();


        _uxmlFileHandler?.ReleaseInterfaces();
        
    }
    
    private void OnDestroy()
    {

        
    }
    private void OnMouseMove(MouseMoveEvent evt)
    {
        if (_showHoveredOnElement) {

        
            var newElement = evt.target as VisualElement;

            if (newElement != currentElement)
            {
                if (currentElement != null)
                {
                    Debug.Log($"Left: {currentElement.name}");
                }
                    

                currentElement = newElement;


                if (currentElement != null) 
                {
                    Debug.Log($"Entered: {currentElement.name}");
                }
            }

                    
        }
    }

    private void QueryAllElements()
    {
        _userInterfaces.ForEach(ui => ui.QueryElements(_root));
    }
    private void RegisterAllInterfaces()
    {
        _userInterfaces.ForEach(ui => ui.Register(_root));
        
    }
    private void UnregisterAllInterfaces()
    {
        _userInterfaces.ForEach(ui => ui?.Unregister());
    }
    

}



