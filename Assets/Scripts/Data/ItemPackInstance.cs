
using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemPackInstance : ItemInstance
{
    [HideInInspector] public List<ItemInstance> ContainedItems { get; protected set; } // contained items are generated from the lists of PossibleItems and GuaranteedRewards.
    [HideInInspector] public int MaxItems { get; protected set; }

    [HideInInspector] public PackType PackType { get; protected set; }
    #region
    /// <summary>
    /// Item pools.
    /// </summary
    #endregion
    [HideInInspector] public List<Item> PossibleItems { get; protected set; }
    [HideInInspector] public List<PackReward> GuaranteedRewards { get; protected set; }

    [HideInInspector] public Action OnPackOpened { get; protected set; }

    public ItemPackInstance() : base() { } // empty constructor

    public ItemPackInstance(Item item) : base(item) //whenever a new item pack instance is created
    {

        if (item is ItemPack itemPack)
        {

            ItemType = itemPack;

            ContainedItems = new List<ItemInstance>();

            PackType = itemPack.PackType;

            PossibleItems = itemPack.PossibleItems;
            GuaranteedRewards = itemPack.GuaranteedRewards;

            MaxItems = itemPack.MaxItems;

            GeneratePackContents();

        }
        else if (item == null)
        {
            Debug.LogError("The item being instantiated is not null!");

        }
        else
        {
            Debug.LogError("The item being instantiated is not a an ItemPack!");

        }


    }

    private void GeneratePackContents()
    {

        ContainedItems.Clear(); // so that it can regenerate contents

        AddGuaranteedRewards(); // Add guaranteed rewards first


        AddRandomItems(); // Generate random items


        CalculatePackValue(); // Recalculate pack value based on contents
    }
    private ItemInstance GetRandomItem()
    {
        Item item = PackType switch
        {
            PackType.Random => PossibleItems[UnityEngine.Random.Range(0, PossibleItems.Count)],
            // Implement weighted selection if Item has rarity/weight
            PackType.Weighted => PossibleItems[UnityEngine.Random.Range(0, PossibleItems.Count)],

            _ => PossibleItems[UnityEngine.Random.Range(0, PossibleItems.Count)], // default value 
        };
        return new ItemInstance(item);
    }

    private void CalculatePackValue()
    {

        //   Value = ContainedItems.Sum(item => item.Value * item.Quantity);

    }
    private void AddGuaranteedRewards()
    {
        foreach (var reward in GuaranteedRewards)
        {


            ItemInstance rewardItem = new ItemInstance(reward.Item);


            if (ExistInContainedItems(rewardItem, out ItemInstance exsistingItem))
            {
                exsistingItem.Quantity += 1;
            }
            else
            {
                ContainedItems.Add(rewardItem);
            }

        }
    }

    private void AddRandomItems()
    {
        int itemCount = MaxItems + 1;
        int remainingSlots = itemCount - ContainedItems.Count;

        for (int i = 0; i < remainingSlots; i++)
        {
            if (PossibleItems.Count > 0)
            {
                ItemInstance randomItem = GetRandomItem();
                if (ExistInContainedItems(randomItem, out ItemInstance exsistingItem))
                {
                    exsistingItem.Quantity += 1;
                }
                else
                {
                    ContainedItems.Add(randomItem);
                }


            }
        }
    }
    #region
    /// <summary>
    /// <br> Copyied over from Inventory. </br>
    /// <br> Determines whether an item of the specified type exists in ContainedItems.</br>
    /// </summary>
    /// <param name="itemToAdd"></param>
    /// <param name="exsistingItem"></param>
    /// <returns></returns>
    #endregion
    private bool ExistInContainedItems(ItemInstance itemToAdd, out ItemInstance exsistingItem)
    {
        exsistingItem = null;
        exsistingItem = ContainedItems.Find(existingItem => existingItem.ItemType == itemToAdd.ItemType);

        if (exsistingItem != null)
        {
            bool sameQuality = exsistingItem.Quality == itemToAdd.Quality;
            return sameQuality;
        }
        return false;
        // Return true if the item exists in the ContainedItems and if they are the same quality
    }

    /*
    public override void Use()
    {
        // Override in derived classes for specific use behavior
        Debug.Log($"Using {ItemType.name}");
    }
    */

    public List<ItemInstance> PreviewContents()
    {
        foreach (var item in ContainedItems)
        {
            Debug.Log($"Previewing item: {item.ItemType.name} x{item.Quantity}");
        }
        return new List<ItemInstance>(ContainedItems);
    }
    /*
    public List<ItemInstance> OpenPack()
    {
        var items = new List<ItemInstance>(ContainedItems);

    //    OnPackOpened(); // Trigger any events or callbacks
        ContainedItems.Clear();
        return items;
    }
    */
    /*
    private void PackOpened()
    {
        OnPackOpened?.Invoke(); 
        // Event or callback when pack is opened
        Debug.Log($"Pack opened! Contains {ContainedItems.Count} items:");
        foreach (var item in ContainedItems)
        {
            Debug.Log("ITEM GET : " + item.ItemType);
            Debug.Log($"- {item.ItemType.name} x{item.Quantity}");
        }
    }
    */
}

[System.Serializable]
public class PackReward
{
    public Item Item;
    public float DropChance = 1f; // 1 = 100% chance

}

public enum PackType
{
    Random,     // Completely random items
    Weighted,   // Items have different weights/rarities
}