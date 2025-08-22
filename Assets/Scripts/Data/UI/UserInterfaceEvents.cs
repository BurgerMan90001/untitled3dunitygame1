using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/UserInterfaceEvents")]
public class UserInterfaceEvents : Event
{
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

        //   Debug.Log("ASDASD");

    }



    public void ShowInterface(UserInterfaceType userInterface)
    {
        OnShowInterface?.Invoke(userInterface);

        //    Debug.Log("AAAAAAAA");
    }
    /// <summary>
    /// <br> Hides the most recently shown interface. Does nothing if there is none. </br>
    /// </summary>
    public void HideRecentInterface()
    {

        OnHideRecentInterface?.Invoke();

        //    Debug.Log("1231232131");

    }
}

