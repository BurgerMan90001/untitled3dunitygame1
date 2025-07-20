
using System.ComponentModel;
using UnityEngine.UIElements;
// TODO FIX CONTIUME AND DATA PERSISTENCE

public class UI_MainMenu : IUserInterface
{

    private Button _buttonNewGame;
    private Button _buttonLoadGame;
    private Button _buttonContinueGame;

    private VisualElement _panelMainMenu;

    private DataPersistenceData _dataPersistenceData;
    private UserInterfaceToggler _userInterfaceToggler;

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
    public UI_MainMenu(DataPersistenceData dataPersistenceData, UserInterfaceToggler userInterfaceToggler)
    {
   
        _dataPersistenceData = dataPersistenceData; 
        _userInterfaceToggler = userInterfaceToggler;
        var container = new Container();
        
        
    }

    public void Register(VisualElement root) // called in on enable in userinterface
    {

        _buttonNewGame.clicked += OnNewGameClicked;
        _buttonLoadGame.clicked += OnLoadGameClicked;
        _buttonContinueGame.clicked += OnContinueGameClicked;

        CheckIfThereIsSaveData();
    }
    public void Unregister()
    {
        _buttonNewGame.clicked -= OnNewGameClicked;
        _buttonLoadGame.clicked -= OnLoadGameClicked;
        _buttonContinueGame.clicked -= OnContinueGameClicked;
    }
    public void QueryElements(VisualElement root)
    {
        _panelMainMenu = root.Q<VisualElement>("Panel_MainMenu");

        _buttonNewGame = _panelMainMenu.Q<Button>("Button_NewGame");
        _buttonLoadGame = _panelMainMenu.Q<Button>("Button_LoadGame");
        _buttonContinueGame = _panelMainMenu.Q<Button>("Button_ContinueGame");

    }

    
    private void OnNewGameClicked()
    {
        _userInterfaceToggler.ToggleUserInterface(UserInterfaceType.SaveSlotsMenu, true); // is loading = false 

        _userInterfaceToggler.ToggleUserInterface(UserInterfaceType.MainMenu, false);


    }
    private void OnLoadGameClicked()
    {
        _userInterfaceToggler.ToggleUserInterface(UserInterfaceType.SaveSlotsMenu, true); // is loading = true


        _userInterfaceToggler.ToggleUserInterface(UserInterfaceType.MainMenu, false);

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
   
    
}
