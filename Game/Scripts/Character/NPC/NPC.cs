using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue Knot Name")]
    [SerializeField] private string _dialogueKnotName;



    // private DialogueEvents _dialogueEvents;

    //  private bool _initialized = false;

    private readonly bool _debugMode = false;

    /*
    public void Initialize(DialogueEvents dialogueEvents)
    {

        _dialogueEvents = dialogueEvents;

        _initialized = true;
    }
    */
    public void Interact(GameObject interactor)
    {
        /*
        if (!_initialized)
        {
            Debug.LogWarning("This npc has not been initialized yet!");
            return;
        }
        */

        if (EventManager.Instance.DialogueEvents.InDialogue)
        {
            if (_debugMode)
            {
                Debug.Log("CONT");
            }
            EventManager.Instance.DialogueEvents.ContinueDialogue();

        }


        else if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {
            if (_debugMode)
            {
                Debug.Log("ENTER");
            }

            EventManager.Instance.DialogueEvents.EnterDialogue(_dialogueKnotName, gameObject);// begins the NPC's dialgoue at their knotName

        }
        else // the npc will default to ... if there is no dialogue knot 
        {
            EventManager.Instance.DialogueEvents.EnterDialogue("silentDialogue", gameObject);
        }
    }

    public void EnterCombat()
    {
        DontDestroyOnLoad(gameObject);


    }

}
