using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Input")]

    public PlayerInputActions PlayerInputActions;

    public MovementInput MovementInput;
    public CameraInput CameraInput;
    public MenuInput MenuInput;
    public DebugInput DebugInput;
    [Header("Debug")]
    [SerializeField] private bool _clearInventoryOnEnable = false;

    //  private Inventory _inventory;
    private bool _interfaceEnabled = false;



    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private CombatEvents _combatEvents;

    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;

    public void Inject(DialogueEvents dialogueEvents, CombatEvents combatEvents, UserInterfaceEvents userInterfaceEvents)
    {
        _combatEvents = combatEvents;
        _dialogueEvents = dialogueEvents;

        _userInterfaceEvents = userInterfaceEvents;
    }


    private void OnEnable()
    {
        PlayerInputActions.Player.Disable();
        Debug.Log("REGISTER");
        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;


        _combatEvents.OnEnterCombat += OnEnterCombat;
        _combatEvents.OnExitCombat += OnExitCombat;

        MenuInput.RegisterInputEvent(MenuInput.InventoryToggleAction, OnOpenInventory);

        DebugInput.RegisterInputEvent(DebugInput.Debug1Action, OnDebug1); // Z
        DebugInput.RegisterInputEvent(DebugInput.Debug2Action, OnDebug2); // X



        //   ClearInventory(_clearInventoryOnEnable);
    }


    private void OnDestroy()
    {
        Debug.Log("DESTROY");
        _combatEvents.OnEnterCombat -= OnEnterCombat;
        _combatEvents.OnExitCombat -= OnExitCombat;

        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;



        DebugInput.UnregisterInputEvent(DebugInput.Debug1Action, OnDebug1);
        DebugInput.UnregisterInputEvent(DebugInput.Debug2Action, OnDebug2);

        MenuInput.UnregisterInputEvent(MenuInput.InventoryToggleAction, OnOpenInventory);

    }
    private void OnOpenInventory(InputAction.CallbackContext ctx)
    {
        Debug.Log("ASdjoikksdakm");
        if (ctx.started)
        {
            if (_interfaceEnabled)
            {
                _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.HUD);
                _interfaceEnabled = false;

                MovementInput.EnableMovement(true);
                CameraInput.EnableLook(true);
            }
            else
            {
                _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Inventory);
                _interfaceEnabled = true;

                MovementInput.EnableMovement(false);
                CameraInput.EnableLook(false);

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
        MovementInput.EnableMovement(false);
        MenuInput.EnableInventoryToggle(false);
    }
    private void OnExitCombat()
    {
        MovementInput.EnableMovement(true);
        MenuInput.EnableInventoryToggle(true);
    }

    private void OnUpdateChoices(List<string> choices) // discards the list of choice strings. disables input
    {
        MovementInput.EnableMovement(false);
        CameraInput.EnableLook(false);
        CameraInput.EnableInteract(false);
        MenuInput.EnableInventoryToggle(false);
    }

    private void OnChoiceSelected(int choicesIndex) // discards the chosen int index. enables input
    {
        MovementInput.EnableMovement(true);
        CameraInput.EnableLook(true);
        CameraInput.EnableInteract(true);
        MenuInput.EnableInventoryToggle(true);
    }
    private void OnDebug1(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {


            Debug.Log("Debug1 button pressed.");

        }

    }
    private void OnDebug2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Debug2 button pressed.");


        GameCursor.ToggleLock();
    }


}
