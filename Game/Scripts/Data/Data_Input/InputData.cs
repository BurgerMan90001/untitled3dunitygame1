using UnityEngine;

[CreateAssetMenu(menuName = "Input/InputData")]
public class InputData : Data
{

    [Header("InputEvents")]
    public MovementInput MovementInput;
    public CameraInput CameraInput;
    public MenuInput MenuInput;
    public DebugInput DebugInput;

    [Header("Debug")]
    [SerializeField] public bool DebugMode = false;




    public bool InputEnabled { get; private set; }






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


