using UnityEngine;

using UnityEngine.UIElements;

[System.Serializable]
public class SaveSlotData
{
    /*
    [Header("Profile")]
    [SerializeField] private string profileID = "";

    [Header("Content")]
    [SerializeField] private GameObject emptySaveSlot;
    [SerializeField] private GameObject playedSaveSlot;

    */

    private string _emptySlotText = "Empty";

    private string _playedSlotText = "Played";
    public Button Button { get; private set; }
    public string ProfileID { get; private set; }

//    [SerializeField] private TextMeshProUGUI text1;
//    [SerializeField] private TextMeshProUGUI text2; // text for a playedSaveSlot

    

 //   private VisualElement _emptySaveSlot;
 

    public SaveSlotData(string profileID, Button button)
    {
        ProfileID = profileID;
        Button = button;
    }
    /*
    private void Awake()
    {
        saveSlotButton = GetComponent<Button>();
    }
    */
    public void SetData(GameData data)
    {
        if (data == null) // when there is no data for this profileID
        {
            ShowEmptySaveSlot();

        }
        else
        {
            ShowPlayedSaveSlot();

        //    text1.text = "LOAD GAME";
          //  text2.text = "FILER";
        }
    }

    private void ShowEmptySaveSlot()
    {
        
        Button.style.display = DisplayStyle.Flex;

        Button.text = _emptySlotText;

    }
    private void ShowPlayedSaveSlot()
    {
        Button.style.display = DisplayStyle.Flex;

        Button.text = _playedSlotText;
      

    }
    
    public void SetButtonInteractable(bool active)
    {
        Button.SetEnabled(active);
    }
}