using UnityEngine;


[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class RainbowHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(CombatStats target, CombatStats attacker, float damage)
    {
        target.Hurt(damage);


    }
}

