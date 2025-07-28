using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;




public class DialogueManager : MonoBehaviour, IDialogueManager
{

    private DialogueEvents _dialogueEvents;

    private DialogueData _dialogueData;


    public void Inject(DialogueEvents dialogueEvents)
    {
        _dialogueEvents = dialogueEvents;
    }

    private void Awake()
    {

        _dialogueData = GetComponent<DialogueData>();

        if (_dialogueData.Story == null)
        {
            _dialogueData.Story = new Story(_dialogueData.InkJson.text);
            Debug.Log("Created a new ink story because it didn't exsist.");
        }

        if (_dialogueData.ShowVariables)
        {
            ShowVariables();
        }
        _dialogueData.ChoiceText = new StringBuilder();


    }

    private void Start()
    {
        //    _dialogueData.Story = new Story(_textAsset.text);
        _dialogueData.SetInDialogue(false);
    }
    private void OnEnable()
    {
        _dialogueData.Story.onError += OnError;
        // trigger this class' EnterDialogue when the game dialogue event is triggered

        _dialogueEvents.OnEnterDialogue += EnterDialogue;
        _dialogueEvents.OnContinueDialogue += ContinueOrExitStory;
        _dialogueEvents.OnChoiceSelected += SelectChoice;
        Debug.Log("hjksdfhfsdnfsf");


    }
    private void OnDisable()
    {
        _dialogueData.Story.onError -= OnError;

        _dialogueEvents.OnEnterDialogue -= EnterDialogue;
        _dialogueEvents.OnContinueDialogue -= ContinueOrExitStory;
        _dialogueEvents.OnChoiceSelected -= SelectChoice;

        ExitDialogue();


    }


    private void EnterDialogue(string knotName) // begins or continues dialogue
    {

        if (!knotName.Equals(""))
        {
            _dialogueData.SetInDialogue(true);
            _dialogueData.Story.ChoosePathString(knotName); // jump to the knotname in the ink file

            ContinueOrExitStory();
        }
        else
        {
            Debug.LogWarning("Knot name was empty when entering dialogue");
        }

    }

    private void ContinueOrExitStory() // updates the dialogue lines
    {
        if (_dialogueData.Story.canContinue)
        {

            string dialogueLine = _dialogueData.Story.Continue();


            _dialogueEvents.UpdateDialogueLine(dialogueLine);
            if (IsThereStoryChoices())
            {
                UpdateChoices();

            }

        }
        else // when there is no more dialogue lines in the story, exit
        {
            ExitDialogue();

        }
    }
    private void ExitDialogue()
    {
        _dialogueEvents.ExitDialogue();

        _dialogueEvents.UpdateDialogueLine("");

        _dialogueData.ChoiceText?.Clear();

        _dialogueData.SetInDialogue(false);

        if (_dialogueData.ResetStoryOnExit)
        {
            _dialogueData.Story.ResetState();
        }

    }
    private void UpdateChoices()
    {
        if (IsThereStoryChoices())
        {

            List<string> choicesText = _dialogueData.Story.currentChoices.Select(choice => choice.text).ToList();


            _dialogueEvents.UpdateStoryChoices(choicesText);
            _dialogueData.ChoiceText.Clear();


        }
        else
        {
            Debug.LogError("There are currently no choices available. ");
        }
    }

    private bool IsThereStoryChoices()
    {
        return _dialogueData.Story.currentChoices.Count > 0; // if there are choices in the story

    }


    private void SelectChoice(int choiceIndex)
    {
        _dialogueData.Story.ChooseChoiceIndex(choiceIndex);

    }
    public void ShowVariables()
    {
        foreach (var variable in _dialogueData.Story.variablesState)
        {
            Debug.Log($"Variable: '{variable}' = {_dialogueData.Story.variablesState[variable]} (Type: {_dialogueData.Story.variablesState[variable].GetType()})");

        }
    }

    private void OnError(string message, Ink.ErrorType type)
    {
        if (type == Ink.ErrorType.Warning)
        {
            Debug.LogWarning(message);
        }

        else
        {
            Debug.LogError(message);
        }

    }


    /*

public override void Initialize(DialogueEvents dialogueEvents)
{
   Debug.Log("Dialogue manager Initializeed.");
   throw new System.NotImplementedException();
}
/*

private IEnumerator AutomaticallyContinueStory()
{
yield return new WaitForSeconds(_continueDialogueInSeconds);
ContinueOrExitStory();
}
*/

}
