using UnityEngine;

public class ItemInstanceContainer : MonoBehaviour, IInteractable
{
    [Header("Item")]
    [SerializeField] protected ItemInstance _itemInstance;

    /*
    [Header("Target Inventory")]
    [SerializeField] protected Inventory _inventory;
    */
    private Mesh _containerMesh;
    private void Awake()
    {
        _containerMesh = GetComponent<MeshFilter>().mesh;
    }

    public void Interact(GameObject interactor)
    {
        _itemInstance = new ItemInstance(_itemInstance.ItemType);
        _itemInstance.SetMesh(_containerMesh);
        /*
        if (_inventory.AddItem(_itemInstance))
        {
            Destroy(gameObject);
        }
        */

    }
}

