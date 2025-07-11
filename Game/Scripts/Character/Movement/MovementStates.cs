using UnityEngine;

public enum MovementStates // list of different player movement states
{ // different levels on how fast the player will be moving 
    Idle,
    Walking,
    Running,
    Climbing,
    Crouching,
    InAir,

}

[System.Serializable]
[CreateAssetMenu(menuName = "Character/Movement/StateSettings")]
public class StateSettings : ScriptableObject
{
    public MovementStates State;
    public float SpeedMultiplier;
    public float LinearDamping; // amount of air drag
}