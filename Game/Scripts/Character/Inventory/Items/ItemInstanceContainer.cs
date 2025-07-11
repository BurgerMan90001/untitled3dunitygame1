using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour
{

    [SerializeField] protected ItemInstance _itemInstance;
    #region
    /// <summary>
    /// // create a new item instance and returns it so that it can be taken by someone
    /// </summary>
    /// <returns></returns
    #endregion
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
    public virtual void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}

