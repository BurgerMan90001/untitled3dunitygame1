
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
    [Header("Data")]
    [SerializeField] private CombatData _combatData;

    public bool InDialogue { get; private set; }

    public event Action<string> OnEnterDialogue;

    public event Action OnContinueDialogue;

    

    public event Action OnExitDialogue;
    public string DialogueLine;
    
    public StringBuilder ChoiceText = new StringBuilder();
   
    public Action<List<string>> OnUpdateChoices;

    public Action<int> OnChoiceSelected;

    private GameObject _interactedWithNpc;

    private void OnEnable()
    {
        InDialogue = false;
    }
    private void OnDisable()
    {
        InDialogue = false;
    }
    #region
    /// <summary>
    /// <br> Triggers the OnEnterDialogue event. </br>
    /// <br> InDialogue is set to true. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void EnterDialogue(string knotName, GameObject npc)
    {
        InDialogue = true;

        OnEnterDialogue?.Invoke(knotName); // null check

        _interactedWithNpc = npc;

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
        
        OnExitDialogue?.Invoke();

        ChoiceText?.Clear();

        InDialogue = false;

        

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
    /// <br> Triggers the OnUpdateChoices event. </br>
    /// </summary>
    /// <param name="choiceIndex"></param>
    #endregion
    public void UpdateStoryChoices(List<string> choicesText)
    {
        OnUpdateChoices?.Invoke(choicesText);
    }
    

    public void OnVariableChanged()
    {

    }
}
