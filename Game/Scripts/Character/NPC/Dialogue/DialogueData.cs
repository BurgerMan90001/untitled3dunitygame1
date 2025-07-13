
using Ink.Runtime;
using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// A type of game event that will trigger EnterDialogue() in DialogueManager
/// </summary>
[CreateAssetMenu(menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    public event Action<string> OnEnterDialogue;

    public event Action OnExitDialogue;
    [field: SerializeField] public string DialogueLine { get; internal set; }


    public List<Choice> Choiceslist { get; internal set; }
    public void EnterDialogue(string knotName)
    {
        OnEnterDialogue?.Invoke(knotName); // null check
    }
    public void ExitDialogue()
    {
        OnExitDialogue?.Invoke();
    }

    
}
