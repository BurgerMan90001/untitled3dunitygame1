using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : Menu
{

    private Button _buttonNewGame;
    private string _buttonNewGameName = "Button_NewGame";

    private Button _buttonLoadGame;
    private string _buttonLoadGameName = "Button_LoadGame";

    private Button _buttonContinueGame;
    private string _buttonContinueGameName = "Button_ContinueGame";

    private List<string> elementNames = new List<string>()
    {

    };

    private string _panelMainMenuName = "Panel_MainMenu";
    private VisualElement _panelMainMenu;


    private SaveSlotsMenu _saveSlotsMenu;
    

    private VisualElement _root;

    private UXMLFileHandler _uxmlFileHandler;

    private DataPersistenceData _dataPersistenceData;
    

    // menuButton
    // mainMenuBackingPanel
    #region
    /*
    NewGameButton
    LoadGameButton
    ContinueGameButton

    Panel_MainMenu

    Panel_SaveSlots


    Button_SaveSlot1
    Button_ContinueGame

    Button_ContinueGame

    .button_primary
    .button_secondary
    */
    #endregion
    public MainMenu(VisualElement root, 
        UXMLFileHandler uxmlfileHandler,
        DataPersistenceData dataPersistenceData)
    {
        _root = root;
        
        _uxmlFileHandler = uxmlfileHandler;
        
        _dataPersistenceData = dataPersistenceData;

        _saveSlotsMenu = new SaveSlotsMenu(this, _dataPersistenceData);

    }
    
    #region
    /// <summary>
    /// <br> Checks if there is save data.</br>
    /// <br> If there isn't, disable the continue game button.</br>
    /// </summary>
    #endregion
    private void CheckIfThereIsSaveData() // called in on start in userinterface
    {
        if (!_dataPersistenceData.SearchForSaveGameData()) // if there is no saved game data
        {
            _buttonContinueGame.SetEnabled(false);
            _buttonContinueGame.style.opacity = 0.5f;
        }
    }
    
    
    public void RegisterEvents() // called in on enable in userinterface
    {
        QueryElements(); // get all elements

        VisualElement panelSaveSlots = _uxmlFileHandler.UserInterfaceElements[UserInterfaces.SaveSlotsMenu];

        _saveSlotsMenu.QueryElements(panelSaveSlots);

        _buttonNewGame.clicked += OnNewGameClicked;
        _buttonLoadGame.clicked += OnLoadGameClicked;
        _buttonContinueGame.clicked += OnContinueGameClicked;

        

        CheckIfThereIsSaveData();

        _saveSlotsMenu.RegisterEvents();


    }

    private void QueryElements()
    {
        _panelMainMenu = _root.Q<VisualElement>(_panelMainMenuName);

        _buttonNewGame = _panelMainMenu.Q<Button>(_buttonNewGameName);
        _buttonLoadGame = _panelMainMenu.Q<Button>(_buttonLoadGameName);
        _buttonContinueGame = _panelMainMenu.Q<Button>(_buttonContinueGameName);

        

    }
    
    private void OnNewGameClicked()
    {
        _saveSlotsMenu.ActivateMenu(true);
        _panelMainMenu.style.display = DisplayStyle.None;
    
    }
    private void OnLoadGameClicked()
    {
        _saveSlotsMenu.ActivateMenu(true);

        _panelMainMenu.style.display = DisplayStyle.None;

    }
    public void OnContinueGameClicked()
    {
        DisableMenuButtons();
        /*
        _dataPersistenceManager.SaveGame(); // save the game before loading new scene
        SceneManager.LoadSceneAsync("Main Game");
        
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        */
        
    }

    private void DisableMenuButtons()
    {
        _buttonNewGame.SetEnabled(true);
        _buttonContinueGame.SetEnabled(false);
    }
    public void Activate(bool enable)
    {
        if (enable)
        {
            _panelMainMenu.style.display = DisplayStyle.Flex;
        }
        else
        {

            _panelMainMenu.style.display = DisplayStyle.None;
        }
    }
    
}
