using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInputEvent 
{
    /*
    public List<InputActionReference> InputActionReferences { get; }
    */
    public InputType InputType { get; }
    public bool Enabled { get; }
    public void SetActive(bool active);

    void EnableInputAction(bool enabled, InputActionReference inputAction);

    void Register( InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction);
    void Unregister(InputActionReference inputActionReference, Action<InputAction.CallbackContext> inputAction);

}
public enum InputType
{
    Movement,
    Camera,
}


// MAYBE
public class InputEvent : ScriptableObject
{
    public List<InputActionReference> InputActionReferences { get; protected set; }
    public InputType InputType { get; protected set; }
    public bool Enabled { get; protected set; }
    public void SetActive(bool active)
    {
        if (active)
        {
            foreach (var input in InputActionReferences)
            {
                input.action.Enable();
            }

        } 
        else

        {
            foreach (var input in InputActionReferences)
            {
                input.action.Disable();
            }
        }
    }
}
