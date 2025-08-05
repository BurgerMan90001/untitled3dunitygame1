/*
public enum MovementStates // list of different player movement states
{ // different levels on how fast the player will be moving 
    Idle,
    Walking,
    Running,
    Climbing,
    Crouching,
    InAir,
    Swimming,
}


[System.Serializable]
[CreateAssetMenu(menuName = "Character/Movement/StateSettings")]
public class StateSettings : ScriptableObject
{
    public MovementStates State;
    public float SpeedMultiplier;
    public float LinearDamping; // amount of air drag
}
*/
public struct MovementStates
{
    private const float DefaultSpeedMultiplier = 1f;
    private const float DefaultLinearDamping = 2f;

    public static readonly MovementStates Idle = new MovementStates(DefaultSpeedMultiplier, DefaultLinearDamping);
    public static readonly MovementStates Walking = new MovementStates(DefaultSpeedMultiplier, DefaultLinearDamping);
    public static readonly MovementStates Running = new MovementStates(1.46f, DefaultLinearDamping);
    public static readonly MovementStates Climbing = new MovementStates(0.5f, 0);
    public static readonly MovementStates Crouching = new MovementStates(0.72f, DefaultLinearDamping);
    public static readonly MovementStates InAir = new MovementStates(0.07f, 0);
    //  public static readonly MovementStates Swimming = new MovementStates(1, 0.5); MAYBE 
    public float SpeedMultiplier { get; set; }
    public float LinearDamping { get; set; } // amount of air drag
    public MovementStates(float speedMultiplier, float linearDamping)
    {
        SpeedMultiplier = speedMultiplier;
        LinearDamping = linearDamping;
    }
}