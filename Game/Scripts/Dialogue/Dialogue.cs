using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "Character/Dialogue")]
#region
/// <summary>
/// Contains the dialogue that the UI_Dialogue will display
/// </summary>
#endregion
public class Dialogue : ScriptableObject
{
    
    public string CurrentDialogue;
    

    public List<Choice> Choiceslist;
}
