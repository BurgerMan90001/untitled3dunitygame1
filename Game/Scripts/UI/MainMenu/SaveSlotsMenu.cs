using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UI_SaveSlotsMenu : IUserInterface
{

    private VisualElement _panelSaveSlots;

    private List<Button> _saveSlotButtons;


    private List<string> _profileIDs = new List<string>();


    private Button _buttonBack;
  
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

    private DataPersistenceData _dataPersistenceData;
    private UserInterfaceToggler _userInterfaceToggler;

    public UI_SaveSlotsMenu(DataPersistenceData dataPersistenceData, UserInterfaceToggler userInterfaceToggler)
    {

        _dataPersistenceData = dataPersistenceData;
        _userInterfaceToggler = userInterfaceToggler;
    }
    public void QueryElements(VisualElement root)
    { 
        _panelSaveSlots = root.Q<VisualElement>("Panel_SaveSlots");

        _saveSlotButtons = _panelSaveSlots.Query<Button>( className: "button_saveSlot").ToList(); // without the dot
        
        _buttonBack = _panelSaveSlots.Query<Button>(className: "button_back");

    }
   
    
    public void Register(VisualElement root)
    {
        _buttonBack.clicked += OnBackClicked;
        SetupSaveSlots();
    }

    public void Unregister()
    {
        _buttonBack.clicked -= OnBackClicked;
        UnsetupSaveSlots();
    }
    private void SetupSaveSlots()
    {
        for (int i = 0; i < _saveSlotButtons.Count; i++)
        {
            Button saveSlotButton = _saveSlotButtons[i];

            saveSlotButton.userData = new SaveSlotData(i.ToString(), saveSlotButton); // numbers profile ids to 0, 1, 2

            saveSlotButton.style.display = DisplayStyle.Flex;

            var buttonData = saveSlotButton.userData;

            saveSlotButton.clicked += () => OnSaveSlotClicked(buttonData); // register click events

        }
    }
    private void UnsetupSaveSlots()
    {
        for (int i = 0; i < _saveSlotButtons.Count; i++)
        {
            Button saveSlotButton = _saveSlotButtons[i];
            var buttonData = saveSlotButton.userData;
            saveSlotButton.clicked -= () => OnSaveSlotClicked(buttonData); // register click events
        }
    }
    private void OnBackClicked()
    {
        _userInterfaceToggler.ToggleUserInterface(UserInterfaces.MainMenu);
        _userInterfaceToggler.ToggleUserInterface(UserInterfaces.SaveSlotsMenu);
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

        _dataPersistenceData.ChangeSelectedProfileID(saveSlotData.ProfileID);

        if (!_isLoadingGame) //create a new game 
        {
            _dataPersistenceData.StartNewGame();
        }

        _dataPersistenceData.SaveGameData();

        SceneLoadingManager.LoadScene("Main Game", UserInterfaces.HUD, true);
        


        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    }
    

    
    public void ActivateMenu(bool isLoadingGame)
    {
        _panelSaveSlots.style.display = DisplayStyle.Flex;
        _panelSaveSlots.SetEnabled(true);
        _isLoadingGame = isLoadingGame;

        ActivateSaveSlots();

        Button firstSelectedButton = _buttonBack;

        firstSelectedButton.Focus(); // selects the button

    }

    private void ActivateSaveSlots()
    {
        Dictionary<string, GameData> profilesGameData = _dataPersistenceData.GetAllProfilesGameData();
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
