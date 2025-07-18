
using UnityEngine;
using UnityEngine.InputSystem;


public enum MovementInputType
{
    Move,
    Sprint,
    Crouch,
    Jump,
}
// TODO MAKE CONSISTENT WITH CAMERAINPUT
[CreateAssetMenu(menuName = "Input/MovementInput")]
public class MovementInput : ScriptableObject, IInputEvent
{
    public bool MoveEnabled { get; private set; }
    public bool SprintEnabled { get; private set; }
    public bool CrouchEnabled { get; private set; }
    public bool JumpEnabled { get; private set; }
    public bool Enabled { get; private set; }

    [field: SerializeField] public InputType InputType { get; private set; }

 //   public List<InputActionReference> InputActionReferences { get; }

    [Header("InputActionReferences")]
    [field : SerializeField] public  InputActionReference MoveAction { get; private set; }
    [field: SerializeField] public InputActionReference SprintAction { get; private set; }
    [field: SerializeField] public InputActionReference CrouchAction { get; private set; }
    [field: SerializeField] public InputActionReference JumpAction { get; private set; }
  
    public MovementInput()
    {
        InputType = InputType.Movement;
    }
    public void EnableInputAction(bool enabled, InputActionReference inputAction)
    {
        if (enabled)
        {
            
            inputAction.action.Enable();
        } else
        {
            inputAction.action.Disable();
        }
    }
    public void EnableMovement(bool enabled)
    {
        MoveEnabled = enabled;
        EnableInputAction(enabled, MoveAction);
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            
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
            
        }
        Enabled = active;
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

    
    #region
    /// <summary>
    /// <br> Enables and disables movement. </br>
    /// </summary>
    /// <param name="active"></param>
    #endregion
    public void EnableMovement(bool active)
    {
        if (active)
        {

        }
        else
        {

        }
    }
    #region
    /// <summary>
    /// /// <summary>
    /// <br> Enables and disables crouch. </br.>
    /// </summary>
    /// <param name="active"></param>
    /// </summary>
    /// <param name="active"></param>
    #endregion
    public void EnableCrouch(bool active)
    {
        
        if (active)
        {

        }
        else
        {

        }
    }
    #region
    /// <summary>
    /// <br> Enables and disables jump. </br.>
    /// </summary>
    /// <param name="active"></param>
#endregion
    public void EnableJump(bool active)
    {
        if (active)
        {

        }
        else
        {

        }
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

