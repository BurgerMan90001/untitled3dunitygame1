using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
// TODO MAKE GHOST IMAGE NOT STRETCH AND OPTIMIZE
#region
/// <summary>
/// <br> For dragging and dropping ui elements. </br>
/// <br> Stolen from Unity docs. </br>
/// <br> https://docs.unity3d.com/Manual/UIE-create-drag-and-drop-ui.html </br>
/// </summary>
#endregion
public class DragAndDropManipulator : PointerManipulator
{

    private readonly VisualElement _root;
    private readonly VisualElement _inventoryBackingPanel;
    private readonly VisualElement _ghostImage;

    private bool _isDraggingElement;

    private Vector2 _ghostImageStartPosition;

    private Vector3 _pointerStartPosition;

    private VisualElement _selectedFullItemSlot;

    private readonly Inventory _inventory;

    UQueryBuilder<VisualElement> _allSlots;

    #region
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="inventoryBackingPanel"></param>
    /// <param name="root"></param>
    #endregion
    public DragAndDropManipulator(VisualElement target, VisualElement ghostImage, 
        VisualElement inventoryBackingPanel, VisualElement root, Inventory inventory)
    {

        this.target = target; // an item slot visual element
        _root = root; // the backing panel or parent of item slots
        _inventoryBackingPanel = inventoryBackingPanel;
        _ghostImage = ghostImage;
        _inventory = inventory;
        

    }
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDown);
        target.RegisterCallback<PointerMoveEvent>(PointerMove);


        target.RegisterCallback<PointerUpEvent>(PointerUp);
        target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOut);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDown);
        target.UnregisterCallback<PointerMoveEvent>(PointerMove);

        target.UnregisterCallback<PointerUpEvent>(PointerUp);
        target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOut);
    }
    #region
    /// <summary>
    /// <br> Queries for all inventory slots.</br>
    /// <br> Updates everytime inventory is changed.</br>
    /// </summary>
