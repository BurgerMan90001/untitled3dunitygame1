using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class InputManager : Manager
{

    private GameObject _eventSystem;
    [Header("Stuff")]


    [Header("Data")]
    [SerializeField] private InputData _inputData;

    [Header("Events")]
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private CombatEvents _combatEvents;



    private void OnEnable()
    {
        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;


        _combatEvents.OnEnterCombat += OnEnterCombat;
        _combatEvents.OnExitCombat += OnExitCombat;


        _inputData.DebugInput.RegisterInputEvent(_inputData.DebugInput.Debug1Action, OnDebug1); // Z
        _inputData.DebugInput.RegisterInputEvent(_inputData.DebugInput.Debug2Action, OnDebug2); // X

        _eventSystem = FindFirstObjectByType<InputSystemUIInputModule>().gameObject;

    }
    private void OnDisable()
    {

        _combatEvents.OnEnterCombat -= OnEnterCombat;
        _combatEvents.OnExitCombat -= OnExitCombat;

        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;


        _inputData.DebugInput.UnregisterInputEvent(_inputData.DebugInput.Debug1Action, OnDebug1);
        _inputData.DebugInput.UnregisterInputEvent(_inputData.DebugInput.Debug2Action, OnDebug2);


    }

    private void OnEnterCombat(CombatUnit _)
    {
        _inputData.MovementInput.EnableMovement(false);
        _inputData.MenuInput.EnableInventoryToggle(false);
    }
    private void OnExitCombat()
    {
        _inputData.MovementInput.EnableMovement(true);
        _inputData.MenuInput.EnableInventoryToggle(true);
    }

    private void OnUpdateChoices(List<string> choices) // discards the list of choice strings. disables input
    {
        _inputData.MovementInput.EnableMovement(false);
        _inputData.CameraInput.EnableLook(false);
        _inputData.CameraInput.EnableInteract(false);
        _inputData.MenuInput.EnableInventoryToggle(false);
    }

    private void OnChoiceSelected(int choicesIndex) // discards the chosen int index. enables input
    {
        _inputData.MovementInput.EnableMovement(true);
        _inputData.CameraInput.EnableLook(true);
        _inputData.CameraInput.EnableInteract(true);
        _inputData.MenuInput.EnableInventoryToggle(true);
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



}
