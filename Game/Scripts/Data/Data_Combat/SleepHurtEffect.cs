using UnityEngine;
//MAYBE

[CreateAssetMenu(menuName = "Combat/HurtEffect/SleepHurtEffect")]
public class SleepHurtEffect : HurtEffect
{

    public override void ApplyEffect(CombatUnit target, CombatUnit attacker, float damage)
    {
        target.CombatStats.Hurt(damage);

    }
}
