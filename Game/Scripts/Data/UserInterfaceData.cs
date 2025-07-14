using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "UI/UserInterfaceData")]
public class UserInterfaceData : ScriptableObject
{
    public Dictionary<UserInterfaces, VisualElement> UserInterfaceElements;
//    public Action<UserInterfaces> OnToggleUserInterface;
    public Action<UserInterfaces> OnToggleUserInterface;

    public void ToggleUserInterface(UserInterfaces userInterface)
    {
        OnToggleUserInterface?.Invoke(userInterface);
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
