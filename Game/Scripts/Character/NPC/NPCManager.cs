using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private DialogueData _dialogueData;
    private void Awake()
    {
        
    }

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        InitializeNPCS();
    }
    private void OnDisable()
    {
        
    }

    /// <summary>
    /// <br> Injects all npcs with depndancies. </br>
    /// </summary>
    private void InitializeNPCS() 
    {
        if (_dialogueData == null)
        {
            Debug.LogError("The DialogueData is null!");
            return;
        }
        foreach (Transform npc in transform)
        {
            if (npc.gameObject.TryGetComponent(out NPCInteraction component))
            {
                component.Initialize(_dialogueData);
                npc.gameObject.SetActive(true);
            } else
            {
                npc.gameObject.SetActive(false);
            }

                
        }
    }
}
