
using UnityEngine;
/// <summary>
/// <br> TODO ADD MORE FEATURES. </br>
/// <br> Jumps to the dialogueKnotName in the ink story. </br>
/// <br> Each NPC can have their own unique dialogueKnotName.</br>
/// </summary>
public class NPCInteraction
{

    private string _dialogueKnotName;

    private DialogueData _dialogueData;

    private int _interactionStage = 0;

    private const string _silentDialogueKnotName = "silentDialogue";
    public NPCInteraction(string dialogueKnotName, DialogueData dialogueData)
    {
        _dialogueKnotName = dialogueKnotName;
        _dialogueData = dialogueData;
    }

    public void Interact(GameObject npc)
    {

        if (_dialogueData.InDialogue)
        {

            _dialogueData.Events.ContinueDialogue();

        }

        else if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {

            _dialogueData.Events.EnterDialogue(_dialogueKnotName, npc);// begins the NPC's dialgoue at their knotName

        }
        else // the npc will default to ... if there is no dialogue knot 
        {
            _dialogueData.Events.EnterDialogue(_silentDialogueKnotName, npc);
        }
    }
}
