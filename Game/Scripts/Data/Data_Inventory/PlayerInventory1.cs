using UnityEngine;

public class PlayerInventory : Inventory
{
    [SerializeField] private GeneralStats _stats; // the stats 

    public void SellItem(int index)// for now only players can sell items. 
    {
        _stats.Money += Items[index].SellPrice; 
        RemoveItem(index);
    }

    

}
