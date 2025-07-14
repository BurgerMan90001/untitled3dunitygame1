using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Combat/CombatData")]
public class CombatData : Data
{
    public Action OnEnterCombat;

    public Action OnExitCombat;







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

}
