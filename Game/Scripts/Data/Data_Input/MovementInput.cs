
using UnityEngine;
using UnityEngine.InputSystem;


public enum MovementInputType // MABYE
{
    Move,
    Sprint,
    Crouch,
    Jump,
}

[CreateAssetMenu(menuName = "Input/MovementInput")]
public class MovementInput : InputEvent
{
    public bool MoveEnabled { get; private set; }
    public bool SprintEnabled { get; private set; }
    public bool CrouchEnabled { get; private set; }
    public bool JumpEnabled { get; private set; }


    [Header("InputActionReferences")]
    [field : SerializeField] public InputActionReference MoveAction { get; private set; }
    [field: SerializeField] public InputActionReference SprintAction { get; private set; }
    [field: SerializeField] public InputActionReference CrouchAction { get; private set; }
    [field: SerializeField] public InputActionReference JumpAction { get; private set; }
  
    public MovementInput()
    {
        InputType = InputType.Movement;
    }
    
    public override void SetActive(bool active)
    {
        
        EnableMovement(active);
        EnableJump(active);
        EnableCrouch(active);
        EnableSprint(active);
            
        Enabled = active;
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

