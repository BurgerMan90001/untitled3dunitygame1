
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
    //   [SerializeField] private bool

    public UserInterfaceType ShownInterface = UserInterfaceType.None;

    public Dictionary<UserInterfaceType, VisualElement> UserInterfaceElements { get; private set; } = new Dictionary<UserInterfaceType, VisualElement>();

    public event Action<UserInterfaceType> OnSwitchToUserInterface;

    public UserInterfaceEvents Events { get; private set; }

    private UserInterfaceToggler _interfaceToggler;




    private void Awake()
    {

    }

    private void OnEnable()
    {




        _interfaceToggler = new UserInterfaceToggler(this);

        _inventory.OnInventoryChanged += OnInventoryChanged;

        SceneLoader.OnSceneLoaded += _interfaceToggler.SwitchToUserInterface;


    }

    private void OnDisable()
    {
        _inventory.OnInventoryChanged -= OnInventoryChanged;

        SceneLoader.OnSceneLoaded -= _interfaceToggler.SwitchToUserInterface;


    }

    private void OnInventoryChanged()
    {

    }


    public void SwitchToUserInterface(UserInterfaceType userInterfaceType)
    {
        _interfaceToggler.SwitchToUserInterface(userInterfaceType);
    }

    public void SetInterfaceActive(UserInterfaceType userInterfaceType, bool active)
    {
        _interfaceToggler.SetInterfaceActive(userInterfaceType, active);
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