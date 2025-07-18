using KaimiraGames;
using System.Collections.Generic;
using UnityEngine;
// TODO OPTIMIXZE 
[CreateAssetMenu(menuName = "Items/Itempools/ItemPool")]
public class ItemPool : ScriptableObject
{
    public List<ItemRarityPool> ItemWeightPools;

    public int Test;


    public WeightedList<Item> Items = new();


    private void OnValidate()
    {
        AddItems();
    }
    private void OnEnable()
    {
        AddItems();

     //   test();
    }
    private void OnDisable()
    {
        
    }


    private void test()
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(GetNextRandomItem().name);
        }
    }
    /// <summary>
    /// <br> Adds multiple items so that weight calculation is done a single time. </br>
    /// </summary>
    public void AddItems()
    {

        List<WeightedListItem<Item>> myItems = new();

        

        for (int i = 0; i < ItemWeightPools.Count; i++) 
        {
            ItemRarityPool pool = ItemWeightPools[i];
            for (int j = 0; j < pool.Items.Count; j++)
            {
                WeightedListItem<Item> weightedListItem = new WeightedListItem<Item>(pool.Items[j], pool.Weight);
                myItems.Add(weightedListItem);
            }
            

        }
        
        Items.Add(myItems);
        
    }

    public Item GetNextRandomItem()
    {

        return Items.Next();
    }

    
    
}
