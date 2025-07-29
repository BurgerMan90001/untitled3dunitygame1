using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Events/CombatEvents")]
public class CombatEvents : Event
{
    public event Action<CombatUnit> OnEnterCombat;
    public event Action<CombatStates> OnExitCombat;


    #region
    /// <summary>
    /// <br> It first triggers the OnEnterCombat event. </br>
    /// <br> Then it loads the combat scene </br>
    /// </summary>
    #endregion
    public void EnterCombat(CombatUnit enemyUnit)
    {
        OnEnterCombat?.Invoke(enemyUnit);

        //    SceneLoader.LoadScene(SceneLoadingSettings.Combat);
    }
    #region
    /// <summary>
    /// <br> Triggers the OnExitCombat event. </br>
    /// <br> Loads the previous scene. </br>
    /// </summary>
    #endregion
    public void ExitCombat(CombatStates combatState)
    {
        //   SceneLoader.LoadScene(SceneLoadingSettings.City);

        OnExitCombat?.Invoke(combatState);
    }
}


/*
    /// <summary>
    /// <br> Triggers the OnTurnChanged  event. </br>
    /// </summary>
    public void ChangeTurn() 
    {
        OnTurnChanged?.Invoke();

        if (_debug)
        {
            Debug.Log("CHANGED TURNS");
        }

    }
    */