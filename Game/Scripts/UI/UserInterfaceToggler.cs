using System;
using UnityEngine.UIElements;
// TODOO MAKE OVERLOADS CLEANER AND STUFF

#region
/// <summary>
/// <br> A helper class that toggles user interfaces. </br>
/// </summary>
#endregion
public class UserInterfaceToggler
{
    private UXMLFileHandler _uxmlFileHandler;
    private InputManager _inputManager;

    public Action InterfaceChanged;

    private UserInterfaceType _shownInterface; // the currently shown interface. is set to none if there is no interfaces

    public UserInterfaceToggler(UXMLFileHandler uxmlFileHandler)
    {
        _uxmlFileHandler = uxmlFileHandler;

        _shownInterface = UserInterfaceType.None;
    }


    private void ShowInterface(UserInterfaceType userInterface)
    {
        _shownInterface = userInterface;
        VisualElement elementToBeShown = GetUserInterfaceElement(userInterface);
        elementToBeShown.style.display = DisplayStyle.Flex;
    }
    private void HideInterface(UserInterfaceType userInterface)
    {
        VisualElement elementToBeHiden = GetUserInterfaceElement(userInterface);
        elementToBeHiden.style.display = DisplayStyle.None;
    }

    /*
    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the UserInterfaceType value. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="inputActionMap"> Set as null to leave the action map unchanged </param>
    /// Set as null to leave the action map unchanged
    #endregion
    public void ToggleUserInterface(UserInterfaceType userInterface, bool active)
    {

        _shownInterface = userInterface;

        if (active)
        {
            ShowInterface(userInterface);
        }
        else
        {
            HideInterface(userInterface);
        }

    }
    */

    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {

        if (_shownInterface == UserInterfaceType.None)
        {
            ShowInterface(userInterface);
        }
        else
        {
            HideInterface(_shownInterface);
            ShowInterface(userInterface);
            _shownInterface = userInterface;
        }

    }

    public void SwitchToUserInterface(SceneLoadingSettings sceneLoadingSettings)
    {
        SwitchToUserInterface(sceneLoadingSettings.UserInterface);
    }
    #region
    /// <summary>
    /// <br> Returns a visual element from the UserInterfaceType key in the UserInterfaceElements dictionary. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <returns></returns>
    #endregion
    private VisualElement GetUserInterfaceElement(UserInterfaceType userInterface)
    {

        return _uxmlFileHandler.UserInterfaceElements[userInterface];
    }
}

