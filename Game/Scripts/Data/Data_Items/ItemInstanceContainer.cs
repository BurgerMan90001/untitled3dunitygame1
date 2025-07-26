using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [SerializeField] protected ItemInstance _itemInstance;

    [Header("Target Inventory")]
    [SerializeField] protected Inventory _inventory;
   
    
    public void Interact(GameObject interactor)
    {
        _itemInstance = new ItemInstance(_itemInstance.ItemType);
        if (_inventory.AddItem(_itemInstance))
        {
            Destroy(gameObject);
        }
        

    }
}

