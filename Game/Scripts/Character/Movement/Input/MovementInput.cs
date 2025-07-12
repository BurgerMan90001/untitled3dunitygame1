using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
[CreateAssetMenu(menuName = "Input/MovementInput")]
public class MovementInput : ScriptableObject
{
    public bool LookEnabled { get; private set; }
    public bool Enabled { get; private set; }

    [Header("InputActionReferences")]
    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _sprintAction;
    [SerializeField] private InputActionReference _crouchAction;
    [SerializeField] private InputActionReference _jumpAction;


    public void SetActive(bool active)
    {
        if (active)
        {
            Enabled = true;
            _moveAction.action.Enable();
            _sprintAction.action.Enable();
            _crouchAction.action.Enable();
            _jumpAction.action.Enable();
        }
        else
        {
            _moveAction.action.Disable();
            _sprintAction.action.Disable();
            _crouchAction.action.Disable();
            _jumpAction.action.Disable();
            Enabled = false;
        }

    }
    
    public void Register(PlayerMovement playerMovement)
    {
        SetActive(true);

        _moveAction.action.started += playerMovement.OnMove;
        _moveAction.action.performed += playerMovement.OnMove;
        _moveAction.action.canceled += playerMovement.OnMove;

        _sprintAction.action.started += playerMovement.OnSprint;
        _sprintAction.action.canceled += playerMovement.OnSprint;

        _crouchAction.action.started += playerMovement.OnCrouch;
        _crouchAction.action.canceled += playerMovement.OnCrouch;

        _jumpAction.action.started += playerMovement.OnJumpPerformed;


    }

    public void Unregister(PlayerMovement playerMovement)
    {

        _moveAction.action.started -= playerMovement.OnMove;
        _moveAction.action.performed -= playerMovement.OnMove;
        _moveAction.action.canceled -= playerMovement.OnMove;



        _sprintAction.action.started -= playerMovement.OnSprint;
        _sprintAction.action.canceled -= playerMovement.OnSprint;

        _crouchAction.action.started -= playerMovement.OnCrouch;
        _crouchAction.action.canceled -= playerMovement.OnCrouch;

        _jumpAction.action.started -= playerMovement.OnJumpPerformed;

        SetActive(false);

    }
}
