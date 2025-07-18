using UnityEngine;

public abstract class HurtEffect : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target, GameObject attacker, float damage);
}

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
    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;

        attacker.GetComponent<CombatStats>().Health += (damage * HealPercent);
    }
}
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


    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;
        target.GetComponent<CombatStats>().AttackPercent -= WeaknessPercent;

    }
}

[CreateAssetMenu(menuName = "Combat/HurtEffect/SleepHurtEffect")]

public class SleepkHurtEffect : HurtEffect
{
  //  public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;


    }
}

[CreateAssetMenu(menuName = "Combat/HurtEffect/SpookHurtEffect")]
public class RainbowHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
    public override void ApplyEffect(GameObject target, GameObject attacker, float damage)
    {
        target.GetComponent<CombatStats>().Health -= damage;

    
    }
}



/// <summary>
/// <br> MICKEY OVERPOWERD </br>
/// </summary>
public enum HurtType 
{
    
    Physical,
    Stun,
    

    /*
    Cold,
    Fire, 
    Electric,
    Magic,
    */
        
    Love, // lifesteal
    Spook, // weakness
    Bubble, // bubble


    // MAYBE
    Nature,
    Rainbow, // damage over time
    Sleep,
}
