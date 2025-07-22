
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

#region
/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
#endregion
public enum UserInterfaceType
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
    Shop,

}


#region
/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Data/UserInterfaceData")]
public class UserInterfaceData : Data
{
    [Header("Data")]
    [SerializeField] private Inventory _inventory;

    [Header("Settings")]

    public UserInterfaceType ShownInterface = UserInterfaceType.None;

    public Dictionary<UserInterfaceType, VisualElement> UserInterfaceElements;

    public event Action<UserInterfaceType> OnSwitchToUserInterface;
    public UserInterfaceEvents Events { get; private set; }

    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {

        if (ShownInterface == UserInterfaceType.None)
        {
            ShowInterface(userInterface);
        }
        else
        {

            HideInterface(ShownInterface);
            ShowInterface(userInterface);
            ShownInterface = userInterface;
        }

    }

    public void SetInterfaceActive(UserInterfaceType userInterfaceType, bool active)
    {
        if (active)
        {
            ShowInterface(userInterfaceType);
        }
        else
        {
            HideInterface(userInterfaceType);
        }
    }

    private void ShowInterface(UserInterfaceType userInterface)
    {
        ShownInterface = userInterface;
        VisualElement elementToBeShown = UserInterfaceElements[userInterface];
        elementToBeShown.style.display = DisplayStyle.Flex;
    }
    private void HideInterface(UserInterfaceType userInterface)
    {
        VisualElement elementToBeHiden = UserInterfaceElements[userInterface];
        elementToBeHiden.style.display = DisplayStyle.None;
    }
    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }

}

public class UserInterfaceEvents : IEvent
{
    public UserInterfaceEvents() { }



}