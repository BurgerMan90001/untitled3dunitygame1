

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


    [Header("Data")]
    [SerializeField] private Inventory _inventory;
   // [SerializeField] private ShopData 
    
    public Dictionary<UserInterfaceType, VisualElement> UserInterfaceElements;

    public Action<UserInterfaceType, bool> OnToggleUserInterface;

    private void OnEnable()
    {
    //    _inventory.OnInventoryChanged += OnInventoryChanged;
    }

    private void OnDisable()
    {
       // _inventory.OnInventoryChanged -= OnInventoryChanged;
    }
    
    private void OnInventoryChanged()
    {

    }
    public void RegisterEvent(Data data, Action actionSubscriber)
    {
    //    data += actionSubscriber;
    }
    public void UnregisterEvent(Action actionEvent, Action actionSubscriber)
    {
        actionEvent -= actionSubscriber;
    }
    public void ToggleUserInterface(UserInterfaceType userInterfaceType, bool active)
    {
        OnToggleUserInterface?.Invoke(userInterfaceType, active);
    }
    /*
    public void ToggleUserInterface(UserInterfaceType userInterfaceType, bool active)
    {
        OnToggleUserInterface?.Invoke(userInterfaceType, active);
    }
    */

    /*

    public void ToggleUserInterface(UserInterfaceType userInterface, string inputActionMap)
    {
        OnToggleUserInterface?.Invoke(userInterface, inputActionMap);
    }
    */
}
/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
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
