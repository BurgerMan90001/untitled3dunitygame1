
using MyBox;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
// TODO REDUCE CODE INVENTORY CHANGE IS LAGGY
/// <summary>
/// <br> </br>
/// </summary>

[CreateAssetMenu(menuName = "Items/Inventory")]
public class Inventory : ScriptableObject
{

    public int MaxItems = 28;
    [DisplayInspector] public List<ItemInstance> Items;

    public event Action OnInventoryChanged;

    private void AddToInventory(ItemInstance itemToAdd)
    {
        if (IsThereEmptySlot(out int firstEmptySlotIndex))
        {
            Items[firstEmptySlotIndex] = itemToAdd;
        }
    }
    #region
    /// <summary>
    /// <br> Happens whenever the inventory data is modified in any way. </br>
    /// <br> Invokes the OnInventoryChanged event. </br>
    /// </summary>
    #endregion
    public void InventoryChange()
    {
        OnInventoryChanged?.Invoke();
    }


    private void AddOrStackItem(ItemInstance itemToAdd)
    {
        if (ExistInInventory(itemToAdd, out ItemInstance matchingItem) && matchingItem.IsStackable)
        {
            matchingItem.Quantity += itemToAdd.Quantity;

            Debug.Log("QUANITY ADDDED");
        }
        else if (!ExistInInventory(itemToAdd, out _) || !itemToAdd.IsStackable)
        { // if it does not exist or is not stackable, add a new item

            AddToInventory(itemToAdd);
            Debug.Log("ADDDED NEW ITEM");

        }
        else
        {
            Debug.LogError("Something went wrong when adding the item. ");
        }


    }
    public void OpenPack(ItemPackInstance itemPackInstance)
    {
        Debug.Log("ITS A PACK");
        itemPackInstance.PreviewContents();

        foreach (var item in itemPackInstance.ContainedItems)
        {
            AddOrStackItem(item);

        }
        InventoryChange();


    }
    public virtual bool AddItem(ItemInstance itemToAdd)
    {


        if (itemToAdd is ItemPackInstance itemPackInstance)
        {

            OpenPack(itemPackInstance);
            return true;
        }

        else if (!IsInventoryFull()) // if its a normal item and the inventory is not full
        {
            AddOrStackItem(itemToAdd);
            InventoryChange();

            return true;

        }
        else
        {
            Debug.Log("No space in the inventory for item: " + itemToAdd.ItemType);
            return false;
        }
    }
    public void RemoveItem(int itemIndex)
    {

        if (Items[itemIndex].Quantity > 1)
        {
            Items[itemIndex].Quantity -= 1; // Just ecrease quantity if item is stackable
            InventoryChange();

        }
        else
        {
            Items.RemoveAt(itemIndex);
        }

    }


    #region
    /// <summary>
    /// <br> Clears the inventory and adds empty items the items list. </br>
    /// </summary>
    #endregion
    public void ResetInventory()
    {
        Items.Clear(); // Clear the inventory when the player inventory is enabled

        for (int i = 0; i < MaxItems; i++) // add empty items 
        {
            var item = new ItemInstance();
            Items.Add(item);
        }
    }
    #region
    /// <summary>
    /// Determines whether an item of the specified type exists in the inventory.
    /// </summary>
    /// <param name="itemToAdd">The item to check for existence in the inventory. Only the <see cref="ItemInstance.ItemType"/> property is used
    /// for comparison.</param>
    /// <param name="item">When the method returns, contains the matching item from the inventory if found; otherwise, <see
    /// langword="null"/>.</param>
    /// <returns><see langword="true"/> if an item of the same type as <paramref name="itemToAdd"/> exists in the inventory;
    /// otherwise, <see langword="false"/>.</returns>
    #endregion
    private bool ExistInInventory(ItemInstance itemToAdd, out ItemInstance exsistingItem)
    {
        exsistingItem = null;

        exsistingItem = Items.Find(existingItem => existingItem.ItemType == itemToAdd.ItemType);

        if (exsistingItem != null)
        {
            bool sameQuality = exsistingItem.Quality == itemToAdd.Quality;
            return sameQuality;
        }
        return false;

        // Return true if the item exists in the inventory and they have the same quality
    }
    #region
    /// <summary>
    /// <br> True if there is an null or empty slot.</br>
    ///  <br> Also returns the first empty index it finds.</br>
    /// </summary>
    /// <param name="firstEmptySlotIndex"></param>
    /// <returns></returns>
    #endregion
    private bool IsThereEmptySlot(out int firstEmptySlotIndex)
    {

        firstEmptySlotIndex = Items.FindIndex(item => item.ItemType == null);
        if (firstEmptySlotIndex == -1)
        {
            Debug.LogWarning("There are no more empty slots!");
        }
        return !(firstEmptySlotIndex == -1);
    }

    public bool IsInventoryFull()
    {
        bool hasNulls = Items.Any(item => item.ItemType == null);

        if (Items.Count < MaxItems || hasNulls)
        {
            return false; // Inventory is not full
        }

        return true; // Inventory is full
    }
    #region
    /// <summary>
    /// <br> Swaps the indexes of two item instances in the Items list. </br>
    /// <br> Can either use specific item instances or indexes of item instances. </br>
    /// </summary>
    /// <param name="indexA"></param>
    /// <param name="indexB"></param>
    /// <returns></returns>
    #endregion
    public bool SwapItems(int indexA, int indexB) // TODO make bool function
    {

        SwapIndexes(indexA, indexB);
        InventoryChange();
        return true;
    }
    public bool SwapItems(ItemInstance itemA, ItemInstance itemB)
    {
        int indexA = Items.IndexOf(itemA);
        int indexB = Items.IndexOf(itemB);

        if (indexA == -1 || indexB == -1)
            return false; // One or both items not found

        SwapIndexes(indexA, indexB);
        InventoryChange();
        return true;
    }
    private void SwapIndexes(int indexA, int indexB)
    {
        (Items[indexB], Items[indexA]) = (Items[indexA], Items[indexB]);
    }
}