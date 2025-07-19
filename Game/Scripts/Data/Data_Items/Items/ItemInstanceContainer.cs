using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [SerializeField] protected ItemInstance _itemInstance;

    [Header("Target Inventory")]
    [SerializeField] protected Inventory _inventory;
    #region
    /// <summary>
    /// // create a new item instance and returns it so that it can be taken by someone
    /// </summary>
    /// <returns></returns
    #endregion

    /*
    public virtual ItemInstance TakeItem() 
    {

        if (_itemInstance.ItemType is ItemPack itemPack)
        {
            _itemInstance = new ItemPackInstance(itemPack);

            return _itemInstance;
        }
        _itemInstance = new ItemInstance(_itemInstance.ItemType);
        return _itemInstance;
    }
    */
    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }

    public void Interact(GameObject interactor)
    {

        if (_inventory.AddItem(_itemInstance))
        {
            DestroyGameObject();
        }
        /*
        if (hitGameObject.TryGetComponent(out ItemInstanceContainer component))
        {

            ItemInstance item = component.TakeItem();

            if (_inventory.AddItem(item)) // if the item was added to the inventory successfully
            {
                component.DestroyGameObject(); // destroy the game object after taking the item
            }

        }
        */

    }
}

