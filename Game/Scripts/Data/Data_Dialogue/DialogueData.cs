using Ink.Runtime;
using MyBox;
using System.Text;
using UnityEngine;
#region
/// <summary>
/// Dialogue events and data.
/// </summary>
#endregion

public class DialogueData : Data
{
    public TextAsset InkJson;
    public Story Story;
    [Header("Events")]
    [SerializeField] private DialogueEvents _events;
    //   private StoryState StoryState;

    [Header("Debug")]
    public bool DebugMode = false;
    public bool ShowVariables = false;

    public bool InDialogue { get; private set; }
    public bool ResetStoryOnExit { get; private set; } = false;


    public string DialogueLine;

    [ReadOnly] public GameObject CurrentNpc;

    public StringBuilder ChoiceText = new StringBuilder();

    public bool CombatEntered => (bool)Story.variablesState["combatEntered"];

    private void OnEnable()
    {
        _events.OnUpdateDialogueLine += UpdateDialogueLine;
    }
    private void OnDestroy()
    {
        _events.OnUpdateDialogueLine -= UpdateDialogueLine;
    }
    private void UpdateDialogueLine(string dialogueLine)
    {
        DialogueLine = dialogueLine;
    }

    public void SetInDialogue(bool inDialogue)
    {
        InDialogue = inDialogue;
    }

    public override void LoadData(GameData data)
    {
        Story = data.Story;
    }

    public override void SaveData(GameData data)
    {
        data.Story = Story;
    }
}

