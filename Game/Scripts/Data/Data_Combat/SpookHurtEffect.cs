using UnityEngine; //TODO IMPLEMENT
#region 
/// <summary>
/// <br> Weakness effect. </br>
///< br> Applies an percent of weakness that reduces attack power. </br>
/// </summary>
#endregion

[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class SpookHurtEffect : HurtEffect
{
    [SerializeField] private float _weaknessPercent = 0.25f; // one quarter


    public override void ApplyEffect(CombatUnit target, CombatUnit attacker, float damage)
    {
        

    }
}