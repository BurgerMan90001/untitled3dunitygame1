using MyBox;
using System;
using System.Collections.Generic;
using System.Linq;
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


    [Header("Debug")]
    [SerializeField] private bool _debugMode = true;

    public Dictionary<UserInterfaceType, VisualElement> UserInterfaceElements;


    public UserInterfaceEvents Events { get; private set; }
    public Stack<UserInterfaceType> ShownInterfaces { get; private set; } = new Stack<UserInterfaceType>();
    [ReadOnly][SerializeField] private List<UserInterfaceType> ShownInterfacesStack;

    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {


        HideRecentInterface();
        ShowInterface(userInterface);




    }



    public void ShowInterface(UserInterfaceType userInterface)
    {
        if (userInterface == UserInterfaceType.None)
        {
            Debug.LogWarning($"Can't show {UserInterfaceType.None}");
            return;
        }

        ShownInterfaces.Push(userInterface);
        VisualElement elementToBeShown = UserInterfaceElements[userInterface];
        elementToBeShown.style.display = DisplayStyle.Flex;

        ShownInterfacesStack = ShownInterfaces.ToList<UserInterfaceType>();


    }
    /// <summary>
    /// <br> Hides the most recently shown interface. Does nothing if there is none. </br>
    /// </summary>
    public void HideRecentInterface()
    {

        if (ShownInterfaces.TryPop(out UserInterfaceType userInterface))
        {
            VisualElement elementToBeHiden = UserInterfaceElements[userInterface];
            elementToBeHiden.style.display = DisplayStyle.None;

            ShownInterfacesStack = ShownInterfaces.ToList<UserInterfaceType>();
        }

    }
    /// <summary>
    /// Gets the most recently added user interface in the shown interfaces stack.
    /// </summary>
    /// <returns></returns>
    public UserInterfaceType GetRecentInterface()
    {
        if (ShownInterfaces.TryPeek(out UserInterfaceType userInterface))
        {
            return userInterface;
        }
        else
        {
            if (_debugMode)
            {
                Debug.LogWarning("There is no interfaces in _shownInterfaces. ");
            }
            return UserInterfaceType.None;
        }
    }

    public void ClearInterfaces()
    {
        foreach (var ui in ShownInterfaces)
        {
            HideRecentInterface();
        }
    }
    private void ShowStack()
    {
        if (_debugMode)
        {
            Debug.Log(ShownInterfaces.Count);
            /*
            foreach (var ui in ShownInterfaces)
            {
                Debug.Log(ui.ToString());
            }
            */

        }
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