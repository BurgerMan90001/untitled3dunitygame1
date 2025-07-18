
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : ScriptableObject 
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;

    [Header("InputEvents")]

    [field: SerializeField] public MovementInput MovementInput { get; private set; }
    [field: SerializeField] public CameraInput CameraInput { get; private set; }
    [field: SerializeField] public MenuInput MenuInput { get; private set; }

    //    [SerializeField] private List<ScriptableObject> _inputEvents;

    public bool InputEnabled { get; private set; }

    
    private void OnEnable()
    {
        _onChoiceSelected = (_) => // discards the chosen int index. enables input
        {
            MovementInput.EnableMovement(true);
            CameraInput.EnableLook(true);
        };

        _onUpdateChoices = (_) => // discards the list of choice strings. disables input
        {
            MovementInput.EnableMovement(false);
            CameraInput.EnableLook(false);
        };

        _dialogueData.OnChoiceSelected += _onChoiceSelected;
        _dialogueData.OnUpdateChoices += _onUpdateChoices;
    }
    private void OnDisable()
    {
        _dialogueData.OnChoiceSelected -= _onChoiceSelected;
        _dialogueData.OnUpdateChoices -= _onUpdateChoices;

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

    
    
}

public enum InputMaps
{

}


