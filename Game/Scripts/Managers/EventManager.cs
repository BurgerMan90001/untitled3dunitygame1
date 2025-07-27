using UnityEngine;
// TODO MAYBE MAKE NOT SINGLETON
/// <summary>
/// <br> A singleton</br>
/// </summary>
public class EventManager : MonoBehaviour
{

    public CombatEvents CombatEvents;
    public DialogueEvents DialogueEvents;
    public DataPersistenceEvents DataPersistenceEvents;
    public UserInterfaceEvents UserInterfaceEvents;
    public GameTimeEvents GameTimeEvents;
    public static EventManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("There is another Event Manager in the scene.");
        }

    }
}
