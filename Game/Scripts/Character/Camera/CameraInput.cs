
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
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _pickUpAction;
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _leftClickAction;
    

    public CameraInput()
    {
        InputType = InputType.Camera;

    }
   
    public void SetActive(bool active)
    {
        if (active)
        {
            EnableLook(true);
            EnableInteract(true);
            EnablePickup(true);
            EnableClick(true);
            GameCursor.Lock();
            
        }
        else
        {
            EnableLook(false);
            EnableInteract(false);
            EnablePickup(false);
            EnableClick(false);   
            GameCursor.Unlock();
            
            
        }
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
        EnableInputAction(enabled, _lookAction);
        
    }
    public void EnableInteract(bool enabled)
    {
        InteractEnabled = enabled;
        EnableInputAction(enabled, _interactAction);
    }

    public void EnablePickup(bool enabled)
    {
        PickupEnabled = enabled;
        EnableInputAction(enabled, _pickUpAction);
    }

    public void EnableClick(bool enabled)
    {
        LeftClickEnabled = enabled;
        EnableInputAction(enabled, _leftClickAction);
    }
    public void Register(CameraActions cameraActions)
    {
        SetActive(true);

        _lookAction.action.started += cameraActions.OnLook;
        _lookAction.action.performed += cameraActions.OnLook;
        _lookAction.action.canceled +=  cameraActions.OnLook;

        _interactAction.action.started += cameraActions.OnInteract;
        _interactAction.action.canceled +=  cameraActions.OnInteract;

        _leftClickAction.action.started += cameraActions.OnLeftClick;
        _leftClickAction.action.canceled += cameraActions.OnLeftClick;

        _pickUpAction.action.started += cameraActions.OnPickup;
        _pickUpAction.action.canceled += cameraActions.OnPickup;

        
    }

   
    public void Unregister(CameraActions cameraActions)
    {

        _lookAction.action.started -= cameraActions.OnLook;
        _lookAction.action.performed -= cameraActions.OnLook;
        _lookAction.action.canceled -= cameraActions.OnLook;
     //   _interactAction.action.st
        _interactAction.action.started -= cameraActions.OnInteract;
        _interactAction.action.canceled -= cameraActions.OnInteract;

        _leftClickAction.action.started -= cameraActions.OnLeftClick;
        _leftClickAction.action.canceled -= cameraActions.OnLeftClick;

        _pickUpAction.action.started -= cameraActions.OnPickup;
        _pickUpAction.action.canceled -= cameraActions.OnPickup;

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