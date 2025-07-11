using UnityEngine;
/// <summary>
/// <br> A class with a dialogueKnotName field. </br>
/// <br> Each NPC can have their own unique dialogueKnotName.</br>
/// </summary>
public class NPC_Interaction : MonoBehaviour
{
    [Header("Dependencies")]
 //   [SerializeField] private GameEvents _gameEvents;


    [Header("Dialogue (optional)")]
    [SerializeField] private string _dialogueKnotName;

    private string silentDialogueKnotName;
    
    public void NPCDialogue() // enters into a dialogue event
    {
     //   if (gameObject != this.gameObject) return; // if the gameObject is not the same as this one, do nothing
        if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {
            
            DialogueEvents.TriggerEnterDialogueEvent(_dialogueKnotName); // begins the NPC's dialgoue at their knotName
            

        } else // the npc will default to ... if there is no dialogue knot 
        {
            DialogueEvents.TriggerEnterDialogueEvent("silentDialogue");
        }

    }
}
