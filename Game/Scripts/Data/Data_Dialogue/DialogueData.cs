using Ink.Runtime;
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
    [field: SerializeField] public TextAsset InkJson { get; private set; }
    public Story Story;

    [Header("Settings")]
    [SerializeField] public bool ResetStoryOnExit { get; private set; } = true;

    [Header("Data")]

    [Header("Debug")]
    [field: SerializeField] public bool DebugMode { get; private set; }

    public bool InDialogue { get; private set; }

    public DialogueEvents Events { get; private set; } = new DialogueEvents();

    public string DialogueLine;

    public StringBuilder ChoiceText;

    public bool CombatEntered => (bool)Story.variablesState["combatEntered"];


    public void SetInDialogue(bool inDialogue)
    {
        InDialogue = inDialogue;
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

