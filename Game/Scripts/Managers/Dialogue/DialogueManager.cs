using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//TODO SEPARATE _variableStateHandler  and dialogue manager

public class DialogueManager : MonoBehaviour
{
    [Header("Dependencies")]


    [Header("Dialogue Settings")]
    [SerializeField] private bool _autoPlayDialogue = false;
    [SerializeField] private float _continueDialogueInSeconds = 5f;

    [Header("Ink Story")]
    [SerializeField] private TextAsset _inkJson;

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private CombatData _combatData;
    [SerializeField] private VariableStateHandler _variableStateHandler;

    private Story _story;

   

    private StringBuilder _choiceText;

    private void Awake()
    {
        _story = new Story(_inkJson.text);
        _choiceText = new StringBuilder();

        //    _story.variablesState[]
    }
    private void OnEnable()
    {
        // trigger this class' EnterDialogue when the game dialogue event is triggered
        _dialogueData.OnEnterDialogue += EnterDialogue;
        _dialogueData.OnContinueDialogue += ContinueOrExitStory;
        _dialogueData.OnChoiceSelected += SelectChoice;

        _story.variablesState.variableChangedEvent += _variableStateHandler.OnVariableChanged; 

    }


    private void OnDisable()
    {
        _dialogueData.OnEnterDialogue -= EnterDialogue;
        _dialogueData.OnContinueDialogue -= ContinueOrExitStory;
        _dialogueData.OnChoiceSelected -= SelectChoice;

        _story.variablesState.variableChangedEvent -= _variableStateHandler.OnVariableChanged;
    }

    private void EnterDialogue(string knotName) // begins or continues dialogue
    {
        Debug.Log("ASDASDASDASDASDASD");
        if (!knotName.Equals(""))
        {
            _story.ChoosePathString(knotName); // jump to the knotname in the ink file

        }
        else
        {
            Debug.LogWarning("Knot name was empty when entering dialogue");
        }
        ContinueOrExitStory();
    }

    private void ContinueOrExitStory() // updates the dialogue lines
    {
        if (_story.canContinue)
        {

            string dialogueLine = _story.Continue();

            _dialogueData.DialogueLine = dialogueLine; // update the scriptable object's line

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
        _dialogueData.DialogueLine = "";

        _story.ResetState();

        _dialogueData.ExitDialogue();

    }
    private void UpdateChoices()
    {
        if (IsThereStoryChoices())
        {

            List<string> choicesText = _story.currentChoices.Select(choice => choice.text).ToList();
            _dialogueData.UpdateStoryChoices(choicesText);
            _choiceText.Clear();

        } else
        {
            Debug.LogError("There are currently no choices available. ");
        }
    }

    private bool IsThereStoryChoices()
    {
        return _story.currentChoices.Count > 0; // if there are choices in the story

    }

    private void SelectChoice(int choiceIndex)
    {

        _story.ChooseChoiceIndex(choiceIndex);


    }

    
    private IEnumerator AutomaticallyContinueStory()
    {
        yield return new WaitForSeconds(_continueDialogueInSeconds);
        ContinueOrExitStory();
    }

}
