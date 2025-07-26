
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
    /*
    [SerializeField] private userInterfaceEvents _userInterfaceEvents;
    [SerializeField] private InputData _inputData;
    [SerializeField] private GameTimeData _gameTimeData;
    */

    public bool InShop;


    /*
    #region
    /// <summary>
    /// <br> Invokes the OnShopShown event. </br>
    /// </summary>
    #endregion
    public void EnterShop(string shopGuid)
    {

        

        _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Shop);

        _inputData.MovementInput.EnableMovement(false);
        _inputData.CameraInput.EnableLook(false);

        
        InShop = true;


    }

    public void ExitShop()
    {
        

        _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.HUD);


        _inputData.MovementInput.EnableMovement(true);
        _inputData.CameraInput.EnableLook(true);
        

        InShop = false;
    }
    */
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

    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }
}
