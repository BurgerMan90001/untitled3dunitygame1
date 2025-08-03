
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//TODO MAYBE OPTIMIZE INVENTORY UPDATES TO ONLY UPDATE CHANGED ELEMENTS
// TODO MAYBE USE BINDINGS TO OPTIMIZE

public class UI_Inventory : IUserInterface // animation and stuff
{

    private readonly Inventory _inventory;
    private readonly List<VisualElement> _itemVisualElements;

    private ItemDragAndDropManipulator _itemDragAndDropManipulator;
    private TooltipManipulator _tooltipManipulator;


    private VisualElement _inventoryPanel;
    private VisualElement _ghostImage; // the ghost image that will be used to show the item being dragged

    public UI_Inventory(Inventory inventory)
    {
        _inventory = inventory; // the dynamic inventory that this UI_Inventory will use

        _itemVisualElements = new List<VisualElement>();

    }

    public void QueryElements(VisualElement root)
    {
        _inventoryPanel = root.Q<VisualElement>("Panel_Inventory_Left");
        _ghostImage = root.Q<VisualElement>("GhostImage");
    }

    public void Register(VisualElement root)
    {
        for (int i = 0; i < _inventoryPanel.childCount; i++)
        {

            var child = _inventoryPanel[i];

            _itemVisualElements.Add(child);

            _itemDragAndDropManipulator = new ItemDragAndDropManipulator(child, _ghostImage, _inventoryPanel, root, _inventory);
            _tooltipManipulator = new TooltipManipulator(child, root); // tooltip manipulator will be used to show the tooltip


            child.AddManipulator(_tooltipManipulator);
            child.AddManipulator(_itemDragAndDropManipulator);

            if (i < _inventory.Items.Count && _inventory.Items[i] != null) // set the user data
            {
                child.userData = _inventory.Items[i]; // Store directly in the element
            }


        }


    }

    public void Unregister()
    {

        for (int i = 0; i < _inventoryPanel.childCount; i++)
        {

            var child = _inventoryPanel[i];
            if (child == null) continue;

            child?.RemoveManipulator(_tooltipManipulator);
            child?.RemoveManipulator(_itemDragAndDropManipulator);
        }
    }

    #region
    /// <summary>
    /// Updates the ItemSlots dictionary with the current items in the dynamic inventory. 
    /// </summary>
    #endregion
    public void UpdateInterface() // O(n)
    {

        for (int i = 0; i < _inventory.Items.Count; i++)
        {
            VisualElement child = _itemVisualElements[i];

            ItemInstance itemInstance = _inventory.Items[i]; // get the item instance from the dynamic inventory

            if (child == null || itemInstance == null)
            {
                Debug.LogWarning("A child or itemInstance is null");
                continue; // skip if the child is null

            }
            child.userData = itemInstance;

            child.style.backgroundImage = Background.FromSprite(itemInstance.Icon);

        }
    }

}
