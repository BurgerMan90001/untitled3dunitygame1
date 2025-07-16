using UnityEngine;

public class ShopManager : MonoBehaviour
{
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
