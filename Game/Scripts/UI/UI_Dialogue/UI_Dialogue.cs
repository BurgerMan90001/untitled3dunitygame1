

using System.Collections.Generic;
using UnityEngine;
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
        _choiceLabel = _panelChoices.Q<Label>("Label_Choice");

        _choiceButtons = _panelChoices.Query<Button>(className: "choiceButton").ToList();

        _panelChoices.style.display = DisplayStyle.None; // make sure
    }
    public void Register(VisualElement root)
    {
        _dialogueData.Events.OnEnterDialogue += DisplayDialoguePanel;
        _dialogueData.Events.OnContinueDialogue += UpdateText;
        _dialogueData.Events.OnExitDialogue += HideDialogue;


        _dialogueData.Events.OnUpdateChoices += UpdateChoices;


        SetupChoiceButtons();


    }
    public void Unregister()
    {
        _dialogueData.Events.OnEnterDialogue -= DisplayDialoguePanel;
        _dialogueData.Events.OnContinueDialogue -= UpdateText;
        _dialogueData.Events.OnExitDialogue -= HideDialogue;



        _dialogueData.Events.OnUpdateChoices -= UpdateChoices;



        UnsetupChoiceButtons();

    }
    private void SetupChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            int index = i;
            var choiceButton = _choiceButtons[i];

            choiceButton.clicked += () => ChoiceSelected(index);
        }
    }
    private void UnsetupChoiceButtons()
    {
        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            int index = i;
            var choiceButton = _choiceButtons[i];

            choiceButton.clicked -= () => ChoiceSelected(index);
        }
    }

    private void DisplayDialoguePanel(string _)
    {
        _userInterfaceData.SetInterfaceActive(UserInterfaceType.Dialogue, true);
        UpdateText();


    }
    private void UpdateText()
    {
        _dialogueLabel.text = _dialogueData.DialogueLine;
    }
    private void HideDialogue(GameObject _)
    {
        _userInterfaceData.SetInterfaceActive(UserInterfaceType.Dialogue, false);
    }

    private void UpdateChoices(List<string> choiceText)
    {
        _panelChoices.style.display = DisplayStyle.Flex;


        for (int i = 0; i < _choiceButtons.Count; i++)
        {
            var choiceButton = _choiceButtons[i];



            if (i < choiceText.Count)
            {
                choiceButton.text = choiceText[i];
                choiceButton.style.opacity = 1;
                choiceButton.SetEnabled(true);


            }
            else
            {
                choiceButton.text = "";
                choiceButton.style.opacity = 0;
                choiceButton.SetEnabled(false);
            }



        }
        GameCursor.Unlock();

    }

    private void ChoiceSelected(int choiceIndex)
    {

        _dialogueData.Events.SelectChoice(choiceIndex);

        GameCursor.Lock();

        _panelChoices.style.display = DisplayStyle.None;


    }


}
