using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class DialogueManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;


    private void Awake()
    {
        _dialogueData.Story = new Story(_dialogueData.InkJson.text);
        if (_dialogueData.DebugMode)
        {
            ShowVariables();
        }
        _dialogueData.ChoiceText = new StringBuilder();


    }

    private void Start()
    {
        _dialogueData.SetInDialogue(false);
    }
    private void OnEnable()
    {
        _dialogueData.Story.onError += OnError;
        // trigger this class' EnterDialogue when the game dialogue event is triggered

        _dialogueData.Events.OnEnterDialogue += EnterDialogue;
        _dialogueData.Events.OnContinueDialogue += ContinueOrExitStory;
        _dialogueData.Events.OnChoiceSelected += SelectChoice;



    }
    private void OnDisable()
    {
        _dialogueData.Story.onError -= OnError;


        _dialogueData.Events.OnEnterDialogue -= EnterDialogue;
        _dialogueData.Events.OnContinueDialogue -= ContinueOrExitStory;
        _dialogueData.Events.OnChoiceSelected -= SelectChoice;

    }


    private void EnterDialogue(string knotName) // begins or continues dialogue
    {
        if (!knotName.Equals(""))
        {
            _dialogueData.SetInDialogue(true);
            _dialogueData.Story.ChoosePathString(knotName); // jump to the knotname in the ink file

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

            _dialogueData.DialogueLine = dialogueLine; // update the scriptable object's line


            if (IsThereStoryChoices())
            {
                UpdateChoices();

            }

        }
        else // when there is no more dialogue lines in the story, exit
        {
            _dialogueData.Events.ExitDialogue();

            _dialogueData.DialogueLine = "";

            _dialogueData.ChoiceText?.Clear();

            _dialogueData.SetInDialogue(false);

            if (_dialogueData.ResetStoryOnExit)
            {
                _dialogueData.Story.ResetState();
            }

        }
    }
    private void OnExitDialogue(GameObject _)
    {


    }
    private void UpdateChoices()
    {
        if (IsThereStoryChoices())
        {

            List<string> choicesText = _dialogueData.Story.currentChoices.Select(choice => choice.text).ToList();

            //    updateStoryChoices(choicesText);

            _dialogueData.Events.UpdateStoryChoices(choicesText);
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
    private IEnumerator AutomaticallyContinueStory()
    {
        yield return new WaitForSeconds(_continueDialogueInSeconds);
        ContinueOrExitStory();
    }
    */

}
