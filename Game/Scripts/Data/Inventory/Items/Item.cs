
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    #region
    /// <summary>
    /// Serializable fields.
    /// </summary>
    #endregion
    [field: SerializeField] public string ItemName { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }

    [field: SerializeField][TextArea] public string Description { get; protected set; }
    [field: SerializeField] public bool IsStackable { get; protected set; }
    [field: SerializeField] public float OriginalValue { get; protected set; } // money value, not sell value

    [field: SerializeField] public int MinQuality { get; protected set; } // quality
    [field: SerializeField] public int MaxQuality { get; protected set; } // quality

    public Item() // when creating a new item SO, these are the default values
    {
        Description = "This is a placehodler for an item please remove.";

        IsStackable = true;

        OriginalValue = 0;

        MinQuality = 0;
        MaxQuality = 5;

    }
    
}

