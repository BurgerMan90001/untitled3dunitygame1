
using System;
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
/// <br> Universal data and events for npc shops.</br>
/// </summary>
[CreateAssetMenu(menuName = "Data/ShopData")]
public class ShopData : Data
{
    /*
    [Header("Item Pools")]
    [SerializeField] private List<ShopItemPool> _shopItemPools;
    */
    [Header("Data")]
    [SerializeField] private UserInterfaceData _userInterfaceData;
    [SerializeField] private InputData _inputData;
    [SerializeField] private GameTimeData gameTimeData;

    public Action<string> OnShopEntered;

    public Action OnShopExited;

    public bool InShop { get; private set; } = false;
   

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
    public void EnterShop(string shopGuid)
    {
        OnShopEntered?.Invoke(shopGuid);

        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Shop, true);
        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, true);

        
        _inputData.MovementInput.EnableMovement(false);
        _inputData.CameraInput.EnableLook(false);


        InShop = true;

    }

    public void ExitShop()
    {
        OnShopExited?.Invoke();

        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Shop, false);
        _userInterfaceData.ToggleUserInterface(UserInterfaceType.Inventory, false);

        _inputData.MovementInput.EnableMovement(true);
        _inputData.CameraInput.EnableLook(true);

        InShop = false;
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
