

using System.Collections.Generic;
using UnityEngine.UIElements;
// animationsz logic
public class UI_Dialogue : IUserInterface
{
    private UserInterfaceData _userInterfaceData;

    private VisualElement _panelDialogue; // Panel_Dialogue uxml name
    private VisualElement _panelChoices; // Panel_Choices uxml name

    private Label _choiceLabel;
    private Label _dialogueLabel;


    private List<Button> _choiceButtons;

    private DialogueData _dialogueData;
    public UI_Dialogue(UserInterfaceData userInterfaceData, DialogueData dialogueData)
    {
        _userInterfaceData = userInterfaceData;
        _dialogueData = dialogueData;
        
    }
    public void QueryElements(VisualElement root)
    {
        
        _panelDialogue = root.Q<VisualElement>("Panel_Dialogue");
        _panelChoices = root.Q<VisualElement>("Panel_Choices");

        _dialogueLabel = _panelDialogue.Q<Label>("Label_Dialogue");
        _choiceLabel = _panelDialogue.Q<Label>("Label_Choice");

        _choiceButtons = _panelChoices.Query<Button>(className: "choiceButton").ToList();

        _panelChoices.style.display = DisplayStyle.None; // make sure
    }
    public void Register(VisualElement root)
    {
        _dialogueData.OnEnterDialogue += DisplayDialogue;
        _dialogueData.OnContinueDialogue += UpdateText;
        _dialogueData.OnExitDialogue += HideDialogue;
        

        _dialogueData.OnUpdateChoices += UpdateChoices;

        /*
        for (int i = 0; i < _choiceButtons.Count; i++) 
        {
            int index = i;
            var choiceButton = _choiceButtons[i];

            choiceButton.clicked += () => ChoiceSelected(index);
        }
        */
        
    }
    public void Unregister()
    {
        _dialogueData.OnEnterDialogue -= DisplayDialogue;
        _dialogueData.OnContinueDialogue -= UpdateText;
        _dialogueData.OnExitDialogue -= HideDialogue;

        

        _dialogueData.OnUpdateChoices -= UpdateChoices;


        /*
        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            int index = i;
            var choiceButton = _choiceButtons[i];

            choiceButton.clicked -= () => ChoiceSelected(index);
        }
        */
    }
    
    

    private void DisplayDialogue(string _)
    {
        _userInterfaceData.ToggleUserInterface(UserInterfaces.Dialogue);
        _dialogueLabel.text = _dialogueData.DialogueLine;
    }
    private void UpdateText()
    {
        _dialogueLabel.text = _dialogueData.DialogueLine;
    }
    private void HideDialogue()
    {
        _userInterfaceData.ToggleUserInterface(UserInterfaces.Dialogue);
    }
    
    private void UpdateChoices(List<string> choiceText)
    {
        _panelChoices.style.display = DisplayStyle.Flex;
        
        /*
        foreach (var choice in choiceText)
        {
            Debug
        }
        */
        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;

        UnityEngine.Cursor.visible = true;
    }
    
    private void ChoiceSelected(int choiceIndex) 
    {
        _dialogueData.SelectChoice(choiceIndex);

        UnityEngine.Cursor.lockState = UnityEngine.CursorLockMode.None;

        UnityEngine.Cursor.visible = false;
    }
    
 
}
