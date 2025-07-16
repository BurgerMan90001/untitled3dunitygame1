using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[System.Serializable]
[CreateAssetMenu(menuName = "Input/GenericInput")]
public class GenericInput : ScriptableObject
{
    public bool LookEnabled {  get; private set; }
    [field : SerializeField] public bool Enabled { get; private set; }

    [Header("InputActionReferences")]
    [SerializeField] private List<InputActionReference> _inputReferences;
    /*
    [SerializeField] private InputActionReference _lookAction;
    [SerializeField] private InputActionReference _pickUpAction;
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private InputActionReference _leftClickAction;
    */


    public void SetActive(bool active)
    {
        if (active)
        {
            foreach (InputActionReference reference in _inputReferences)
            {
                reference.action.Enable();
            }
            
        }
        else
        {
            foreach (InputActionReference reference in _inputReferences) 
            { 
                reference.action.Disable(); 
            }
        }

    }
    public void ToggleInput(int index, bool active)
    {
        if (active)
        {
            _inputReferences[index].action.Enable();

        }
        else
        {
            _inputReferences[index].action.Disable();

        }
    }
    /*
    public void Register(GameObject gameObject)
    {
        SetActive(true);

        _lookAction.action.started += gameObject.OnLook;
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

        _interactAction.action.started -= cameraActions.OnInteract;
        _interactAction.action.canceled -= cameraActions.OnInteract;

        _leftClickAction.action.started -= cameraActions.OnLeftClick;
        _leftClickAction.action.canceled -= cameraActions.OnLeftClick;

        _pickUpAction.action.started -= cameraActions.OnPickup;
        _pickUpAction.action.canceled -= cameraActions.OnPickup;

        SetActive(false);

    }
    */
}
