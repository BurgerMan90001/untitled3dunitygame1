// TODO MAYBE MAKE UNIVERSAL MANAGER METHODES 
// TODO SIMPLIFY
//joipgfdjiosdfoipghsdpokfgdpox
/// <summary>
/// <br> For managers and such. Inherits from monobehaviour.</br>
/// <br> for now empty and for singletons </br>
/// <br> NOTES: Register events in onenable or start. unregister in ondestroy.</br>
/// </summary>

public interface IManager
{

}

public interface IUserInterfaceManager : IManager // Userinterface manager
{
    void Inject(DataPersistenceEvents dataPersistenceEvents, UserInterfaceEvents userInterfaceEvents, DialogueEvents dialogueEvents, Inventory inventory);
}
public interface IInputManager : IManager // Input manager
{
    void Inject(DialogueEvents dialogueEvents, CombatEvents combatEvents, GameInput gameInput, UserInterfaceEvents userInterfaceEvents);
}
public interface IDataPersistenceManager : IManager // Datapersistence manager
{
    void Inject(DataPersistenceEvents dataPersistenceEvents);
}
public interface IDialogueManager : IManager // Dialougue manager
{
    void Inject(DialogueEvents dialogueEvents);
}
public interface IGameTimeManager : IManager // Gametime manager
{
    void Inject(GameTimeEvents gameTimeEvents);
}
public interface ICombatManager : IManager // Combat manager
{
    void Inject(DialogueEvents dialogueEvents, CombatEvents combatEvents);
}

public interface IEventManager : IManager
{
    CombatEvents CombatEvents { get; set; }
    DialogueEvents DialogueEvents { get; set; }
    DataPersistenceEvents DataPersistenceEvents { get; set; }
    UserInterfaceEvents UserInterfaceEvents { get; set; }
    GameTimeEvents GameTimeEvents { get; set; }
    void Inject();
}


/*
public abstract class Manager : MonoBehaviour
{
    public abstract void Initialize();

}
*/
