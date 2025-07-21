// TODO SEPARATE DATA FROM EVENTS IMPLEMENT THEM WITH EVENT CLASS
/*
public class Event : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void TriggerEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventTriggered();
        }
    }

    public void AddListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}

*/
public interface IEvent
{

}
