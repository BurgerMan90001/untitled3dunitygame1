using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, IInputManager
{
    [Header("Debug")]
    [SerializeField] private bool _clearInventoryOnEnable = false;

    //  private Inventory _inventory;
    private bool _interfaceEnabled = false;


    private GameInput _gameInput;
    private DialogueEvents _dialogueEvents;
    private CombatEvents _combatEvents;

    private UserInterfaceEvents _userInterfaceEvents;
    public void Inject(DialogueEvents dialogueEvents, CombatEvents combatEvents, GameInput gameInput, UserInterfaceEvents userInterfaceEvents)
    {
        _combatEvents = combatEvents;
        _dialogueEvents = dialogueEvents;
        _gameInput = gameInput;
        _userInterfaceEvents = userInterfaceEvents;
    }

    private void OnEnable()
    {


        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;


        _combatEvents.OnEnterCombat += OnEnterCombat;
        _combatEvents.OnExitCombat += OnExitCombat;

        _gameInput.MenuInput.RegisterInputEvent(_gameInput.MenuInput.InventoryToggleAction, OnOpenInventory);

        _gameInput.DebugInput.RegisterInputEvent(_gameInput.DebugInput.Debug1Action, OnDebug1); // Z
        _gameInput.DebugInput.RegisterInputEvent(_gameInput.DebugInput.Debug2Action, OnDebug2); // X



        //   ClearInventory(_clearInventoryOnEnable);
    }
    private void OnDisable()
    {

        _combatEvents.OnEnterCombat -= OnEnterCombat;
        _combatEvents.OnExitCombat -= OnExitCombat;

        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;



        _gameInput.DebugInput.UnregisterInputEvent(_gameInput.DebugInput.Debug1Action, OnDebug1);
        _gameInput.DebugInput.UnregisterInputEvent(_gameInput.DebugInput.Debug2Action, OnDebug2);

        _gameInput.MenuInput.UnregisterInputEvent(_gameInput.MenuInput.InventoryToggleAction, OnOpenInventory);


    }
    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (_interfaceEnabled)
            {
                _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.HUD);
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

    /*
    private void ClearInventory(bool active)
    {
        if (active)
        {

            _inventory.ResetInventory();
        }
    }
    */
    private void OnEnterCombat(CombatUnit _)
    {
        _gameInput.MovementInput.EnableMovement(false);
        _gameInput.MenuInput.EnableInventoryToggle(false);
    }
    private void OnExitCombat()
    {
        _gameInput.MovementInput.EnableMovement(true);
        _gameInput.MenuInput.EnableInventoryToggle(true);
    }

    private void OnUpdateChoices(List<string> choices) // discards the list of choice strings. disables input
    {
        _gameInput.MovementInput.EnableMovement(false);
        _gameInput.CameraInput.EnableLook(false);
        _gameInput.CameraInput.EnableInteract(false);
        _gameInput.MenuInput.EnableInventoryToggle(false);
    }

    private void OnChoiceSelected(int choicesIndex) // discards the chosen int index. enables input
    {
        _gameInput.MovementInput.EnableMovement(true);
        _gameInput.CameraInput.EnableLook(true);
        _gameInput.CameraInput.EnableInteract(true);
        _gameInput.MenuInput.EnableInventoryToggle(true);
    }
    private void OnDebug1(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {

            /*
            Debug.Log("Debug1 button pressed.");
            if (_eventSystem.activeSelf) // if active
            {
                _eventSystem.SetActive(false); // turn off

            }
            else
            {
                _eventSystem.SetActive(true);
            }
            */
        }

    }
    private void OnDebug2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Debug2 button pressed.");


        GameCursor.ToggleLock();
    }


}
