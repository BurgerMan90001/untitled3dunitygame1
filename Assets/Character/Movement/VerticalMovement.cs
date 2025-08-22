
using UnityEngine;


#region
/// <summary>
/// Handles vertical movement (gravity and jumping) for the player.
/// </summary>
#endregion
public class VerticalMovement
{

    private readonly MovementStateManager _movementStateManager;
    private readonly Transform _body;


    public bool IsCrouched { get; private set; }

    public VerticalMovement(Transform body, MovementStateManager MovementStateManager)
    {
        _body = body;
        _movementStateManager = MovementStateManager;
    }


    public void MakeBodyJump(Rigidbody rigidBody, float jumpForce, bool isGrounded)
    {
        if (isGrounded)
        {   //reset y velocity to 0f 
            rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);
            rigidBody.AddForce(_body.up * jumpForce, ForceMode.Impulse);


        }
    }
    public void CrouchBody(Rigidbody rigidBody, float crouchYScale, bool isGrounded)
    {

        _body.localScale = new Vector3(_body.localScale.x, crouchYScale, _body.localScale.z);

        if (isGrounded)
        {
            _movementStateManager.SetMovementState(MovementStates.Crouching);
            rigidBody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        IsCrouched = true;
    }
    public void UnCrouchBody(float originalYScale, bool isGrounded)
    {
        if (isGrounded)
        {
            _movementStateManager.SetMovementState(MovementStates.Walking);
        }

        _body.localScale = new Vector3(_body.localScale.x, originalYScale, _body.localScale.z);
        IsCrouched = false;
    }





}