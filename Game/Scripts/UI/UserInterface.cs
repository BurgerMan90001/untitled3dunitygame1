
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
public class UserInterface : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InputManager _inputManager;

    

    [Header("Data")]
    [SerializeField] private List<Data> _data;
    [SerializeField] private DynamicInventory _dynamicInventory; // the dynamic inventory scriptable object that will be used to manage the inventory
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private DataPersistenceData _dataPersistenceData;
    [SerializeField] private DialogueData _dialogueData;


    [Header("Inventory Settings")]
    [SerializeField] private bool _pauseOnInventory = true; // pause when the inventory is opened

    [Header("UXML Loader Settings")]
    [SerializeField] private AssetLabelReference _uxmlAssetLabelReference;
    
    [Header("First Shown Interface")]
    public UserInterfaces InitalShownUserInterface;


    [Header("Settings")]
    [SerializeField] private AssetLabelReference _sceneLabelReference;

    private UIDocument _uiDocument;
    private VisualElement _root; // the base that will uxmls be added on to


    private UXMLFileHandler _uxmlFileHandler;

    private UserInterfaceToggler _interfaceToggler;

    private UI_Dialogue _uiDialogue; // handles dialogue related user interface functionality
    private UI_Inventory _uiInventory; // handles inventory related user interface functionality

    private UI_MainMenu _uiMainMenu;
    private UI_SaveSlotsMenu _uiSaveSlotsMenu;


    private List<IUserInterface> _userInterfaces = new List<IUserInterface>();


  //  private static UserInterface _instance;

    private void Awake()
    {

        /*
        if (this != null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Debug.LogWarning("There was another UserInterface instance in the scene. Destroying duplicate.");
            Destroy(gameObject);
        }
        */

        _uiDocument = GetComponent<UIDocument>();

        _root = _uiDocument.rootVisualElement;
        _root.style.flexGrow = 1;

        
        
        _uxmlFileHandler = new UXMLFileHandler(_root, _uxmlAssetLabelReference);
        _interfaceToggler = new UserInterfaceToggler(_uxmlFileHandler);

        _uiMainMenu = new UI_MainMenu(_dataPersistenceData, _interfaceToggler);
        _uiSaveSlotsMenu = new UI_SaveSlotsMenu(_dataPersistenceData, _interfaceToggler);
        _uiDialogue = new UI_Dialogue(_userInterfaceData, _dialogueData);
        _uiInventory = new UI_Inventory(_dynamicInventory);

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
            

            SceneLoadingManager.OnSceneLoaded += _interfaceToggler.ToggleUserInterface;
            _userInterfaceData.OnToggleUserInterface += _interfaceToggler.ToggleUserInterface;
            _dynamicInventory.OnInventoryChanged += _uiInventory.UpdateInterface; // whenever the inventory changes, update the inventory ui

            _interfaceToggler.ToggleUserInterface(InitalShownUserInterface);

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

        UnregisterAllInterfaces();


        _uxmlFileHandler?.ReleaseInterfaces();
        
    }
    
    private void OnDestroy()
    {

        
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




/*
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
    */