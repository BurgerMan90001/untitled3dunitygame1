
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : ScriptableObject 
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;


    [Header("InputEvents")]
    [field: SerializeField] public MovementInput MovementInput { get; private set; }
    [field: SerializeField] public CameraInput CameraInput { get; private set; }
    [field: SerializeField] public MenuInput MenuInput { get; private set; }


    [Header("Debug")]
    [SerializeField] private bool _debugEnabled = false;
    [SerializeField] private InputActionReference _debug1;
    [SerializeField] private InputActionReference _debug2;

    [SerializeField] private CombatData _combatData;
   // [field: SerializeField] public DebugInput DebugInput { get; private set; }


    public bool InputEnabled { get; private set; }

    
    private void OnEnable()
    {
        _dialogueData.OnChoiceSelected += OnChoiceSelected;
        _dialogueData.OnUpdateChoices += OnUpdateChoices;

        if (_debugEnabled)
        {
            _debug1.action.started += OnDebug1;
            _debug2.action.started += OnDebug2;
        }
    }
    private void OnDisable()
    {
        _dialogueData.OnChoiceSelected -= OnChoiceSelected;
        _dialogueData.OnUpdateChoices -= OnUpdateChoices;

        if (_debugEnabled)
        {
            _debug1.action.started -= OnDebug1;
            _debug2.action.started -= OnDebug2;
        }

    }
    private void OnDebug1(InputAction.CallbackContext ctx)
    {


    }
    private void OnDebug2(InputAction.CallbackContext ctx) 
    {

    }
    private void OnCombatEntered() 
    {

    }
    private void OnCombatExited()
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
    }

    private void OnChoiceSelected(int choicesIndex) // discards the chosen int index. enables input
    {
        MovementInput.EnableMovement(true);
        CameraInput.EnableLook(true);
    }
    
}

public enum InputMaps
{

}


