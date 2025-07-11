using System;
/// <summary>
/// A type of game event that will trigger EnterDialogue() in DialogueManager
/// </summary>
public static class DialogueEvents
{
    public static event Action<string> OnEnterDialogue;
    
    
    public static void TriggerEnterDialogueEvent(string knotName)
    {
        OnEnterDialogue?.Invoke(knotName); // null check
    }
    

    
}
