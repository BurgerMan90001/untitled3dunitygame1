
using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine; 

//TODO IMPROVE
#region
/// <summary>
/// Dialogue events and data.
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Data/DialogueData")]
public class DialogueData : Data
{
    [Header("Ink Story")]
    [SerializeField] private TextAsset _inkJson;

    [Header("Settings")]
    [SerializeField] private bool _resetStoryAfterExit = true;
    
    [Header("Data")]
    
    [Header("Debug")]
    [SerializeField] private bool _debugMode = false;
 
    public bool InDialogue { get; private set; }

    public DialogueEvents Events { get; private set; } = new DialogueEvents();

    public Story Story { get; private set; }

    public string DialogueLine;

    public StringBuilder ChoiceText;
   
    

    private GameObject _interactedWithNpc;


    private DialogueManager _dialogueManager;
    
    private void OnEnable()
    {
        ChoiceText = new StringBuilder();
        Story = new Story(_inkJson.text);
        InDialogue = false;

        
        
        if (_debugMode)
        {
            _dialogueManager.ShowVariables();
        }

        
        _dialogueManager = new DialogueManager(Story, this);

        _dialogueManager.RegisterEvents();

        Events.OnEnterDialogue += OnEnterDialogue;

        Events.OnExitDialogue += OnExitDialogue;
    }
    private void OnDisable()
    {
        InDialogue = false;

        Events.OnEnterDialogue -= OnEnterDialogue;

        Events.OnExitDialogue -= OnExitDialogue;

        _dialogueManager?.UnregisterEvents();
    }
    

    private void OnEnterDialogue(string knotName, GameObject npc)
    {
        InDialogue = true;

       _interactedWithNpc = npc;
    }
    private void OnExitDialogue()
    {
        DialogueLine = "";

        ChoiceText?.Clear();

        InDialogue = false;

        if (_resetStoryAfterExit)
        {
            Story.ResetState();
        }

    }
    public bool TryGetVariable(string variableName, out object variable)
    {
        variable = Story.variablesState.TryGetDefaultVariableValue(variableName);
        if (variable != null)
        {
            return true;
        }                
        return false;
    }


    
}

public class DialogueEvents : IEvent
{
    #region
    /// <summary>
    /// <br> The string knotName and the gameobeject npc that was interacted with. </br>
    /// </summary>
    #endregion
    public event Action<string,GameObject> OnEnterDialogue;
    public event Action OnContinueDialogue;
    public event Action OnExitDialogue;

    public event Action<List<string>> OnUpdateChoices;
    public event Action<int> OnChoiceSelected;

    public DialogueEvents()
    {

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
        OnEnterDialogue?.Invoke(knotName, npc); // null check

        

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
}
