using Ink.Runtime;
using MyBox;
using System.IO;
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


//TODO
public static class StoryStateSerialization
{
    // Set a path to save and restore StoryState.
    private static readonly string _fileName = "StoryState.json";
    private static readonly string _savePath = Application.persistentDataPath + "/" + _fileName;

    /// <summary>
    /// Convert a StoryState into a JSON string and save file.
    /// </summary>
    public static void Serialize(StoryState storyState)
    {
        // Either create or overwrite an existing story file.
        File.WriteAllText(_savePath, storyState.ToJson());
    }

    /// <summary>
    /// Update referenced Story object based on saved StoryState (if it exists)
    /// </summary>
    /// <param name="story"></param>
    /// <returns></returns>
    public static Story Deserialize(ref Story story)
    {

        string JSONContents; // create internal JSON string.


        if (File.Exists(_savePath))
        {

            JSONContents = File.ReadAllText(_savePath); // reads the entire json file

            story.state.LoadJson(JSONContents); // overwrite current Story based on saved StoryState data.
        }


        return story; // return either referenced or restored story.
    }
}