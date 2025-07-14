using UnityEngine;
/// <summary>
/// <br> A class with a dialogueKnotName field. </br>
/// <br> Each NPC can have their own unique dialogueKnotName.</br>
/// </summary>
public class NPCInteraction : MonoBehaviour
{

    [Header("Dialogue Knot Name")]
    [SerializeField] private string _dialogueKnotName;

    private DialogueData _dialogueData;

    private bool _initialized = false;

    private string _silentDialogueKnotName;
    
    public void Initialize(DialogueData dialogueData)
    {
        _dialogueData = dialogueData;

        _initialized = true;
    }
    public void NPCDialogue() // enters into a dialogue event
    {
        if (!_initialized)
        {
            Debug.LogWarning("This npc has not been initialized yet!");
            return;
        }
        
        if (_dialogueData.InDialogue)
        {
        //    Debug.Log("CONTUN");
            _dialogueData.ContinueDialogue();
        }
        
        else if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {
         //   Debug.Log("START");
            _dialogueData.EnterDialogue(_dialogueKnotName);// begins the NPC's dialgoue at their knotName

        }
        else // the npc will default to ... if there is no dialogue knot 
        {
            _dialogueData.EnterDialogue("silentDialogue");
        }

    }
}
