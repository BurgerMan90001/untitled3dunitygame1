using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO OPTIMIZE DIALOGUE DATA AND DIALOGUE MANAGER

public class DialogueManager
{
    private DialogueData _dialogueData;


    private Story _story;
    public DialogueManager(Story story, DialogueData dialogueData)
    {
     
        _story = story;
        _dialogueData = dialogueData;

    }
    
    public void RegisterEvents()
    { 
        _story.onError += OnError;
        // trigger this class' EnterDialogue when the game dialogue event is triggered
        
        _dialogueData.Events.OnEnterDialogue += EnterDialogue;
        _dialogueData.Events.OnContinueDialogue += ContinueOrExitStory;
        _dialogueData.Events.OnChoiceSelected += SelectChoice;
        

    }
    public void UnregisterEvents()
    { 
        _story.onError -= OnError;

        
        _dialogueData.Events.OnEnterDialogue -= EnterDialogue;
        _dialogueData.Events.OnContinueDialogue -= ContinueOrExitStory;
        _dialogueData.Events.OnChoiceSelected -= SelectChoice;
        
    }
    
    
    private void EnterDialogue(string knotName, GameObject _) // begins or continues dialogue
    {
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
            _dialogueData.Events.ExitDialogue();


        }
    }
   
    private void UpdateChoices()
    {
        if (IsThereStoryChoices())
        {

            List<string> choicesText = _story.currentChoices.Select(choice => choice.text).ToList();
            
        //    updateStoryChoices(choicesText);
            
            _dialogueData.Events.UpdateStoryChoices(choicesText);
            _dialogueData.ChoiceText.Clear();
            

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
    public void ShowVariables()
    {
        foreach (var variable in _story.variablesState)
        {
            Debug.Log($"Variable: '{variable}' = {_story.variablesState[variable]} (Type: {_story.variablesState[variable].GetType()})");

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
//    _story.variablesState.variableChangedEvent += OnVariableChanged;
//  _story.variablesState.variableChangedEvent -= _variableStateHandler.OnVariableChanged;
// MAYBE BUT BROKEN MAYBE FIX
/*
    private void Test(string variableName, Ink.Runtime.Object newValue)
    {
        Debug.Log(variableName);
        Debug.Log(newValue);
    }

    private void OnVariableChanged(string variableName, Ink.Runtime.Object newValue)
    {
        if (_showVariableName)
        {
            Debug.Log(variableName);
            Debug.Log(newValue);
        }


        switch (newValue)
        {

            case BoolValue boolValue:
                BoolValueChanged(variableName, boolValue.value);
                Debug.Log("BOOL");
                break;
            case StringValue stringValue:
                StringValueChanged(variableName, stringValue.value);
                break;
            case FloatValue floatValue:
                FloatValueChanged(variableName, floatValue.value);
                break;

            case IntValue intValue:
                IntValueChanged(variableName, intValue.value);
                break;
            case null:
                Debug.LogError("OnVariableChanged got a null variable. ");
                break;
            default:
                Debug.LogWarning("OnVariableChanged got changed to an unknown variable. ");
                break;

        }

    }
    private void BoolValueChanged(string variableName, bool newValue)
    {
        switch (variableName, newValue)
        {
            case ("battleEntered", true): // if battleEntered variable state has changed and is true
                _combatData.EnterCombat();

                break;
            default:
                Debug.LogError("Could not find matching variable name.");
                break;
        }

    }
    private void IntValueChanged(string variableName, int newValue)
    {

    }
    private void FloatValueChanged(string variableName, float newValue)
    {

    }

    private void StringValueChanged(string variableName, string newValue)
    {

    }
    */

/*
    #region
    /// <summary>
    /// <br> Triggers a function with an updated bool value as an arguement.</br>
    /// </summary>
    /// <param name="variableName"></param>
    /// <param name="action"></param>
    #endregion
    private bool CheckBoolVariable(string variableName)
    {
        return (bool)_story.variablesState[variableName];
    }
    
    private int CheckIntVariable(string variableName)
    {
        return (int)_story.variablesState[variableName];

    }
    */