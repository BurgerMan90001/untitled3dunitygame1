
using System;
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
public class CameraInput : ScriptableObject, IInputEvent
{

    public bool LookEnabled {  get; private set; }
    public bool InteractEnabled { get; private set; }
    public bool LeftClickEnabled { get; private set; }
    public bool RightClickEnabled { get; private set; }
    public bool PickupEnabled { get; private set; }
    public bool Enabled { get; private set; }

    
    [field: SerializeField] public InputType InputType { get; private set; }



    [Header("InputActionReferences")]
    [field: SerializeField] public InputActionReference LookAction { get; private set; }
    [field: SerializeField] public InputActionReference PickupAction { get; private set; }
    [field: SerializeField] public InputActionReference InteractAction { get; private set; }
    [field: SerializeField] public InputActionReference LeftClickAction { get; private set; }


    public CameraInput()
    {
        InputType = InputType.Camera;

    }
   
    public void SetActive(bool active)
    {
        EnableLook(active);
        EnableInteract(active);
        EnablePickup(active);
        EnableClick(active);

        Enabled = active;

    }
    #region
    /// <summary>
    /// <br> Enables or disables an input action. </br>
    /// <br> Returns true if its enabled and false if it isnt. </br>
    /// </summary>
    /// <param name="enabled"></param>
    /// <param name="inputAction"></param>
    /// <returns></returns>
    #endregion
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
    public void RegisterInputEvent(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        SetActive(true);

        inputActionReference.action.started += inputAction;
        inputActionReference.action.performed += inputAction;
        inputActionReference.action.canceled += inputAction;
        

        
    }

   
    public void UnregisterInputEvent(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        inputActionReference.action.started -= inputAction;
        inputActionReference.action.performed -= inputAction;
        inputActionReference.action.canceled -= inputAction;

        
        SetActive(false);

    }


}



/*
public struct InputActionCallbackType
{
    public InputAction.CallbackContext ctx;
    public static readonly InputActionCallback qwe = new InputActionCallback();
}

*/