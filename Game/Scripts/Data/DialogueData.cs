
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
#region
/// <summary>
/// Dialogue events and data.
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Data/DialogueData")]
public class DialogueData : Data
{
    public event Action<string> OnEnterDialogue;

    public event Action OnContinueDialogue;

    public bool InDialogue { get; private set; }

    public event Action OnExitDialogue;
    public string DialogueLine;
    
    public StringBuilder ChoiceText = new StringBuilder();
   
    public Action<List<string>> OnUpdateChoices;

    public Action<int> OnChoiceSelected;

    public void EnterDialogue(string knotName)
    {
        
        OnEnterDialogue?.Invoke(knotName); // null check

        InDialogue = true;
        Debug.Log("ENTER");
    }
    public void ContinueDialogue()
    {
        OnContinueDialogue?.Invoke();
        Debug.Log("COJNT");
    }
    public void ExitDialogue()
    {
        Debug.Log("EXIT");
        OnExitDialogue?.Invoke();

        ChoiceText?.Clear();

        InDialogue = false;

    }

    public void SelectChoice(int choiceIndex)
    {
        OnChoiceSelected?.Invoke(choiceIndex);
    }
    public void UpdateStoryChoices(List<string> choicesText)
    {
        OnUpdateChoices?.Invoke(choicesText);
    }
    
}
