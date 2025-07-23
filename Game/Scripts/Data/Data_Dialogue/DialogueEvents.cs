using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/DialogueEvents")]
public class DialogueEvents : Event
{
    [SerializeField] private DialogueData _dialogueData;
    #region
    /// <summary>
    /// <br> The string knotName and the gameobeject npc that was interacted with. </br>
    /// </summary>
    #endregion
    public event Action<string> OnEnterDialogue;
    public event Action OnContinueDialogue;
    public event Action<string> OnUpdateDialogueLine;
    public event Action<GameObject> OnExitDialogue;


    public event Action<List<string>> OnUpdateChoices;
    public event Action<int> OnChoiceSelected;

    private GameObject _npc;

    public bool InDialogue => _dialogueData.InDialogue;

    public DialogueEvents() { }
    #region
    /// <summary>
    /// <br> Triggers the OnEnterDialogue event. </br>
    /// <br> InDialogue is set to true. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void EnterDialogue(string knotName, GameObject npc)
    {
        OnEnterDialogue?.Invoke(knotName); // null check

        _npc = npc;

    }
    #region
    /// <summary>
    /// <br> Triggers the OnOnContinueDialogue event. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void ContinueDialogue()
    {
        OnContinueDialogue?.Invoke();

    }
    #region
    /// <summary>
    /// <br> Triggers the OnExitDialogue selected event. </br>
    /// <br> Clears the ChoiceText stringbuilder string and sets InDialogue to false. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void ExitDialogue()
    {
        OnExitDialogue?.Invoke(_npc);
    }


    #region
    /// <summary>
    /// <br> Triggers the OnChoiceSelected event. </br>
    /// <br> Automatically continues dialogue when a choice is chosen, and turns input back on. </br>
    /// <br> Needs a line to play after a choice, or else it won't work. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void SelectChoice(int choiceIndex)
    {
        OnChoiceSelected?.Invoke(choiceIndex);


        ContinueDialogue(); // automatically continue dialogue
    }
    #region
    /// <summary>
    /// <br> Happens when there are choices in the ink story. </br>
    /// <br> Triggers the OnUpdateChoices event. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void UpdateStoryChoices(List<string> choicesText)
    {

        OnUpdateChoices?.Invoke(choicesText);
    }

    public void UpdateDialogueLine(string newDialogueLine)
    {
        _dialogueData.DialogueLine = newDialogueLine;
        OnUpdateDialogueLine?.Invoke(newDialogueLine);
    }
    public void ObservVariable(string variableName)
    {

    }
}
