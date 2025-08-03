
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

/// <summary> 
/// checks if the player is grounded by casting a ray or sphere downwards.

public class IsGrounded
{

    private const float MAX_SLOPE_ANGLE = 45f; // 45 degrees

    private readonly MovementStateManager _movementStateManager;
    private readonly Transform _body;
    private readonly HorizontalMovement _horizontalMovement;

    private RaycastHit _slopeHit;
    private Vector3 _slopeMoveDirection;

    public bool OnGround { get; private set; }
    private bool _isGroundedThisFrame;
    public bool OnSlope { get; private set; }
    private bool _onSlopeThisFrame;

    public bool InWater { get; private set; }

    private float _slopeAngle;

    private float _groundCheckTimer;


    private WaterSurface waterSurface;
    WaterSearchParameters searchParameters = new WaterSearchParameters();
    WaterSearchResult searchResult = new WaterSearchResult();

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


    public void CheckIfGrounded(bool crouching, float groundCheckDistance, LayerMask groundMask, bool drawDebugRay)
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
            _onSlopeThisFrame = _slopeAngle < MAX_SLOPE_ANGLE && _slopeAngle != 0;
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
