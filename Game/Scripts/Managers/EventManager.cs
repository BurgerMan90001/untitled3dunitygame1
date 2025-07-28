using UnityEngine;
// TODO MAYBE MAKE NOT SINGLETON
/// <summary>
/// <br> A singleton</br>
/// </summary>
public class EventManager : MonoBehaviour, IEventManager
{

    [field: SerializeField] public CombatEvents CombatEvents { get; set; }
    [field: SerializeField] public DialogueEvents DialogueEvents { get; set; }
    [field: SerializeField] public DataPersistenceEvents DataPersistenceEvents { get; set; }
    [field: SerializeField] public UserInterfaceEvents UserInterfaceEvents { get; set; }
    [field: SerializeField] public GameTimeEvents GameTimeEvents { get; set; }
    public static EventManager Instance { get; private set; }
    /// <summary>
    /// <br> JUST CREATES AN INSTANCE OF EVENT MANAGER FOR NOW.</br>
    /// </summary>
    public void Inject()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("There is another Event Manager in the scene.");
        }
    }


}
