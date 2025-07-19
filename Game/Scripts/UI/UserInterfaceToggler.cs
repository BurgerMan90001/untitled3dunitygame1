using System;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// <br> A helper class that toggles user interfaces. </br>
/// </summary>
public class UserInterfaceToggler 
{
    private UXMLFileHandler _uxmlFileHandler;
    private InputManager _inputManager;

    public Action InterfaceChanged;

    private UserInterfaceType _shownInterface; 
    public UserInterfaceToggler(UXMLFileHandler uxmlFileHandler)
    {
        _uxmlFileHandler = uxmlFileHandler;
    }
    
    public void Register(Action<UserInterfaceType,bool> onUserInterfaceToggled) 
    {
        onUserInterfaceToggled += ToggleUserInterface;
    }

    public void Unregister(Action<UserInterfaceType,bool> onUserInterfaceToggled) 
    {
        onUserInterfaceToggled -= ToggleUserInterface;
    }
    
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
        VisualElement elementToBeShown = GetUserInterfaceElement(userInterface);
        _shownInterface = userInterface;
        if (elementToBeShown == null)
        {
            Debug.LogError("The elementToBeShown is null.");
            return;
        }
        if (active)
        {
            elementToBeShown.style.display = DisplayStyle.Flex;
        } else
        {
            elementToBeShown.style.display = DisplayStyle.None;
        }
        
    }
    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the visual elements DisplayStyle value. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="inputActionMap"> Set as null to leave the action map unchanged </param>
    /// Set as null to leave the action map unchanged
    #endregion
    public void ToggleUserInterface(UserInterfaceType userInterface) 
    {
        VisualElement elementToBeShown = GetUserInterfaceElement(userInterface);
        
        if (elementToBeShown == null)
        {
            Debug.LogError("The elementToBeShown is null.");
            return;
        }
        
        if (elementToBeShown.style.display == DisplayStyle.Flex)
        {
            elementToBeShown.style.display = DisplayStyle.None;

        }
        else
        {
            elementToBeShown.style.display = DisplayStyle.Flex;
        }
        
    }

    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the UserInterfaceType value.</br>
    /// <br> Also switches to the inputActionMap set. </br>
    /// <br>NotImplementedException();</br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="inputActionMap"></param>
    #endregion
    public void ToggleUserInterface(UserInterfaceType userInterface, bool active, string inputActionMap)
    {

        ToggleUserInterface(userInterface, active);
        throw new NotImplementedException();
        /*
        if (inputActionMap != null)
        {
            
            _inputManager.SwitchToActionMap(inputActionMap);

        }
        else
        {
            Debug.LogWarning("The switched to inputActionMap is null.");
        }
        */

    }
    /// <summary>
    /// <br> Loops through the entire UserInterfaceElements dictionary and disables each element. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    public void SwitchUserInterface(UserInterfaceType userInterface)
    {
        VisualElement elementToBeShown = GetUserInterfaceElement(userInterface);
        if (_shownInterface == UserInterfaceType.None)
        {
            ToggleUserInterface(userInterface, true);
        }
        else
        {
            elementToBeShown.style.display = DisplayStyle.Flex;
            _shownInterface = userInterface;
        }
        
    }
    /// <summary>
    /// <br> Returns a visual element from the UserInterfaceType key in the UserInterfaceElements dictionary. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <returns></returns>
    private VisualElement GetUserInterfaceElement(UserInterfaceType userInterface) 
    {

        return _uxmlFileHandler.UserInterfaceElements[userInterface];
    }
}
