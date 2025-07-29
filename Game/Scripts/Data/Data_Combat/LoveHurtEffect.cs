using UnityEngine;

#region 
/// <summary>
/// <br> Lifesteal effect. </br>
/// <br> 25% of attack damage heals the attacker. <br>
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Combat/HurtEffect/LoveHurtEffect")]
public class LoveHurtEffect : HurtEffect
{
    [SerializeField] private float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(CombatUnit target, CombatUnit attacker, float damage)
    {
        //    target.Hurt(damage);


        attacker.Heal(damage * HealPercent);
    }
}