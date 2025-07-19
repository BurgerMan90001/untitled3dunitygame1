
using UnityEngine;


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

    Heat,
    Cool,

    Rust,

}


public abstract class HurtEffect : ScriptableObject
{
    public HurtType Type;
    public abstract void ApplyEffect(CombatStats target, CombatStats attacker, float damage);
}


