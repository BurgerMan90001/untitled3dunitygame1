using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue Knot Name")]
    [SerializeField] private string _dialogueKnotName;


    private NPCInteraction _npcInteraction;
    private DialogueData _dialogueData;

    private bool _initialized = false;
    public void Initialize(DialogueData dialogueData)
    {

        _dialogueData = dialogueData;
        //    _npcInteraction = new NPCInteraction(_dialogueKnotName, dialogueData);

        _initialized = true;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    public void Interact(GameObject interactor)
    {
        if (!_initialized)
        {
            Debug.LogWarning("This npc has not been initialized yet!");
            return;
        }
        if (_dialogueData.InDialogue)
        {

            _dialogueData.Events.ContinueDialogue();

        }

        else if (!_dialogueKnotName.Equals("")) // if the knot name is not empty
        {
            Debug.Log(_dialogueKnotName);
            _dialogueData.Events.EnterDialogue(_dialogueKnotName, gameObject);// begins the NPC's dialgoue at their knotName

        }
        else // the npc will default to ... if there is no dialogue knot 
        {
            _dialogueData.Events.EnterDialogue("silentDialogue", gameObject);
        }
    }

    public void EnterCombat()
    {
        DontDestroyOnLoad(gameObject);


    }

}
