
using UnityEngine;
/// <summary>
/// <br> TODO ADD MORE FEATURES. </br>
/// <br> Jumps to the dialogueKnotName in the ink story. </br>
/// <br> Each NPC can have their own unique dialogueKnotName.</br>
/// </summary>
public class NPCInteraction
{

    private string _dialogueKnotName;
    
    private DialogueEvents _dialogueEvents;

    //   private int _interactionStage = 0; MAYBE

    private const string _silentDialogueKnotName = "silentDialogue";
    public NPCInteraction(string dialogueKnotName, DialogueEvents dialogueEvents)
    {
        _dialogueKnotName = dialogueKnotName;
        _dialogueEvents = dialogueEvents;
    }

    public void Interact(GameObject npc)
    {

        if (_dialogueEvents.InDialogue)
        {

            _dialogueEvents.ContinueDialogue();

        }

        else if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {

            _dialogueEvents.EnterDialogue(_dialogueKnotName, npc);// begins the NPC's dialgoue at their knotName

        }
        else // the npc will default to ... if there is no dialogue knot 
        {
            _dialogueEvents.EnterDialogue(_silentDialogueKnotName, npc);
        }
    }
}
