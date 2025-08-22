using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Inventory/ItemPack")]
public class ItemPack : Item
{
    [field: SerializeField] public PackType PackType { get; protected set; }

    public List<Item> PossibleItems = new List<Item>(); // Items that can be in this pack
    public List<PackReward> GuaranteedRewards = new List<PackReward>(); // Guaranteed items

    [field: SerializeField] public int MaxItems { get; protected set; }


    public ItemPack() : base() // when creating a new item SO pack, these are the default values
    {
        Description = "This is a placehodler for a pack please remove.";
        MaxItems = 5;
        PackType = PackType.Random;
    }



}