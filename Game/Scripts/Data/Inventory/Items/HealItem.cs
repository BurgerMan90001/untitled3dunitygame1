using UnityEngine;

[CreateAssetMenu(menuName = "Character/Inventory/HealItem")]
public class HealItem : Item
{
    [field: SerializeField] public float HealValue { get; protected set; }
    [field: SerializeField] public HealType HealType { get; protected set; }

    HealItem() : base() 
    {
        Description = "This is a placehodler for a Heal Item please remove.";

        HealType = HealType.Health;

        HealValue = 0;
    }
}

public enum HealType { 
    Energy,

    Health,
}
