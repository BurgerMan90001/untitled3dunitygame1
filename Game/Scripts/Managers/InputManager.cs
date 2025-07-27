using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Manager
{
    [Header("Test")]
    [SerializeField] private Inventory test;

    private GameObject _eventSystem;
    [Header("Stuff")]


    [Header("Data")]
    [SerializeField] private GameInput _gameInput;

    [Header("Events")]
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private CombatEvents _combatEvents;



    private void OnEnable()
    {
        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;


        _combatEvents.OnEnterCombat += OnEnterCombat;
        _combatEvents.OnExitCombat += OnExitCombat;


        _gameInput.DebugInput.RegisterInputEvent(_gameInput.DebugInput.Debug1Action, OnDebug1); // Z
        _gameInput.DebugInput.RegisterInputEvent(_gameInput.DebugInput.Debug2Action, OnDebug2); // X

        //    _eventSystem = FindFirstObjectByType<InputSystemUIInputModule>().gameObject;

    }
    private void OnDisable()
    {

        _combatEvents.OnEnterCombat -= OnEnterCombat;
        _combatEvents.OnExitCombat -= OnExitCombat;

        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;


        _gameInput.DebugInput.UnregisterInputEvent(_gameInput.DebugInput.Debug1Action, OnDebug1);
        _gameInput.DebugInput.UnregisterInputEvent(_gameInput.DebugInput.Debug2Action, OnDebug2);


    }

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

            Debug.Log("Debug1 button pressed.");
            if (_eventSystem.activeSelf) // if active
            {
                _eventSystem.SetActive(false); // turn off

            }
            else
            {
                _eventSystem.SetActive(true);
            }
        }

    }
    private void OnDebug2(InputAction.CallbackContext ctx)
    {
        Debug.Log("Debug2 button pressed.");


        GameCursor.ToggleLock();
    }

    public override void Initialize()
    {
        Debug.Log("Input manager instantiated.");
    }
}
