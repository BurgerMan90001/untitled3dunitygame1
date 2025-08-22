
using UnityEngine;
#region
/// <summary>
/// Handles horizontal movement for the player using keyboard input.
/// </summary>
#endregion

public class HorizontalMovement
{

    private readonly Vector3 _flatHorizontalVelocity; // velocities for calculating in the limitspeed() function
    private Vector3 _limitedHorizontalVelocity;

    private Vector3 _horizontalVelocity;


    private const float SlopeSpeedMultiplier = 1.25f;

    private readonly Rigidbody _rigidBody;
    private readonly MovementStateManager _movementStateManager;


    public HorizontalMovement(Rigidbody rigidBody, MovementStateManager movementStateManager)
    {

        _movementStateManager = movementStateManager;
        _rigidBody = rigidBody;

    }

    public void MoveRigidBody(Vector2 movementInput, IsGrounded isGrounded, Transform orientation)
    {
        if (movementInput == Vector2.zero) return; // if no input, do not move

        _horizontalVelocity = (orientation.forward * movementInput.y) + (orientation.right * movementInput.x);

        WalkingMovement(isGrounded);
        /*
        switch (_movementStateManager.MovementState)
        {
            case MovementStates.Walking:
                WalkingMovement(isGrounded);
                break;

            case MovementStates.Climbing.:
                LadderMovement();
                break;
            default:
                WalkingMovement(isGrounded);
                break;
        }
        */

    }

    private void WalkingMovement(IsGrounded isGrounded)
    {

        if (isGrounded.OnSlope)
        {

            _rigidBody.AddForce(_movementStateManager.GetCurrentSpeed()
                * SlopeSpeedMultiplier
                * isGrounded.GetSlopeMoveDirection(_horizontalVelocity), ForceMode.Force);
        }
        else
        {
            _rigidBody.AddForce(_horizontalVelocity.normalized * _movementStateManager.GetCurrentSpeed(), ForceMode.Force);
        }

    }
    private void LadderMovement()
    {

        _rigidBody.useGravity = false;
        _horizontalVelocity.y = -_horizontalVelocity.z;
        _horizontalVelocity.z = 0f;
        _rigidBody.linearVelocity = _horizontalVelocity.normalized * _movementStateManager.GetCurrentSpeed();

    }


    private void WaterMovement()
    {

    }
}






#region
/*
    public  void LimitSpeed(Rigidbody rigidBody, MovementStateManager MovementStateManager, bool exitingSlope)
    {

        if (OnSlope() && !exitingSlope)
        {
            if (rigidBody.linearVelocity.magnitude > MovementStateManager.GetCurrentSpeed())
            {
                rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * MovementStateManager.GetCurrentSpeed();
            }
        }
        else
        {

            _flatHorizontalVelocity = new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);

            if (_flatHorizontalVelocity.magnitude > MovementStateManager.GetCurrentSpeed())
            {
                _limitedHorizontalVelocity = _flatHorizontalVelocity.normalized * MovementStateManager.GetCurrentSpeed();
                rigidBody.linearVelocity = new Vector3(_limitedHorizontalVelocity.x, rigidBody.linearVelocity.y, _limitedHorizontalVelocity.z);
            }
        }
    }
    */
#endregion