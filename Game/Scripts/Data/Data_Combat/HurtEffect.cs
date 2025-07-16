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

     //   attacker.GetComponent<CombatStats>().Health += (damage * HealPercent);
    }
}

[CreateAssetMenu(menuName = "Combat/HurtEffect/SleepHurtEffect")]

public class SleepkHurtEffect : HurtEffect
{
    public float HealPercent = 0.25f; // one quarter
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
