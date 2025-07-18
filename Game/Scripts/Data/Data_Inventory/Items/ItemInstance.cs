using System;
using System.Text;
using UnityEngine;


[System.Serializable]
public class ItemInstance
{
    [field: SerializeField] public virtual Item ItemType { get; protected set; }

    private readonly float _pricePercent = 0.8f; // eighty percent of original value
    private readonly int _decimalPlaces = 2;

    // properties are already hidden but these are for safety
    
    [HideInInspector] public Sprite Icon { get; protected set; }
    [HideInInspector] public int Quantity; // amount of items
    [HideInInspector] public bool IsStackable { get; protected set; }

    [HideInInspector]
    public virtual int Quality { get; private set; }

    [HideInInspector] public virtual int MaxQuality { get; private set; }
    [HideInInspector] public virtual int MinQuality { get; private set; }
    [HideInInspector]
    public virtual float Value { get; private set; }

    protected virtual float CalculateValue(float itemValue, int quality, int maxQuality)
    {
        
        float qualityPercent = (float)quality / maxQuality;
        return (float) Math.Round(itemValue * qualityPercent, _decimalPlaces);
    }
    protected virtual float CalculateSellPrice(float value)
    {
        return (float)Math.Round( value * _pricePercent);
    }
    [HideInInspector]
    public virtual float SellPrice { get; protected set; }

    [HideInInspector][TextArea] public string Description { get; protected set; }

    [HideInInspector] public Action OnValueChanged;

    [HideInInspector] private StringBuilder _tooltipString;
    
    public ItemInstance() { }
    public ItemInstance(Item item)  // when a card is first created
    {
        
        if (item == null) {
            Debug.LogError(" Item is trying to be instanced, but it is null.");
            return;
        }

        ItemType = item;
        Icon = item.Icon;

        IsStackable = item.IsStackable;

        Quantity = 1;

        Description = item.Description;

        MinQuality = item.MinQuality;
        MaxQuality = item.MaxQuality + 1;
        
        Quality = UnityEngine.Random.Range(1, MaxQuality);

        Value = CalculateValue(item.OriginalValue, Quality, MaxQuality);
        SellPrice = CalculateSellPrice(Value);

        _tooltipString = new StringBuilder();

        GetToolTipString();

    }
    public virtual string GetToolTipString() // can be updated whenever
    {
        if (_tooltipString == null)
        {

            _tooltipString = new StringBuilder();
        } else
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
    
}
