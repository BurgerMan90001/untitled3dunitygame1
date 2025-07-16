using UnityEngine;

[CreateAssetMenu(menuName = "Items/HurtItem")]
public class HurtItem : Item
{
    [field: SerializeField] public float HurtValue { get; protected set; }

    [field: SerializeField] public HurtType HurtType { get; protected set; }

    public HurtItem() : base()
    {
        Description = "This is a placehodler for a Hurt Item please remove.";

        HurtType = HurtType.Physical;

        HurtValue = 0;
    }
}
