using Ink.Runtime;
using System;
using System.Text;
using UnityEngine;

//TODO IMPROVE AND SIMPLIFY
#region
/// <summary>
/// Dialogue events and data.
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Data/DialogueData")]
public class DialogueData : Data
{
    [Header("Ink Story")]
    [field: SerializeField] public TextAsset InkJson { get; private set; }
    public Story Story;

    [Header("Settings")]
    [field: SerializeField] public bool ResetStoryOnExit { get; private set; } = true;

    [Header("Debug")]
    public bool DebugMode = false;
    public bool ShowVariables = false;

    public bool InDialogue { get; private set; }

    public DialogueEvents Events { get; private set; } = new DialogueEvents();

    public string DialogueLine { get; private set; }
    public event Action<string> OnUpdateDialogueLine;

    public StringBuilder ChoiceText = new StringBuilder();

    public bool CombatEntered => (bool)Story.variablesState["combatEntered"];


    public void SetInDialogue(bool inDialogue)
    {
        InDialogue = inDialogue;
    }
    public void UpdateDialogueLine(string newDialogueLine)
    {
        DialogueLine = newDialogueLine;
        OnUpdateDialogueLine?.Invoke(newDialogueLine);
    }
    public override void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new System.NotImplementedException();
    }
}

//    _dialogueData.Story.variablesState.variableChangedEvent += OnVariableChanged;
//  _dialogueData.Story.variablesState.variableChangedEvent -= _variableStateHandler.OnVariableChanged;

