using System;
using UnityEngine;
/// <summary>
/// <br> Combat events and data. <br>
/// </summary>
[CreateAssetMenu(menuName = "Data/CombatData")]
public class CombatData : Data
{
    public event Action OnEnterCombat;
    public event Action OnExitCombat;
    
    public event Action OnTurnChanged;



    /// <summary>
    /// <br> Triggers the OnEnterCombat event. </br>
    /// </summary>
    public void EnterCombat()
    {   
        OnEnterCombat?.Invoke();
      
    }
    /// <summary>
    /// <br> Triggers the OnExitCombat event. </br>
    /// </summary>
    public void ExitCombat()
    {
        OnExitCombat?.Invoke();
    }

    /// <summary>
    /// <br> Triggers the OnTurnChanged  event. </br>
    /// </summary>
    public void ChangeTurn() 
    {
        OnTurnChanged?.Invoke();

    }
}
