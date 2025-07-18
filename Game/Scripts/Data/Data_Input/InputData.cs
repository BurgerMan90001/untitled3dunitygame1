
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : ScriptableObject 
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;

    [Header("InputEvents")]

    [SerializeField] private MovementInput _movementInput;
    [SerializeField] private CameraInput _cameraInput;
//    [SerializeField] private List<ScriptableObject> _inputEvents;

    public bool InputEnabled { get; private set; }

    
    private void OnEnable()
    {
        _dialogueData.OnChoiceSelected += (_) => _movementInput.EnableMovement(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices += (_) => _movementInput.EnableMovement(false); // discards the list of choice strings. disables input

        _dialogueData.OnChoiceSelected += (_) => _cameraInput.EnableLook(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices += (_) => _cameraInput.EnableLook(false); // discards the list of choice strings. disables input
    }
    private void OnDisable()
    {
        _dialogueData.OnChoiceSelected -= (_) => _movementInput.EnableMovement(true); 
        _dialogueData.OnUpdateChoices -= (_) => _movementInput.EnableMovement(false);

        _dialogueData.OnChoiceSelected -= (_) => _cameraInput.EnableLook(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices -= (_) => _cameraInput.EnableLook(false); // discards the list of choice strings. disables input

    }
    #region
    /// <summary>
    /// <br> Toggle all inputs on or off. </br>
    /// </summary>
    /// <param name="active"></param>
    #endregion
    public void ToggleInput(bool active)
    {
        _movementInput.SetActive(active);
        _cameraInput.SetActive(active);
    }

    /*
    public void ActivateInputAction(bool active)
    {
        if (active)
        {
            
        }
        else
        {

        }
        throw new NotImplementedException();
    }
    */
}

public enum InputMaps
{

}


