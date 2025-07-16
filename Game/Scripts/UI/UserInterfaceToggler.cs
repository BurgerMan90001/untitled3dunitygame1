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
    public UserInterfaceToggler(UXMLFileHandler uxmlFileHandler)
    {
        _uxmlFileHandler = uxmlFileHandler;
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
        VisualElement elementToBeShown = _uxmlFileHandler.UserInterfaceElements[userInterface];

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
        /*
        if (elementToBeShown.style.display == DisplayStyle.Flex)
        {
            elementToBeShown.style.display = DisplayStyle.None;

        }
        else
        {
            elementToBeShown.style.display = DisplayStyle.Flex;
        }
        */
    }

    
    #region
    /// <summary>
    /// <br> Toggles a user interface on or off based on the UserInterfaceType value.</br>
    /// <br> Also switches to the inputActionMap set. </br>
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
        // Hide all UI elements
        foreach (var element in _uxmlFileHandler.UserInterfaceElements.Values)
        {
            element.style.display = DisplayStyle.None;
        }

        // Show the selected interface
        if (_uxmlFileHandler.UserInterfaceElements.ContainsKey(userInterface))
        {
            _uxmlFileHandler.UserInterfaceElements[userInterface].style.display = DisplayStyle.Flex;
        } else
        {
            Debug.LogError("The switched to user interface has not been found!");
        }
    }
}
