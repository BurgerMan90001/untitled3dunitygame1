
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour, IDataPersistence
{
    [Header("Dependancies")]

    [SerializeField] private GameInput _gameInput;

    public Inventory Inventory;

    [Header("Debug")]
    [SerializeField] private bool _clearOnEnable = false;


    private bool _interfaceEnabled = false;

    private void OnEnable()
    {

        _gameInput.MenuInput.RegisterInputEvent(_gameInput.MenuInput.InventoryToggleAction, OnOpenInventory);


        ClearInventory(_clearOnEnable);


    }

    private void OnDisable()
    {
        _gameInput.MenuInput.UnregisterInputEvent(_gameInput.MenuInput.InventoryToggleAction, OnOpenInventory);

    }

    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (_interfaceEnabled)
            {
                EventManager.Instance.UserInterfaceEvents.SwitchToUserInterface(UserInterfaceType.HUD);
                _interfaceEnabled = false;

                _gameInput.MovementInput.EnableMovement(true);
                _gameInput.CameraInput.EnableLook(true);
            }
            else
            {
                EventManager.Instance.UserInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Inventory);
                _interfaceEnabled = true;

                _gameInput.MovementInput.EnableMovement(false);
                _gameInput.CameraInput.EnableLook(false);

            }
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
