using UnityEngine;

public abstract class HurtEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target, GameObject attacker, float damage);
}


/// <summary>
/// <br> Lifesteal effect. </br>
/// </summary>
[CreateAssetMenu(menuName = "Combat/HurtEffect/LoveHurtEffect")]
public class LoveHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;

        attacker.GetComponent<CombatStats>().Health += (damage * HealPercent);
    }
}
/// <summary>
/// <br> Weakness effect. </br>
/// </summary>
[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class SpookHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;

        attacker.GetComponent<CombatStats>().Health += (damage * HealPercent);
    }
}