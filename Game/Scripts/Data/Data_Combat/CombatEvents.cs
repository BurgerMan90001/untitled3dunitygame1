using System;

public class CombatEvents : IEvent
{
    public event Action<CombatUnit> OnEnterCombat;
    public event Action OnExitCombat;
    public event Action<CombatStates> OnCombatStateSwitched;

    

    #region
    /// <summary>
    /// <br> Switches the combat state. </br>
    /// </summary>
    /// <param name="combatState"></param>
    #endregion
    public void SwitchCombatState(CombatStates combatState)
    {
        OnCombatStateSwitched?.Invoke(combatState);

    }

    #region
    /// <summary>
    /// <br> Loads the Combat scene with a combat interface. </br>
    ///  <br> Doesn't do anything if enteredCombat is false. </br>
    /// <br> Triggers the OnEnterCombat event. </br>
    /// </summary>
    #endregion
    public void EnterCombat(CombatUnit enemyUnit)
    {


        OnEnterCombat?.Invoke(enemyUnit);


    }
    #region
    /// <summary>
    /// <br> Triggers the OnExitCombat event. </br>
    /// <br> Loads the previous scene. </br>
    /// </summary>
    #endregion
    public void ExitCombat()
    {
        OnExitCombat?.Invoke();
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