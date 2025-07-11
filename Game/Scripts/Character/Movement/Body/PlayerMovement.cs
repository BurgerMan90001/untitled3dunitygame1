
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private SO_Stats _stats;
    [SerializeField] private Transform _orientation;

    [Header("MovementStateManager Settings")]
    [SerializeField] private float _baseSpeed;
    [SerializeField] private float _globalSpeedMultiplier = 2f;
    [SerializeField] private List<StateSettings> _stateSettings;

    [Header("InputActionReferences")]
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _sprintAction;
    [SerializeField] private InputActionReference _jumpAction;
    [SerializeField] private InputActionReference _crouchAction;

    [Header("Crouch Settings")]
    [SerializeField] private float _crouchYScale = 0.5f;
    private float _originalYScale; // the originalYScale is found from transform.parent.localScale.y;

    [Header("Jump Settings")]
    [SerializeField] private float _jumpForce = 5f;

    [Header("Sprint Settings")]
    [SerializeField] private float _maxStamina = 10f;
    [SerializeField] private float _staminaRegenRate = 1f;
    [SerializeField] private float _sprintStaminaCost = 1f;
    [SerializeField] private double _buttonDurationThreshold = 0.30d;


    [Header("Ground Check Settings")]
    [SerializeField] private readonly float Y_VELOCITY_THRESHOLD = 0.1f;
    [SerializeField] private float _groundCheckDistance = 2.5f;
    [SerializeField] private LayerMask _groundMask = 10; // default layermask is whatIsGround
    [SerializeField] private bool _drawDebugRay = true;

    private Vector2 _movementInput;
    private bool _sprintButtonHeld;
    private Transform _playerMovementObject;

    private MovementStateManager _movementStateManager;

    private VerticalMovement _verticalMovement;
    private HorizontalMovement _horizontalMovement;
    private IsGrounded _isGrounded;
    private Sprint _sprint;

    private Vector2 _horizontalVelocity;


    private void Awake()
    {
        _playerMovementObject = transform;

        
        _sprint = new Sprint(_stats);

        _movementStateManager = new MovementStateManager(_rigidBody, _stateSettings, _baseSpeed, _globalSpeedMultiplier);

        _verticalMovement = new VerticalMovement(_playerMovementObject, _movementStateManager);
        _horizontalMovement = new HorizontalMovement( _rigidBody, _movementStateManager, _orientation);


        _isGrounded = new IsGrounded(_movementStateManager, _horizontalMovement, _playerMovementObject);
    }
    private void Start()
    {
        _originalYScale = transform.parent.localScale.y;
        
    }


    private void OnEnable()
    {
        _moveAction.action.Enable();
        _sprintAction.action.Enable();
        _crouchAction.action.Enable();
        _jumpAction.action.Enable();

        _moveAction.action.started += OnMove;
        _moveAction.action.performed += OnMove;
        _moveAction.action.canceled += OnMove;

        _sprintAction.action.started += OnSprint;
        _sprintAction.action.canceled += OnSprint;

        _crouchAction.action.started += OnCrouch;
        _crouchAction.action.canceled += OnCrouch;

        _jumpAction.action.started += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _moveAction.action.started -= OnMove;
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled += OnMove;

        _sprintAction.action.started -= OnSprint;
        _sprintAction.action.canceled -= OnSprint;

        _crouchAction.action.started -= OnCrouch;
        _crouchAction.action.canceled -= OnCrouch;

        _jumpAction.action.started -= OnJumpPerformed;


        _moveAction.action.Disable();
        _sprintAction.action.Disable();
        _crouchAction.action.Disable();
        _jumpAction.action.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 newInput = ctx.ReadValue<Vector2>();
        if (newInput != _movementInput) // only update if the input has changed to avoid unnecessary updates
        {
            _movementInput = newInput;
        }
    }
    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        _verticalMovement.MakeBodyJump(_rigidBody, _jumpForce, _isGrounded.OnGround);
        
    }
    private void OnSprint(InputAction.CallbackContext ctx)
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
    
    private void OnCrouch(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _verticalMovement.CrouchBody(_rigidBody, _crouchYScale,_isGrounded.OnGround);
        } else if (ctx.canceled)
        {
            _verticalMovement.UnCrouchBody(_originalYScale, _isGrounded.OnGround);
        }
    }
    private void FixedUpdate()
    {
        _horizontalMovement.MoveRigidBody(_movementInput, _isGrounded);
        
        if (Math.Abs(_rigidBody.linearVelocity.y) > Y_VELOCITY_THRESHOLD)
        {
            _isGrounded.CheckIfGrounded(_verticalMovement.IsCrouched, _groundCheckDistance, _groundMask, _drawDebugRay);
        }
        
        
        
    }
    private void Update()
    {
        _sprint.UpdateStamina(_movementStateManager, _sprintStaminaCost, _staminaRegenRate, _isGrounded.OnGround);
    }
}

