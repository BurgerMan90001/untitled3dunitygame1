#region
/// <summary>
/// <br> A helper class that toggles user interfaces. </br>
/// </summary>
#endregion

// TODO MAKE HELPER CLASS

/*
public class UserInterfaceToggler
{
    public event Action OnInterfaceChanged;

    private readonly userInterfaceEvents _data;
    public UserInterfaceToggler(userInterfaceEvents data)
    {
        _data = data;
        _data.ShownInterface = UserInterfaceType.None;
    }


    private void ShowInterface(UserInterfaceType userInterface)
    {
        _data.ShownInterface = userInterface;
        VisualElement elementToBeShown = _data.UserInterfaceElements[userInterface];
        elementToBeShown.style.display = DisplayStyle.Flex;
    }
    private void HideInterface(UserInterfaceType userInterface)
    {
        VisualElement elementToBeHiden = _data.UserInterfaceElements[userInterface];
        elementToBeHiden.style.display = DisplayStyle.None;
    }


    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {

        if (_data.ShownInterface == UserInterfaceType.None)
        {
            ShowInterface(userInterface);
        }
        else
        {

            HideInterface(_data.ShownInterface);
            ShowInterface(userInterface);
            _data.ShownInterface = userInterface;
        }

    }
    public void SetInterfaceActive(UserInterfaceType userInterface, bool active)
    {
        if (active)
        {
            ShowInterface(userInterface);
        }
        else
        {
            HideInterface(userInterface);
        }
    }
    public void SwitchToUserInterface(SceneLoadingSettings sceneLoadingSettings)
    {
        SwitchToUserInterface(sceneLoadingSettings.UserInterface);
    }

}


*/