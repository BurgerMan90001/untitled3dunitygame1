using UnityEngine;

public class ShopNPC: MonoBehaviour
{
    [Header("Shop Settings")]
    [SerializeField] private ShopType _shopType;



    private ShopData _shopData;
    public void Initialize(ShopData shopData)
    {
        _shopData = shopData;
    }

    public void ShopInteraction()
    {
        _shopData.ShowShop();
    }
}


public enum ShopType
{

}