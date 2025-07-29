

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// animationsz logic
// MAYBE HAVE DIFFERENT DIALOGUE UI FOR CHARACTERS
public class UI_Dialogue : IUserInterface
{
    private UserInterfaceEvents _userInterfaceEvents;

    private VisualElement _panelDialogue; // Panel_Dialogue uxml name
    private VisualElement _panelChoices; // Panel_Choices uxml name

    private Label _dialogueLabel;


    private List<Button> _choiceButtons;

    private DialogueEvents _dialogueEvents;

    public UI_Dialogue(UserInterfaceEvents userInterfaceEvents, DialogueEvents dialogueEvents)
    {
        _userInterfaceEvents = userInterfaceEvents;
        _dialogueEvents = dialogueEvents;

    }
    public void QueryElements(VisualElement root)
    {

        _panelDialogue = root.Q<VisualElement>("Panel_Dialogue");
        _panelChoices = root.Q<VisualElement>("Panel_Choices");

        _dialogueLabel = _panelDialogue.Q<Label>("Label_Dialogue");


        _choiceButtons = _panelChoices.Query<Button>(className: "choiceButton").ToList();

        _panelChoices.style.display = DisplayStyle.None; // make sure
    }
    public void Register(VisualElement root)
    {
        _dialogueEvents.OnEnterDialogue += DisplayDialoguePanel;
        _dialogueEvents.OnUpdateDialogueLine += UpdateText;
        _dialogueEvents.OnExitDialogue += HideDialogue;


        _dialogueEvents.OnUpdateChoices += UpdateChoices;


        SetupChoiceButtons();


    }
    public void Unregister()
    {
        _dialogueEvents.OnEnterDialogue -= DisplayDialoguePanel;
        _dialogueEvents.OnUpdateDialogueLine -= UpdateText;
        _dialogueEvents.OnExitDialogue -= HideDialogue;



        _dialogueEvents.OnUpdateChoices -= UpdateChoices;



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

    private void DisplayDialoguePanel(string _, GameObject _1)
    {

        _userInterfaceEvents.ShowInterface(UserInterfaceType.Dialogue);


    }
    private void UpdateText(string newText)
    {

        _dialogueLabel.text = newText;
    }
    private void HideDialogue()
    {
        _userInterfaceEvents.HideRecentInterface();
        _dialogueLabel.text = "";
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

        _dialogueEvents.SelectChoice(choiceIndex);

        GameCursor.Lock();

        _panelChoices.style.display = DisplayStyle.None;


    }


}
