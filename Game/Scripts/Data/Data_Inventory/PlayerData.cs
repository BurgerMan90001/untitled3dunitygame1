
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData : MonoBehaviour, IDataPersistence
{
    [Header("Dependancies")]
    
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private InputData _inputData;

    public Inventory Inventory;

    [Header("Debug")]
    [SerializeField] private bool _clearOnEnable = false;


    private bool _interfaceEnabled = false;

    private void OnEnable()
    {

        _inputData.MenuInput.RegisterInputEvent(_inputData.MenuInput.InventoryToggleAction, OnOpenInventory);
        

        ClearInventory(_clearOnEnable);


    }

    private void OnDisable()
    {
        _inputData.MenuInput.UnregisterInputEvent(_inputData.MenuInput.InventoryToggleAction, OnOpenInventory);

    }

    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        if (_interfaceEnabled)
        {
            _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, false);
            _interfaceEnabled = false;

            _inputData.MovementInput.EnableMovement(true);
            _inputData.CameraInput.EnableLook(true);
        } else
        {
            _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, true);
            _interfaceEnabled = true;

            _inputData.MovementInput.EnableMovement(false);
            _inputData.CameraInput.EnableLook(false);
                
        }
        
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
