

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
[CreateAssetMenu(menuName = "Data/UserInterfaceData")]
public class UserInterfaceData : Data
{
    public Dictionary<UserInterfaces, VisualElement> UserInterfaceElements;

    public Action<UserInterfaces, bool> OnToggleUserInterface;

    
    public void ToggleUserInterface(UserInterfaces userInterface, bool active)
    {
        OnToggleUserInterface?.Invoke(userInterface, active);
    }
    
    
    /*

    public void ToggleUserInterface(UserInterfaces userInterface, string inputActionMap)
    {
        OnToggleUserInterface?.Invoke(userInterface, inputActionMap);
    }
    */
}
/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
public enum UserInterfaces
{
    None,
    HUD,
    Loading,
    Inventory,
    Dialogue,
    Settings,
    PauseMenu,
    MainMenu,
    SaveSlotsMenu,
    Combat,

}
