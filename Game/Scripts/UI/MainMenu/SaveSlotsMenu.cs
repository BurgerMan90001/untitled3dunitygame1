using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UIElements;

public class SaveSlotsMenu : Menu
{

    private VisualElement _panelSaveSlots;

    private List<Button> _saveSlotButtons;


    private MainMenu _mainMenu;


    private List<string> _profileIDs = new List<string>();

    private Button _buttonBack;


    private DataPersistenceManager _dataPersistenceManager;
  
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
    private bool _isLoadingGame = false;

    public SaveSlotsMenu(DataPersistenceManager dataPersistenceManager, 
        MainMenu mainMenu)
    {
        _dataPersistenceManager = dataPersistenceManager;

        _mainMenu = mainMenu;


    }
    public void QueryElements(VisualElement panelSaveSlots)
    {

        _panelSaveSlots = panelSaveSlots;

        _panelSaveSlots.style.display = DisplayStyle.None;

        _saveSlotButtons = _panelSaveSlots.Query<Button>( className: "button_saveSlot").ToList(); // without the dot
        
        _buttonBack = _panelSaveSlots.Query<Button>(className: "button_back");

    }
   
    public void RegisterEvents() // called in on enable after query elements
    {

        _buttonBack.clicked += OnBackClicked;

        SetupSaveSlots();

        
    }
    private void SetupSaveSlots()
    {
        for (int i = 0; i < _saveSlotButtons.Count; i++)
        {
            Button saveSlotButton = _saveSlotButtons[i];

            saveSlotButton.userData = new SaveSlotData(i.ToString(), saveSlotButton); // numbers profile ids to 0, 1, 2

            saveSlotButton.style.display = DisplayStyle.Flex;

            saveSlotButton.clicked += () => OnSaveSlotClicked(saveSlotButton.userData); // register click events

        }
    }
    
    private void OnBackClicked()
    {
        _mainMenu.Activate(true);

        _panelSaveSlots.style.display = DisplayStyle.None; // deactivates or hides the save slot menu
    }
    public void OnSaveSlotClicked(object userData)
    {
        if (userData is SaveSlotData saveSlotData)
        {
            LoadGame(saveSlotData);

        } else
        {
            Debug.LogError("The userData in the save slot is not a save slot!");
        }
        
    }
    private void LoadGame(SaveSlotData saveSlotData)
    {
        _panelSaveSlots.style.display = DisplayStyle.None;
    //    DisableSaveSlotButtons();

        _dataPersistenceManager.ChangeSelectedProfileID(saveSlotData.ProfileID);

        if (!_isLoadingGame) //create a new game 
        {
            _dataPersistenceManager.NewGame();
        }

        _dataPersistenceManager.SaveGame();

        SceneLoadingManager.SceneToLoad = "Main Game";
        SceneLoadingManager.UserInterfaceToLoad = UserInterfaces.HUD;
        SceneLoadingManager.LoadLoadingScreen();


        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    

    
    public void ActivateMenu(bool isLoadingGame)
    {
        _panelSaveSlots.style.display = DisplayStyle.Flex;
        _panelSaveSlots.SetEnabled(true);
        _isLoadingGame = isLoadingGame;


        // a dictionary of all of the profiles ids with their corresponding data
        Dictionary<string, GameData> profilesGameData = _dataPersistenceManager.GetAllProfilesGameData();


        
        ActivateSaveSlots(profilesGameData);

        Button firstSelectedButton = _buttonBack;

        firstSelectedButton.Focus(); // selects the button

    //    SetFirstSelected(firstSelectedButton);
    }

    private void ActivateSaveSlots(Dictionary<string, GameData> profilesGameData)
    {
        Button firstSelectedButton = _buttonBack; // the defualt button that is selected

        for (int i = 0; i < _saveSlotButtons.Count; i++)
        {
            Button saveSlotButton = _saveSlotButtons[i];
            
            if (saveSlotButton.userData is SaveSlotData saveSlotData)
            {
                // tries to get the profile data from the profile id
                profilesGameData.TryGetValue(saveSlotData.ProfileID, out GameData profileData);
                
                saveSlotData.SetData(profileData);

                if (profileData != null && _isLoadingGame) // when there is no data
                {
                    saveSlotData.SetButtonInteractable(false);
                }
                else // when there is data
                {
                    saveSlotData.SetButtonInteractable(true);

                    if (firstSelectedButton.Equals(_buttonBack)) // set the selected button to the first found save slot
                    {
                        firstSelectedButton = saveSlotData.Button;
                    }
                }
            }

        }
        
    }
    
    /*
    public void DeactivateMenu()
    {
        _panelSaveSlots.style.display = StyleKeyword.None;
    //    gameObject.SetActive(false);
    }

    */

    private void DisableSaveSlotButtons()
    {
        foreach (Button saveSlotButton in _saveSlotButtons)
        {
            saveSlotButton.SetEnabled(false);
            saveSlotButton.style.display = DisplayStyle.None;
        }
    }

}
