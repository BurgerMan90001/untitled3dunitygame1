// TODO MAYBE MAKE UNIVERSAL MANAGER METHODES
/// <summary>
/// <br> For managers and such. Inherits from monobehaviour.</br>
/// <br> for now empty and for singletons </br>
/// <br> NOTES: Register events in onenable or start. unregister in ondestroy.</br>
/// </summary>

public interface IManager { }

public interface IUserInterfaceManager : IManager // Userinterface manager
{
    void Initialise(DataPersistenceEvents dataPersistenceEvents, UserInterfaceEvents userInterfaceEvents, DialogueEvents dialogueEvents, Inventory inventory);
}
public interface IInputManager : IManager // Input manager
{
    void Initialise(DialogueEvents dialogueEvents, CombatEvents combatEvents, GameInput gameInput);
}
public interface IDataPersistenceManager : IManager // Datapersistence manager
{
    void Initialise(DataPersistenceEvents dataPersistenceEvents);
}
public interface IDialogueManager : IManager // Dialougue manager
{
    void Initialise(DialogueEvents dialogueEvents);
}
public interface IGameTimeManager : IManager // Gametime manager
{
    void Initialise(GameTimeEvents gameTimeEvents);
}
public interface ICombatManager : IManager // Combat manager
{
    void Initialise(DialogueEvents dialogueEvents, CombatEvents combatEvents);
}



/*
public abstract class Manager : MonoBehaviour
{
    public abstract void Initialize();

}
*/
