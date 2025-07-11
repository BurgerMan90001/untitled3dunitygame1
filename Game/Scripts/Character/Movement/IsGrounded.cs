
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;




/// <summary> 
/// checks if the player is grounded by casting a ray or sphere downwards.





public class IsGrounded
{
    
    private readonly float _maxSlopeAngle = 45f; // 45 degrees
   
    private MovementStateManager _movementStateManager;
    private Transform _body;
    private HorizontalMovement _horizontalMovement;

    private RaycastHit _slopeHit;
    private Vector3 _slopeMoveDirection;

    public bool OnGround { get; private set; }
    private bool _isGroundedThisFrame;
    public bool OnSlope { get; private set; }
    private bool _onSlopeThisFrame;
    
    private float _slopeAngle;

    private float _groundCheckTimer;

    public IsGrounded(MovementStateManager MovementStateManager, HorizontalMovement horizontalMovement, Transform body)
    {
        
        _movementStateManager = MovementStateManager;
        _body = body;

        

    }
    
    private void UpdateMovementState(bool crouching) // linear damping is air drag, and the linear damping will change if the grounded state changes.
    {
        
        if (OnGround)
        {
            if (crouching) 
            {
                _movementStateManager.SetMovementState(MovementStates.Crouching);
            }
            else
            {
                _movementStateManager.SetMovementState(MovementStates.Walking);
            }

        }
        else
        {
            _movementStateManager.SetMovementState(MovementStates.InAir);
           
        }
    }
    

    public void CheckIfGrounded(bool crouching,float groundCheckDistance, LayerMask groundMask, bool drawDebugRay)
    {
        _groundCheckTimer += Time.fixedDeltaTime;
        if (_groundCheckTimer < 0.1f) // check every 0.1 seconds
        {
            return;
        }
        _isGroundedThisFrame = Physics.Raycast(_body.position, Vector3.down, out _slopeHit, groundCheckDistance, groundMask);

        if (_isGroundedThisFrame) // if the raycast hits something
        {
            _slopeAngle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            _onSlopeThisFrame = _slopeAngle < _maxSlopeAngle && _slopeAngle != 0;
            // if the slope angle is less than the max slope angle and not zero, then the player is on a slope
        }


        if (OnGround != _isGroundedThisFrame) // if the player's grounded state changes
        {

            OnGround = _isGroundedThisFrame;

            UpdateMovementState(crouching);

        }
        
        if (OnSlope != _onSlopeThisFrame)
        {
            OnSlope = _onSlopeThisFrame;
            
        }
        
        
        if (drawDebugRay)
        {
            Debug.DrawRay(_body.position, Vector3.down * groundCheckDistance, Color.red);
        }
    }
    public Vector3 GetSlopeMoveDirection(Vector3 horizontalVelocity)
    {
        
        return Vector3.ProjectOnPlane(horizontalVelocity, _slopeHit.normal).normalized;
    }
}
