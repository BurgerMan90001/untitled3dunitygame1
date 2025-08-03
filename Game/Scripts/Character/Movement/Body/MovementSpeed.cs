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

        SetLinearDamping();
        return _baseSpeed * _globalSpeedMultiplier * MovementState.SpeedMultiplier;
    }

    public void SetLinearDamping()
    {

        if (_rigidBody.linearDamping != MovementState.LinearDamping) // if the linear damping is not equal to the one in the state setting, then apply the linear damping
        {

            _rigidBody.linearDamping = MovementState.LinearDamping; // apply the linear damping to the rigidbody
        }
    }
    public void SetMovementState(MovementStates newState)
    {
        if (MovementState.SpeedMultiplier == newState.SpeedMultiplier
            || MovementState.LinearDamping == newState.LinearDamping) return; // if the movement state has actually changed, then change it or cancel

        MovementState = newState;


    }



}

