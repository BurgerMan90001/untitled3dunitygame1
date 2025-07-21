using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// <br> Combat events and data. <br>
/// <br> Initiates the combat. </br>
/// </summary>
[CreateAssetMenu(menuName = "Data/CombatData")]
public class CombatData : Data
{
    [Header("HurtEffects")]
    public List<HurtEffect> HurtEffects;

    [Header("Prefabs")]
    public CombatUnit PlayerUnit;
    public CombatUnit EnemyUnit;


    [Header("Data")]

    [Header("Settings")]


    [Header("Spawn Points")]
    [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; } = new Vector3(3f, 4f, 0);
    [field: SerializeField] public Vector3 EnemySpawnPoint { get; private set; } = new Vector3(-3f, 4f, 0);


    [Header("Debugging")]
    [SerializeField] public bool DebugMode = true;


    public CombatEvents Events { get; private set; } = new CombatEvents();


    [field: SerializeField] public CombatStates CombatState { get; private set; }


    public void SwitchCombatState(CombatStates combatState)
    {
        CombatState = combatState;
        Events.SwitchCombatState(CombatState);
    }

    public void CheckIfCombatEntered(GameObject npc, bool combatEntered)
    {
        if (combatEntered)
        {

            if (npc.TryGetComponent(out CombatUnit combatUnit))
            {
                Events.EnterCombat(combatUnit);
            }
            else
            {
                Debug.LogWarning("The story's combatEntered value is true, but the npc does not have a CombatUnit component! ");
                return;
            }

        }

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


