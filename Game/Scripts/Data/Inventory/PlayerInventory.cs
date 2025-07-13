
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour, IDataPersistence
{
    [Header("Dependancies")]
    
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private InputData _inputData;

    public DynamicInventory Inventory;

    [Header("InputActionReferences")]
    [SerializeField] private InputActionReference _inventoryOpenAction;
    [SerializeField] private InputActionReference _inventoryCloseAction;


    [Header("Debug")]
    [SerializeField] private bool _clearOnEnable = false;

    private void OnEnable()
    {
        _inventoryOpenAction.action.Enable();
        _inventoryCloseAction.action.Enable();


        _inventoryOpenAction.action.started += OnOpenInventory;
    //    _inventoryOpenAction.action.canceled += OnOpenInventory;

        _inventoryCloseAction.action.started += OnCloseInventory;
     //   _inventoryCloseAction.action.canceled += OnCloseInventory;

        ClearInventory(_clearOnEnable);


    }

    private void OnDisable()
    {
        _inventoryCloseAction.action.started -= OnCloseInventory;


        _inventoryOpenAction.action.started -= OnOpenInventory;

        _inventoryOpenAction.action.Disable();
        _inventoryCloseAction.action.Disable();
    }
    private void OnDestroy()
    {
        
    }
    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        
        _userInterfaceData.ToggleUserInterface(UserInterfaces.Inventory);
        _inputData.ToggleInput();
    }
    private void OnCloseInventory(InputAction.CallbackContext ctx)
    {

    }
    private void ClearInventory(bool active)
    {
        if (active)
        {

            Inventory.ResetInventory();
        }
    }
    public void LoadData(GameData data)
    {
    //    Inventory.Items = data.Items;
        
    }

    public void SaveData(GameData data)
    {
   //     data.Items = Inventory.Items;
    }
}
