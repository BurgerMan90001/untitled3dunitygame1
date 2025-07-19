using UnityEngine;
#region 
/// <summary>
/// <br> Weakness effect. </br>
///< br> Applies an percent of weakness that reduces attack power. </br>
/// </summary>
#endregion

[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class SpookHurtEffect : HurtEffect
{
    [SerializeField] private float WeaknessPercent = 0.25f; // one quarter


    public override void ApplyEffect(CombatStats target, CombatStats attacker, float damage)
    {

        target.Hurt(damage);
        target.AttackPercent -= WeaknessPercent;

    }
}