
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IDataPersistence
{
    public DynamicInventory Inventory;

    [Header("Debug")]
    [SerializeField] private bool clearOnEnable = false;

    private void OnEnable()
    {

        if (clearOnEnable)
        {
            
            Inventory.ResetInventory();
        }


    }

    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        
    }

    public void LoadData(GameData data)
    {
        Inventory.Items = data.InventoryItems;
        
    }

    public void SaveData(GameData data)
    {
        data.InventoryItems = Inventory.Items;
    }
}
