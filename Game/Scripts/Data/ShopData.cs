using System;
using UnityEngine;
// pennies
/// <summary>
/// <br></br>
/// </summary>
[CreateAssetMenu(menuName = "Data/ShopData")]
public class ShopData : Data
{
    public Action OnShopShown;

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
    public void ShowShop()
    {
        OnShopShown?.Invoke();
    }

    // 5 common top 0-2

    private void GenerateTopRow()
    {

    }
    // 2 goodones bottom 3-5 


    private void GenerateBottomRow()
    {

    }

}
