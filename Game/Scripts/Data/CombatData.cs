using System;
using UnityEngine;
/// <summary>
/// 
/// </summary>
[CreateAssetMenu(menuName = "Data/CombatData")]
public class CombatData : Data
{
    public Action OnEnterCombat;

    public Action OnExitCombat;

    public Action OnTurnChanged;





    /// <summary>
    /// <br> Triggers the OnEnterCombat event. </br>
    /// </summary>
    public void EnterCombat()
    {
        Debug.Log("ADKSOPPOAKSDPKOASD");
        
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
