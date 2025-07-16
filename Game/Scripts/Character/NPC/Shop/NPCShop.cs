using System;
using UnityEngine;

public class NPCShop: MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private ShopType _shopType;



    private ShopData _shopData;

    private string _shopGuid;

    private bool _inShop = false;
    /// <summary>
    /// <br> Dependancy injection for npcs. </br>
    /// </summary>
    /// <param name="shopData"></param>
    public void Initialize(ShopData shopData, string shopGuid)
    {
        _shopData = shopData;
        _shopGuid = shopGuid;
    }
    /// <summary>
    /// <br> Called when the player interacts with the shop npc.</br>
    /// </summary>
    public void ShopInteraction()
    {
        if (!_inShop) // if not in the shop
        {
            _shopData.EnterShop(_shopGuid);
            _inShop = true;

        } else // if already in shop, exit shop when interacted with
        {
            _shopData.ExitShop();
            _inShop = false;
        }
    }
}

public class ShopManager
{

}
public enum ShopType
{

}