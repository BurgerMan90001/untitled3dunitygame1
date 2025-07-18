

using UnityEngine;

[CreateAssetMenu(menuName = "Items/Itempools/ShopItempool")]
public class ShopItemPool : ItemPool
{
    public ShopType ShopType;

    private void OnValidate()
    {
        AddItems();
    }
}