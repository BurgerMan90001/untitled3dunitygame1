public class PlayerInventory : Inventory
{
    private GeneralStats _generalStats;

    private void Awake()
    {
        _generalStats = GetComponent<GeneralStats>();
    }
    public void SellItem(int index)// for now only players can sell items. 
    {
        _generalStats.Money += Items[index].SellPrice;
        RemoveItem(index);
    }

}
