
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
        _dialogueData.OnChoiceSelected += (_) => MovementInput.EnableMovement(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices += (_) => MovementInput.EnableMovement(false); // discards the list of choice strings. disables input

        _dialogueData.OnChoiceSelected += (_) => CameraInput.EnableLook(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices += (_) => CameraInput.EnableLook(false); // discards the list of choice strings. disables input
    }
    private void OnDisable()
    {
        _dialogueData.OnChoiceSelected -= (_) => MovementInput.EnableMovement(true); 
        _dialogueData.OnUpdateChoices -= (_) => MovementInput.EnableMovement(false);

        _dialogueData.OnChoiceSelected -= (_) => CameraInput.EnableLook(true); // discards the chosen int index. enables input
        _dialogueData.OnUpdateChoices -= (_) => CameraInput.EnableLook(false); // discards the list of choice strings. disables input

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
    /*
    public void EnableMovement(bool active)
    {
        MovementInput.EnableMovement(active);
    }
    public void EnableLook(bool active)
    {
        CameraInput.EnableLook(active);
    }
    */
}

public enum InputMaps
{

}


