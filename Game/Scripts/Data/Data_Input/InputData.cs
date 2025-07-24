
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : Data
{
    [Header("Data")]
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private CombatEvents _combatEvents;

    [Header("InputEvents")]
    [field: SerializeField] public MovementInput MovementInput { get; private set; }
    [field: SerializeField] public CameraInput CameraInput { get; private set; }
    [field: SerializeField] public MenuInput MenuInput { get; private set; }
    //   [field: SerializeField] public DebugInput DebugInput { get; private set; }

    [Header("Debug")]
    [SerializeField] private bool _debugEnabled = false;
    [SerializeField] private InputActionReference _debug1;
    [SerializeField] private InputActionReference _debug2;



    public bool InputEnabled { get; private set; }


    /*
    private void OnEnable()
    {
        _dialogueEvents.OnChoiceSelected += OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices += OnUpdateChoices;

        
        _combatData.Events.OnEnterCombat += OnEnterCombat;
        _combatData.Events.OnExitCombat += OnExitCombat;
        

        if (_debugEnabled)
        {

            _debug1.action.started += OnDebug1;
            _debug2.action.started += OnDebug2;
        }
    }
    private void OnDisable()
    {
        
        _combatData.Events.OnEnterCombat -= OnEnterCombat;
        _combatData.Events.OnExitCombat -= OnExitCombat;
        
        _dialogueEvents.OnChoiceSelected -= OnChoiceSelected;
        _dialogueEvents.OnUpdateChoices -= OnUpdateChoices;

        if (_debugEnabled)
        {
            _debug1.action.started -= OnDebug1;
            _debug2.action.started -= OnDebug2;
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
    private void OnDebug1(InputAction.CallbackContext ctx)
    {


    }
    private void OnDebug2(InputAction.CallbackContext ctx)
    {

    }


    #region
    /// <summary>
    /// <br> Toggle all inputs on or off. </br>
    /// </summary>
    /// <param name="active"></param>
    #endregion
    public void ToggleInput(bool active)
    {
        MovementInput.SetActive(active);
        CameraInput.SetActive(active);
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

    public override void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new System.NotImplementedException();
    }
}



public struct InputSettings
{

}
public enum InputMaps
{

}


