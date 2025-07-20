using UnityEngine;

/// <summary>
/// <br> Stats that are used during combat. </br>
/// </summary>
[CreateAssetMenu(menuName = "Stats/CombatStats")]
public class CombatStats : Stats
{
    [Header("Data")]
 //   [SerializeField] private CombatData _combatData;
    [SerializeField] private Inventory _inventory;

    public float MaxHealth {get; private set;}
    public float Health {get; private set;} // used in battle


    public float Damage = 1; 
    /*
    public float AttackValue = 1; //MAYBE
    public float AttackPercent; // e.g. 1 is 100% and 0.5 is 50%
    */

    public float BlockValue; // MAYBE
    #region
    /// <summary>
    /// <br> Reduces the target's health. </br>
    /// <br> Returns true if its a kill, false if not. </br>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    #endregion
    public bool Hurt(float damage) 
    {
        Health -= damage;
        if (Health <= 0)
        {
            return true;
        }    
        return false;
        
    }

    public void Heal(float value)
    {
        Health += value;
    }
    /*
    public float GetFinalAttackValue()
    {
        return AttackValue * AttackPercent;
    }
    */
    public override void LoadData(GameData data)
    {
        MaxHealth = data.MaxHealth;
    }

    public override void SaveData(GameData data)
    {
        data.MaxHealth = MaxHealth;
    }
}
