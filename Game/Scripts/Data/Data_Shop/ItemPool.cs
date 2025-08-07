using KaimiraGames;
using MyBox;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Itempools/ItemPool")]
public class ItemPool : ScriptableObject
{

    [DisplayInspector] public List<WeightedListItem<Item>> ItemWeights; // List of items with their weights

    public WeightedList<Item> WeightedItemList = new();

    [Header("Debug")]
    [SerializeField] private bool _testItemPool = false;
    [ConditionalField(nameof(_testItemPool))]
    [SerializeField] private int _tries = 10;

    private void OnEnable()
    {
        Test(_tries);
    }


    #region
    /// <summary>
    /// <br> Adds multiple items so that weight calculation is done a single time. </br>
    /// </summary>
    #endregion
    public void UpdateWeightedList()
    {
        WeightedItemList.Add(ItemWeights);
    }

    public Item GetNextRandomItem()
    {

        return WeightedItemList.Next();
    }
    public void Test(int tries)
    {
        if (_testItemPool)
        {
            Debug.Log("Testing Item Pool");

            UpdateWeightedList();
            for (int i = 0; i < tries; i++)
            {
                Debug.Log(GetNextRandomItem());
            }
        }

    }



}

/*
[System.Serializable]
public class WeightedListItem<T>
{
    public T Item { get; }
    public int Weight { get; }
    public WeightedListItem(T item, int weight)
    {
        Item = item;
        Weight = weight;
    }
    public override string ToString()
    {
        return $"{Item} (Weight: {Weight})";
    }
}
*/