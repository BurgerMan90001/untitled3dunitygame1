using Ink.Runtime;
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


    //   private StoryState StoryState;

    public bool DebugMode = false;
    public bool ShowVariables = false;

    public bool InDialogue { get; private set; }
    public bool ResetStoryOnExit { get; private set; } = false;


    public string DialogueLine;


    public StringBuilder ChoiceText = new StringBuilder();

    public bool CombatEntered => (bool)Story.variablesState["combatEntered"];


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

