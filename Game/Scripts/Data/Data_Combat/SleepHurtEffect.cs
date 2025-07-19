using UnityEngine;
//MAYBE

[CreateAssetMenu(menuName = "Combat/HurtEffect/SleepHurtEffect")]
public class SleepHurtEffect : HurtEffect
{
    //  public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(CombatStats target, CombatStats attacker, float damage)
    {
        target.Hurt(damage);

    }
}
