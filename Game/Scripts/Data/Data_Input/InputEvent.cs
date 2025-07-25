using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum InputType
{
    Movement,
    Camera,
    Menu,
    Debug,
}


public class InputEvent : ScriptableObject
{
    protected List<InputActionReference> _inputActionReferences = new();
    [field: SerializeField] public InputType InputType { get; protected set; }
    public bool Enabled { get; protected set; }
    public virtual void SetActive(bool active)
    {
        foreach (var reference in _inputActionReferences)
        {
            if (active)
            {
                reference.action.Enable();
            }
            else
            {
                reference.action.Disable();
            }

        }
        Enabled = active;
    }
    public virtual void EnableInputAction(bool enabled, InputActionReference inputAction)
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
    public virtual void RegisterInputEvent(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        SetActive(true);

        inputActionReference.action.started += inputAction;
        inputActionReference.action.performed += inputAction;
        inputActionReference.action.canceled += inputAction;

        _inputActionReferences.Add(inputActionReference);
    }


    public virtual void UnregisterInputEvent(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction)
    {
        inputActionReference.action.started -= inputAction;
        inputActionReference.action.performed -= inputAction;
        inputActionReference.action.canceled -= inputAction;

        SetActive(false);

        _inputActionReferences.Clear();

    }

}
