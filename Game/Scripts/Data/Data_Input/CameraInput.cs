

using UnityEngine;
using UnityEngine.InputSystem;

public enum CameraInputType
{
    Look,
    Interact,
    Pickup,
    LeftClick,
    RightClick, // UNIMP

}

[CreateAssetMenu(menuName = "Input/CameraInput")]
public class CameraInput : InputEvent
{

    public bool LookEnabled {  get; private set; }
    public bool InteractEnabled { get; private set; }
    public bool LeftClickEnabled { get; private set; }
    public bool RightClickEnabled { get; private set; }
    public bool PickupEnabled { get; private set; }
  

    [Header("InputActionReferences")]
    [field: SerializeField] public InputActionReference LookAction { get; private set; }
    [field: SerializeField] public InputActionReference PickupAction { get; private set; }
    [field: SerializeField] public InputActionReference InteractAction { get; private set; }
    [field: SerializeField] public InputActionReference LeftClickAction { get; private set; }


    public CameraInput()
    {
        InputType = InputType.Camera;

    }
   
    public override void SetActive(bool active)
    {
        EnableLook(active);
        EnableInteract(active);
        EnablePickup(active);
        EnableClick(active);

        Enabled = active;

    }
    
    public void EnableLook(bool enabled)
    {
        LookEnabled = enabled;
        EnableInputAction(enabled, LookAction);
        
        if (enabled)
        {
            GameCursor.Lock();
        } else
        {
            GameCursor.Unlock();
        }
        
        
    }
    public void EnableInteract(bool enabled)
    {
        InteractEnabled = enabled;
        EnableInputAction(enabled, InteractAction);
    }

    public void EnablePickup(bool enabled)
    {
        PickupEnabled = enabled;
        EnableInputAction(enabled, PickupAction);
    }

    public void EnableClick(bool enabled)
    {
        LeftClickEnabled = enabled;
        EnableInputAction(enabled, LeftClickAction);
    }
    
}



/*
public struct InputActionCallbackType
{
    public InputAction.CallbackContext ctx;
    public static readonly InputActionCallback qwe = new InputActionCallback();
}

*/