using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <br> Wealth classes </br>
/// </summary>
public enum ShopType
{
    Lower,
    Middle,
    Upper,
}
// pennies
/// <summary>
/// <br>Data and events for npc shops.</br>
/// </summary>
[CreateAssetMenu(menuName = "Data/ShopData")]
public class ShopData : Data
{
    [Header("Item Pools")]
    [SerializeField] private List<ShopItemPool> _shopItemPools;
    [Header("Data")]
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private InputData _inputData;
    [SerializeField] private GameTimeData gameTimeData;
    public Action<string> OnShopEntered;

    public Action OnShopExited;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    
    /// <summary>
    /// <br> Invokes the OnShopShown event. </br>
    /// </summary>
    public void EnterShop(string shopGuid, ShopType shopType)
    {
        OnShopEntered?.Invoke(shopGuid);

        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Shop, true);
        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, true);

        _inputData.ToggleInput(false);

 
        GameCursor.Unlock();

    }

    public void ExitShop()
    {
        OnShopExited?.Invoke();

        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Shop, false);
        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, false);

        _inputData.ToggleInput(true);

        GameCursor.Lock();
    }
    // 5 common top 0-2
    public void GenerateShopContents() 
    {
        

    }
    public void GenerateTopRow()
    {

    }
    // 2 goodones bottom 3-5 


    private void GenerateBottomRow()
    {

    }

}
[CreateAssetMenu(menuName = "Items/Itempool")]
public class ShopItemPool : ScriptableObject
{
    

}