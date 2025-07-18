using System;
using UnityEngine;



public class NPCShop: MonoBehaviour
{
    [Header("Shop Settings")]
 //   [SerializeField] private ShopType _shopType;
    [SerializeField] private ShopItemPool _shopItemPool;

    private ShopData _shopData;
    private string _shopGuid; // MAYBE

    
    /// <summary>
    /// <br> Dependancy injection for npcs. </br>
    /// </summary>
    /// <param name="shopData"></param>
    public void Initialize(ShopData shopData, string shopGuid)
    {
        _shopData = shopData;
        _shopGuid = shopGuid;
    }

    public void GenerateContents()
    {
        
        switch (_shopItemPool.ShopType)
        {
            case ShopType.Lower:

                break;
            case ShopType.Middle:

                break;
            case ShopType.Upper:

                break;
            
            default:
                Debug.LogError("Could not find the shop's type!");
                break;
        }
    }
    /// <summary>
    /// <br> Called when the player interacts with the shop npc.</br>
    /// </summary>
    public void ShopInteraction()
    {
        if (!_shopData.InShop) // if not in the shop
        {
            _shopData.EnterShop(_shopGuid);


        }
        else // if already in shop, exit shop when interacted with
        {
            _shopData.ExitShop();
        }

    }
}

public class ShopManager
{

}
