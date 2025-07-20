
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
    

    private void OnEnterDialogue(string _)
    {
        InDialogue = true;

    }
    private void OnExitDialogue(GameObject _)
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

