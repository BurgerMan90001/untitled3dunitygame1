
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : GameInput, IPlayerMovement
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private GeneralStats _stats;
    [SerializeField] private Transform _orientation;

    [Header("MovementStateManager Settings")]
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _globalSpeedMultiplier = 2f;
    [SerializeField] private List<StateSettings> _stateSettings;

    [Header("Crouch Settings")]
    [SerializeField] private float _crouchYScale = 0.5f;
    private float _originalYScale; // the originalYScale is found from transform.parent.localScale.y;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f;

    [Header("Sprint Settings")]
    [SerializeField] private float _staminaRegenRate = 1f;
    [SerializeField] private float _sprintStaminaCost = 1f;
    private const double _buttonDurationThreshold = 0.30d;


    [Header("Ground Check Settings")]
    [SerializeField] private float _groundCheckDistance = 2.5f;
    [SerializeField] private LayerMask _groundMask = 10; // default layermask is whatIsGround
    [SerializeField] private bool _drawDebugRayGround = true;

    [Header("Water Settings")]
    [SerializeField] private Vector3 _upAxis;
    [SerializeField] private float _probeDistance;
    [SerializeField] private LayerMask _probeMask;
    [SerializeField] private bool _drawDebugRayWater = true;

    private const float Y_VELOCITY_THRESHOLD = 0.1f;

    private Vector2 _movementInput;
    private Transform _playerMovementObject;

    private MovementStateManager _movementStateManager;

    private VerticalMovement _verticalMovement;
    private HorizontalMovement _horizontalMovement;
    private IsGrounded _isGrounded;
    private Sprint _sprint;

    private void Awake()
    {


        _playerMovementObject = transform;

        _originalYScale = transform.localScale.y;

        _sprint = new Sprint(_stats);

        _movementStateManager = new MovementStateManager(_rigidBody, _stateSettings, _baseSpeed, _globalSpeedMultiplier);

        _verticalMovement = new VerticalMovement(_playerMovementObject, _movementStateManager);
        _horizontalMovement = new HorizontalMovement(_rigidBody, _movementStateManager);


        _isGrounded = new IsGrounded(_movementStateManager, _horizontalMovement, _playerMovementObject);
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {

        Vector2 newInput = ctx.ReadValue<Vector2>();
        if (newInput != _movementInput) // only update if the input has changed to avoid unnecessary updates
        {
            _movementInput = newInput;
        }
    }
    public void OnJump(InputAction.CallbackContext ctx)
    {
        _verticalMovement.MakeBodyJump(_rigidBody, _jumpForce, _isGrounded.OnGround);

    }
    public void OnSprint(InputAction.CallbackContext ctx)
    {

        if (ctx.started && _isGrounded.OnGround)
        {

            _verticalMovement.UnCrouchBody(_originalYScale, _isGrounded.OnGround);
            _sprint.Run(_movementStateManager);

        }
        else if (ctx.canceled || ctx.duration < _buttonDurationThreshold)
        {
            _sprint.CancelRun(_movementStateManager, _isGrounded.OnGround);
        }

    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _verticalMovement.CrouchBody(_rigidBody, _crouchYScale, _isGrounded.OnGround);
        }
        else if (ctx.canceled)
        {
            _verticalMovement.UnCrouchBody(_originalYScale, _isGrounded.OnGround);
        }
    }
    private void FixedUpdate()
    {
        _horizontalMovement.MoveRigidBody(_movementInput, _isGrounded, _orientation);


        //  _isGrounded.CheckIfInWater(_probeDistance, _probeMask, _drawDebugRayWater));

        if (Math.Abs(_rigidBody.linearVelocity.y) > Y_VELOCITY_THRESHOLD)
        {
            _isGrounded.CheckIfGrounded(_verticalMovement.IsCrouched, _groundCheckDistance, _groundMask, _drawDebugRayGround);
        }
    }
    private void Update()
    {
        _sprint.UpdateStamina(_movementStateManager, _sprintStaminaCost, _staminaRegenRate, _isGrounded.OnGround);
    }
}

