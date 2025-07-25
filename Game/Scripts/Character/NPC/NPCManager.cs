using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private DialogueEvents _dialogueEvents;


    /*
    protected virtual void InitializeNPCS()
    {

        foreach (Transform npc in transform)
        {
            if (npc.gameObject.TryGetComponent(out NPC component))
            {
                component.Initialize(_dialogueEvents);
                npc.gameObject.SetActive(true);
            }
            else
            {
                npc.gameObject.SetActive(false);
                Debug.LogWarning("An npc was disabled because they did not have an NPCInteraction component.");
            }


        }
    }
    */

}

