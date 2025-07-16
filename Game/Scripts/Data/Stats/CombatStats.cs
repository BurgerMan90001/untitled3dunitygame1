using UnityEngine;


/// <summary>
/// <br> Stats that are used during battle. </br>
/// </summary>
[CreateAssetMenu(menuName = "Character/Stats/CombatStats")]
public class CombatStats : ScriptableObject, IDataPersistence
{
    public DynamicInventory Inventory;

    public float MaxHealth;
    public float Health; // used in battle

    public float AttackValue = 1;
    public float AttackPercent; // e.g. 1 is 100% and 0.5 is 50%


    public float BlockValue; // MAYBE

    public float GetFinalAttackValue()
    {
        return AttackValue * AttackPercent;
    }

    public void LoadData(GameData data)
    {
        MaxHealth = data.MaxHealth;


    }

    public void SaveData(GameData data)
    {
        data.MaxHealth = MaxHealth;
    }
}
