
using UnityEngine;



public abstract class HurtEffect : ScriptableObject
{
    public HurtType Type;
    public abstract void ApplyEffect(CombatUnit target, CombatUnit attacker, float damage);
}