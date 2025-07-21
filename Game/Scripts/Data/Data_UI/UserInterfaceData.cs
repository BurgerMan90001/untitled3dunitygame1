
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
    // [SerializeField] private ShopData 

    public Dictionary<UserInterfaceType, VisualElement> UserInterfaceElements;

    public Action<UserInterfaceType> OnSwitchToUserInterface;

    private void OnEnable()
    {
        _inventory.OnInventoryChanged += OnInventoryChanged;
    }

    private void OnDisable()
    {
        _inventory.OnInventoryChanged -= OnInventoryChanged;
    }

    private void OnInventoryChanged()
    {

    }


    public void SwitchToUserInterface(UserInterfaceType userInterfaceType)
    {
        OnSwitchToUserInterface?.Invoke(userInterfaceType);
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

    public void Register()
    {

    }
    public void Unregister()
    {

    }

}