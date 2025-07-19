using UnityEngine;


[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class RainbowHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(CombatUnit target, CombatUnit attacker, float damage)
    {
     //   target.CombatStats.Hurt(damage);


    }
}