#endregion
    public void QueryAllInventorySlots()
    {
        _allSlots = _inventoryBackingPanel.Query<VisualElement>(className: "slotIcon");
    }
    private void PointerDown(PointerDownEvent evt)
    {
        
        if (evt.button == 0) // Left mouse button
        {
            if (GetItemInstanceData<PointerDownEvent>(evt, out ItemInstance itemInstance, out VisualElement hoveredElement))
            {
                ShowGhostImage(evt, itemInstance, hoveredElement);
            }
        }
    }
    #region
    /// <summary>
    /// <br> Generic item instance getter that can get the item instance for any event type. </br>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="evt"></param>
    /// <param name="itemInstance"></param>
    /// <returns></returns>
    #endregion

    private bool GetItemInstanceData<T>(EventBase evt, out ItemInstance itemInstance, out VisualElement hoveredElement)
    {
        
        itemInstance = null;
        hoveredElement = null;
        if (evt.currentTarget is VisualElement foundHoveredElement)
        {
            hoveredElement = foundHoveredElement;
            if (foundHoveredElement.userData is ItemInstance foundItemInstance 
                && foundItemInstance.ItemType != null)
            {
                itemInstance = foundItemInstance;
                return true;
            }
        }
        return false;
    }
    
    private void ShowGhostImage(PointerDownEvent evt, ItemInstance itemInstance, VisualElement hoveredElement)
    {
        _selectedFullItemSlot = hoveredElement;

        float width = _ghostImage.resolvedStyle.width / 2f;
        float height = _ghostImage.resolvedStyle.height / 2f;
        
        Vector3 offset = new Vector3(0, width, 0);

        _ghostImageStartPosition = evt.position - offset;
        _ghostImageStartPosition = RootSpaceOfSlot(_selectedFullItemSlot);
        _ghostImageStartPosition = new Vector2(_ghostImageStartPosition.x - 5, _ghostImageStartPosition.y - 5);
        
        _ghostImage.transform.position = _ghostImageStartPosition;
        _pointerStartPosition = evt.position;

        target.CapturePointer(evt.pointerId);
        _isDraggingElement = true;

        StyleGhostImage(hoveredElement);
    }
    private void StyleGhostImage(VisualElement hoveredElement)
    {
        _ghostImage.style.backgroundImage = hoveredElement.resolvedStyle.backgroundImage;
        _ghostImage.style.display = DisplayStyle.Flex;

    }
    
    
    private void PointerUp(PointerUpEvent evt)
    {
        if (evt.button == 0 || evt.button == 1) // Left mouse button or right
        {
            
            if (_isDraggingElement && target.HasPointerCapture(evt.pointerId)) 
            {
                
                target.ReleasePointer(evt.pointerId);
                
                if (GetItemInstanceData<PointerDownEvent>(evt, out ItemInstance itemInstance, out VisualElement hoveredElement))
                {
                    _ghostImage.style.display = DisplayStyle.None;
                    hoveredElement.style.backgroundImage = Background.FromSprite(itemInstance.Icon);
                }
                
                
            }
            
        }
    }
    #region
    /// <summary>
    /// <br> Moves the target element within the bounds of the window. </br>
    /// </summary>
    /// <param name="evt"></param>
    #endregion
    private void PointerMove(PointerMoveEvent evt) 
    {
        
        
        if (_isDraggingElement && target.HasPointerCapture(evt.pointerId)) {

            Vector3 pointerDelta = evt.position - _pointerStartPosition;

            _ghostImage.transform.position = new Vector2(
                Mathf.Clamp(_ghostImageStartPosition.x + pointerDelta.x, 0, _ghostImage.panel.visualTree.worldBound.width),
                Mathf.Clamp(_ghostImageStartPosition.y + pointerDelta.y, 0, _ghostImage.panel.visualTree.worldBound.height));

        }
            
        
    }
    
    #region
    /// <summary>
    /// <br> Stolen from Unity docs. </br>
    /// <br> Finds all slots in the root and finds the closest overlapping slot. </br>
    /// <br> If there are none close, it resets to the original position. </br>
    /// </summary>
    /// <param name="evt"></param>
    #endregion
    private void PointerCaptureOut(PointerCaptureOutEvent evt)
    {
        QueryAllInventorySlots();
        UQueryBuilder<VisualElement> overlappingSlots =
            _allSlots.Where(OverlapsTarget);
        VisualElement closestOverlappingSlot =
            FindClosestSlot(overlappingSlots);
        Vector3 closestPos = Vector3.zero;
        if (closestOverlappingSlot != null)
        {
            closestPos = RootSpaceOfSlot(closestOverlappingSlot);
            closestPos = new Vector2(closestPos.x - 5, closestPos.y - 5);
            _ghostImage.transform.position = closestPos;

            if (_selectedFullItemSlot.userData is ItemInstance itemInstance // if the previously clicked on or selected slot has an item in it.
                && closestOverlappingSlot.userData is ItemInstance overlappingSlotItemInstance) // if there is an item in the selected item slot
            {
                SwapSlotItems(itemInstance, overlappingSlotItemInstance, closestOverlappingSlot);
                
            }

        } else // if there are no close overlapping slots, reset to original
        {
          
            _ghostImage.transform.position = _ghostImageStartPosition;
        }
        
            
        _isDraggingElement = false;
        

    }
    private void SwapSlotItems(ItemInstance itemInstance, ItemInstance overlappingSlotItemInstance, VisualElement closestOverlappingSlot)
    {
        _inventory.SwapItems(itemInstance, overlappingSlotItemInstance);
        closestOverlappingSlot.style.backgroundImage = Background.FromSprite(itemInstance.Icon);

        closestOverlappingSlot.userData = itemInstance;
        _selectedFullItemSlot.userData = null;


    }
    private bool OverlapsTarget(VisualElement slot)
    {
        return _ghostImage.worldBound.Overlaps(slot.worldBound);
    }

    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
    {
        List<VisualElement> slotsList = slots.ToList();
        float bestDistanceSq = float.MaxValue;
        VisualElement closest = null;
        foreach (VisualElement slot in slotsList)
        {
            Vector3 displacement =
                RootSpaceOfSlot(slot) - _ghostImage.transform.position;
            float distanceSq = displacement.sqrMagnitude;
            if (distanceSq < bestDistanceSq)
            {
                bestDistanceSq = distanceSq;
                closest = slot;
            }
        }
        return closest;
    }

    private Vector3 RootSpaceOfSlot(VisualElement slot)
    {
        Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
        return _root.WorldToLocal(slotWorldSpace);
    }
}


public static class IListExtensions
{
    public static IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
    {
        (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        return list;
    }
}


