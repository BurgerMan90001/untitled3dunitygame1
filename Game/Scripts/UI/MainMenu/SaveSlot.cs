
using UnityEngine.UIElements;

public class SaveSlotData
{
    private string _emptySlotText = "Empty";

    private string _playedSlotText = "Played";
    public Button Button { get; private set; }
    public string ProfileID { get; private set; }



    public SaveSlotData(string profileID, Button button)
    {
        ProfileID = profileID;
        Button = button;
    }
    
    public void SetData(GameData data)
    {
        if (data == null) // when there is no data for this profileID
        {
            ShowEmptySaveSlot();

        }
        else
        {
            ShowPlayedSaveSlot();

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