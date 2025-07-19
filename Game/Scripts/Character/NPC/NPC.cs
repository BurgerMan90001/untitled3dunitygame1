using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Dialogue Knot Name")]
    [SerializeField] private string _dialogueKnotName;


    private NPCInteraction _npcInteraction;
    private bool _initialized = false;
    public void Initialize(DialogueData dialogueData)
    {


        _npcInteraction = new NPCInteraction(_dialogueKnotName, dialogueData);

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
        _npcInteraction.Interact(interactor);
    }

    public void EnterCombat()
    {
        DontDestroyOnLoad(gameObject);


    }

}
