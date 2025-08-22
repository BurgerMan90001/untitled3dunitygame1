using UnityEngine;


public class MovementStateManager
{

    private readonly Rigidbody _rigidBody;
    private readonly float _baseSpeed;
    private readonly float _globalSpeedMultiplier = 2f;

    public MovementStateManager(Rigidbody rigidBody, float baseSpeed, float globalSpeedMultiplier)
    {
        _rigidBody = rigidBody;
        _baseSpeed = baseSpeed;
        _globalSpeedMultiplier = globalSpeedMultiplier;
    }

    public MovementStates MovementState { get; private set; }

    public float GetCurrentSpeed()
    {


        return _baseSpeed * _globalSpeedMultiplier * MovementState.SpeedMultiplier;
    }

    public void SetMovementState(MovementStates newState)
    {
        // if the movement state has not changed, then do not change it
        if (MovementState.SpeedMultiplier == newState.SpeedMultiplier // MAYBE BETTER STRUCT COMAPRISON
            && MovementState.LinearDamping == newState.LinearDamping) return; // if the movement state has actually changed, then change it or cancel

        MovementState = newState;
        _rigidBody.SetLinearDamping(MovementState.LinearDamping); // change linear damping if it has changed

    }



}

