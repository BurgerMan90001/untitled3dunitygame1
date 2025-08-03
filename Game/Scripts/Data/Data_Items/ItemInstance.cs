using System;
using System.Text;
using UnityEngine;


[System.Serializable]

public class ItemInstance
{

    [field: SerializeField] public virtual Item ItemType { get; protected set; }
    [field: SerializeField] public virtual Mesh Mesh { get; protected set; }

    private readonly float _pricePercent = 0.8f; // eighty percent of original value
    private readonly int _decimalPlaces = 2;
    public Sprite Icon { get; protected set; }
    public int Quantity; // amount of items


    public int Quality; // quality determines the value of the item
    public float Value;

    protected virtual float CalculateValue(float itemValue, int quality, int maxQuality)
    {

        float qualityPercent = (float)quality / maxQuality;
        return (float)Math.Round(itemValue * qualityPercent, _decimalPlaces);
    }
    protected virtual float CalculateSellPrice(float value)
    {
        return (float)Math.Round(value * _pricePercent);
    }

    public float SellPrice;

    [TextArea] public string Description;

    public Action OnValueChanged;

    private StringBuilder _tooltipString = new StringBuilder();
    #region
    /// <summary>
    /// An empty item
    /// </summary>
    #endregion
    public ItemInstance() { }

    public ItemInstance(Item item)  // when a card is first created
    {

        if (item == null)
        {
            Debug.LogError(" Item is trying to be instanced, but it is null.");
            return;
        }

        ItemType = item;
        Icon = item.Icon;

        Quantity = 1;

        Description = item.Description;

        Quality = UnityEngine.Random.Range(1, ItemType.MaxQuality);

        Value = CalculateValue(item.OriginalValue, Quality, ItemType.MaxQuality + 1);
        SellPrice = CalculateSellPrice(Value);

        GetToolTipString();

    }

    public virtual string GetToolTipString() // can be updated whenever
    {
        if (_tooltipString == null)
        {

            _tooltipString = new StringBuilder();
        }
        else
        {
            _tooltipString.Clear();
        }

        _tooltipString.Append("Item: ").Append(ItemType.ToString()).Append("\n")
            .Append("Quantity: ").Append(Quantity.ToString()).Append("\n")
            .Append("\n")
            .Append("Quality: ").Append(Quality.ToString()).Append("\n")
            .Append("Value: ").Append(Value.ToString()).Append("\n")
            .Append("Sell Price: ").Append(SellPrice.ToString()).Append("\n")
            .Append(Description);

        return _tooltipString.ToString();
    }

    public virtual void SetMesh(Mesh mesh)
    {
        Mesh = mesh;
    }
    #region
    /// <summary>
    /// Returns true if the item instance is empty, meaning it has no ItemType assigned.
    /// </summary>
    /// <returns></returns>
    #endregion
    public bool IsEmpty()
    {
        return ItemType == null;
    }

}
