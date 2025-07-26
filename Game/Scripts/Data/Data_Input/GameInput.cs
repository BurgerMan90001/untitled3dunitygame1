using UnityEngine;

[CreateAssetMenu(menuName = "Input/GameInput")]
public class GameInput : ScriptableObject
{

    [Header("InputEvents")]
    public MovementInput MovementInput;
    public CameraInput CameraInput;
    public MenuInput MenuInput;
    public DebugInput DebugInput;

    [Header("Debug")]
    [SerializeField] public bool DebugMode = false;



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
// MAYBE 

public struct InputSettings
{

}
public enum InputMaps
{

}


