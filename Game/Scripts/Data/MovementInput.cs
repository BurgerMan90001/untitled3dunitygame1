
using System;
using UnityEngine;
using UnityEngine.InputSystem;


public enum MovementInputType // MABYE
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
    [field : SerializeField] public InputActionReference MoveAction { get; private set; }
    [field: SerializeField] public InputActionReference SprintAction { get; private set; }
    [field: SerializeField] public InputActionReference CrouchAction { get; private set; }
    [field: SerializeField] public InputActionReference JumpAction { get; private set; }
  
    public MovementInput()
    {
        InputType = InputType.Movement;
    }
    
    public void SetActive(bool active)
    {
        
        EnableMovement(active);
        EnableJump(active);
        EnableCrouch(active);
        EnableSprint(active);
            
        Enabled = active;
    }
    
    public void Register(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        SetActive(true);

        inputActionReference.action.started += inputAction;
        inputActionReference.action.performed += inputAction;
        inputActionReference.action.canceled += inputAction;
 

    }
    public void Unregister(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        inputActionReference.action.started -= inputAction;
        inputActionReference.action.performed -= inputAction;
        inputActionReference.action.canceled -= inputAction;

        SetActive(false);

    }
    public void EnableInputAction(bool enabled, InputActionReference inputAction)
    {
        if (enabled)
        {
            inputAction.action.Enable();
        }
        else
        {
            inputAction.action.Disable();
        }
    }
    public void EnableMovement(bool enabled)
    {
        MoveEnabled = enabled;
        EnableInputAction(enabled, MoveAction);
    }
    public void EnableCrouch(bool active)
    {
        CrouchEnabled = active;
        EnableInputAction(active, CrouchAction);
    }

    public void EnableJump(bool active)
    {
        JumpEnabled = active;
        EnableInputAction(active, JumpAction);
       
    }
    public void EnableSprint(bool active)
    {
        SprintEnabled = active;
        EnableInputAction(active, SprintAction);

    }
    

    
}

