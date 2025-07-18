using UnityEngine;


/// <summary>
/// <br> Stats that are used during combat. </br>
/// </summary>
[CreateAssetMenu(menuName = "Character/Stats/CombatStats")]
public class CombatStats : ScriptableObject, IDataPersistence
{
    [Header("Data")]
    [SerializeField] private CombatData _combatData;

    public Inventory Inventory;

    public float MaxHealth {get; private set;}
    public float Health {get; private set;} // used in battle

    public float AttackValue = 1;
    public float AttackPercent; // e.g. 1 is 100% and 0.5 is 50%


    public float BlockValue; // MAYBE


    public void Hurt(float value) 
    {
        float newHealthValue = Health -= value;
        if (newHealthValue <= 0) 
        {
            _combatData.ExitCombat();

        }

    }

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
