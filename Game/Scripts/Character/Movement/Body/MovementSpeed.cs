using System.Collections.Generic;
using UnityEngine;


public class MovementStateManager
{
  
    private Rigidbody _rigidBody;
    
    private float _baseSpeed;
    private float _globalSpeedMultiplier = 2f;
    private List<StateSettings> _stateSettings;

    public MovementStateManager(Rigidbody rigidBody, List<StateSettings> stateSettings, float baseSpeed, float globalSpeedMultiplier)
    {
        _rigidBody = rigidBody;
        _stateSettings = stateSettings;
        _baseSpeed = baseSpeed;
        _globalSpeedMultiplier = globalSpeedMultiplier;
    }


    public MovementStates MovementState { get; private set; }
    
    public float GetCurrentSpeed()
    {
        foreach (var stateSetting in _stateSettings)
        {
            if (MovementState == stateSetting.State) // searches list for each movement state, if the current movementstate is equal to that, then apply the multiplier
            {
                
                SetLinearDamping(stateSetting); // set the linear damping to the one in the state setting
                return (_baseSpeed * _globalSpeedMultiplier) * stateSetting.SpeedMultiplier;
            }
        }
        return _baseSpeed * _globalSpeedMultiplier; // apply default speed if no state matches
    }

    public void SetLinearDamping(StateSettings stateSetting)
    {
        if (_rigidBody.linearDamping != stateSetting.LinearDamping) // if the linear damping is not equal to the one in the state setting, then apply the linear damping
        {
            _rigidBody.linearDamping = stateSetting.LinearDamping; // apply the linear damping to the rigidbody
        }
    }
    public void SetMovementState(MovementStates newState)
    {
        if (MovementState == newState) return; // if the movement state has actually changed, then change it or cancel
        
        MovementState = newState;

        
    }
    


}

