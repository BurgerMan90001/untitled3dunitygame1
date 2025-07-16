
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/CameraInput")]
public class CameraInput : ScriptableObject, IInputEvent
{

    public bool LookEnabled {  get; private set; }

    public bool InteractEnabled { get; private set; }
    public bool Enabled { get; private set; }

    
    [field: SerializeField] public InputType InputType { get; private set; }



    [Header("InputActionReferences")]
    /*
    public List<InputActionReference> InputActionReferences { get; }
    */
    
    
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
            _lookAction.action.Enable();
            _pickUpAction.action.Enable();
            _interactAction.action.Enable();
            _leftClickAction.action.Enable();
            /*
            foreach (var input in InputActionReferences)
            {
                input.action.Enable();
            }
            
            */
            GameCursor.Lock();
            
        }
        else
        {
            _lookAction.action.Disable();
            _pickUpAction.action.Disable();
            _interactAction.action.Disable();
            _leftClickAction.action.Disable();
            /*
            foreach (var input in InputActionReferences)
            {
                input.action.Disable();
            }
            */
            GameCursor.Unlock();
            
            
        }
        Enabled = active;

    }
    public void EnableLook(bool enabled)
    {
        if (enabled)
        {
            _lookAction.action.Enable();
            
            LookEnabled = true;
        }
        else
        {
            _lookAction.action.Disable();

            LookEnabled = false;
        }
    }
    public void EnableInteract(bool enabled)
    {
        if (enabled)
        {
            _lookAction.action.Enable();

            InteractEnabled = true;
        }
        else
        {
            _lookAction.action.Disable();
         //   _lookAction.action.p
            InteractEnabled = false;
        }
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