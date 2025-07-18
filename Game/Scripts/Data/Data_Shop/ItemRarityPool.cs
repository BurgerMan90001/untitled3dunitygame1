
using System.Collections.Generic;
using UnityEngine;

public enum RarityType
{
    Common,
    Uncommon,
    Rare,

}
/// <summary>
/// <br></br>
/// </summary>
[CreateAssetMenu(menuName = "Items/ItemRarityPool")]
public class ItemRarityPool : ScriptableObject
{
    public List<Item> Items;
    public RarityType RarityType;
    /// <summary>
    /// <br> Items' weight ratios in a weighted list are compared to each other.</br>
    /// <br> e.g. In a single list, an item with 10 weight is 33% chance, and an item with 20 weight is a 66% chance. </br>
    /// </summary>
    public int Weight; 

}
