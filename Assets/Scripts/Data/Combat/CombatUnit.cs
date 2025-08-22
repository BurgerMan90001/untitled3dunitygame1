using MyBox;
using UnityEngine;
public class CombatUnit : MonoBehaviour
{
    [Header("Combat Stats")]
    [DisplayInspector] public CombatStats CombatStats;


    public bool Hurt(float damage)
    {
        CombatStats.Health -= damage;
        if (CombatStats.Health <= 0)
        {
            return true;
        }
        return false;

    }

    public void Heal(float value)
    {
        CombatStats.Health += value;
    }
    /*
    public float GetFinalAttackValue()
    {
        return AttackValue * AttackPercent;
    }
    */

    public void ResetStats()
    {
        CombatStats.Health = CombatStats.MaxHealth;
    }
}
