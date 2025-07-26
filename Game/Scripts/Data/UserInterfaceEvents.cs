using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/UserInterfaceEvents")]
public class UserInterfaceEvents : Event
{
    public event Action<UserInterfaceType> OnSwitchToUserInterface;
    public event Action<UserInterfaceType> OnShowInterface;
    public event Action OnHideRecentInterface;
    /*
    [Header("Data")]
    [SerializeField] private UserInterfaceData _userInterfaceData;
    */
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {
        HideRecentInterface();
        ShowInterface(userInterface);

    }



    public void ShowInterface(UserInterfaceType userInterface)
    {
        OnShowInterface?.Invoke(userInterface);

    }
    /// <summary>
    /// <br> Hides the most recently shown interface. Does nothing if there is none. </br>
    /// </summary>
    public void HideRecentInterface()
    {

        OnHideRecentInterface?.Invoke();


    }
}

