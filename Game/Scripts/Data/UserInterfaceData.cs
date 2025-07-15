using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "UI/UserInterfaceData")]
public class UserInterfaceData : ScriptableObject
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

}
