using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// <br> Combat events and data. <br>
/// </summary>

public class CombatData : Data
{



    [Header("HurtEffects")]
    public List<HurtEffect> HurtEffects;

    [Header("Prefabs")]
    public CombatUnit PlayerUnit;
    public CombatUnit EnemyUnit;


    [Header("Spawn Points")]
    [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; } = new Vector3(3f, 4f, 0);
    [field: SerializeField] public Vector3 EnemySpawnPoint { get; private set; } = new Vector3(-3f, 4f, 0);


    [Header("Debugging")]
    [SerializeField] public bool DebugMode = true;


    public Stack<CombatStates> CombatState { get; private set; } = new Stack<CombatStates>();



    public void PushCombatState(CombatStates combatState)
    {
        CombatState.Push(combatState);
    }

    public void CheckIfCombatEntered(GameObject npc, bool combatEntered)
    {
        if (combatEntered)
        {

            /*
            if (npc.TryGetComponent(out CombatUnit combatUnit))
            {
                _combatEvents.EnterCombat(combatUnit);
            }
            else
            {
                Debug.LogWarning("The story's combatEntered value is true, but the npc does not have a CombatUnit component! ");
                return;
            }
            */

        }

    }
    public bool IsPlayerTurn()
    {
        if (CombatState.TryPeek(out CombatStates combatState))
        {
            if (combatState == CombatStates.PlayerTurn)
            {
                return true;
            }
        }
        return false;
    }

    public bool DidWin()
    {
        if (CombatState.TryPeek(out CombatStates combatState))
        {
            if (combatState == CombatStates.Won)
            {
                return true;
            }
            else if (combatState == CombatStates.Lost)
            {
                return false;
            }
            else
            {
                Debug.LogWarning($" Found a combat state of : {combatState}, instead of a win condition.");
            }
        }

        Debug.LogError("Could not find the most recent combat state.");
        return false;
    }
    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }
}


