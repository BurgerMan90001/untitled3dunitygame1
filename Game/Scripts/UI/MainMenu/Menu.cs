using UnityEngine;
using UnityEngine.UIElements;

public class Menu
{
    /*
    [Header("First Selected Button")]
    [SerializeField] private Button firstSelectedButton;
    */

    /*
    protected virtual void OnEnable()
    {
        SetFirstSelected(firstSelectedButton);
    }
    */
    #region
    /// <summary>
    /// <br> Selects a button. </br>
    /// </summary>
    /// <param name="firstSelectedButton"></param>
    #endregion
    protected virtual void SelectButton(Button firstSelectedButton)
    {
    //    firstSelectedButton.Select();
        firstSelectedButton.Focus(); // needs to be styled 
    }
}
