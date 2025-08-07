using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



// TODO REFACTOR TOUGH THOUGH MAYBE
#region
/// <summary>
/// <br> For dragging and dropping ui elements. </br>
/// <br> Stolen from Unity docs. </br>
/// <br> https://docs.unity3d.com/Manual/UIE-create-drag-and-drop-ui.html </br>
/// </summary>
#endregion
public class ItemDragAndDropManipulator : PointerManipulator
{

    private readonly VisualElement _root;
    private readonly VisualElement _ghostImage;

    private VisualElement _selectedItemSlot;

    private readonly Inventory _inventory;
    private UQueryBuilder<VisualElement> _allSlots;

    private Vector2 _ghostImageStartPosition;
    private Vector3 _pointerStartPosition;

    private const int MouseButton = 0; // left mouse button
    private bool _isDraggingElement;

    #region
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="inventoryBackingPanel"></param>
    /// <param name="root"></param>
    #endregion
    public ItemDragAndDropManipulator(VisualElement target, VisualElement ghostImage,
        VisualElement inventoryPanel, VisualElement root, Inventory inventory)
    {

        this.target = target; // an item slot visual element
        _root = root; // the backing panel or parent of item slots

        _ghostImage = ghostImage;
        _inventory = inventory;

        Query(inventoryPanel);

    }
    private void Query(VisualElement inventoryPanel)
    {
        _allSlots = inventoryPanel.Query<VisualElement>(className: "slotIcon");
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

    private void PointerDown(PointerDownEvent evt)
    {

        if (!IsMouseButton(evt.button)) return; // if not left mouse button, do nothing 
        if (GetCurrentTargetItemInstanceData(evt.currentTarget, out _, out VisualElement hoveredElement))
        {
            _selectedItemSlot = hoveredElement; // the slot that was clicked on is the selected slot
            CreateGhostImage(evt, hoveredElement);

        }

    }

    private void PointerUp(PointerUpEvent evt)
    {
        if (IsDraggingItem(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);

            HideGhostImage(evt.currentTarget);

        }
    }

    #region
    /// <summary>
    /// <br> Finds all slots in the root and finds the closest overlapping slot. </br>
    /// <br> If there are none close, it resets to the original position. </br>
    /// </summary>
    /// <param name="evt"></param>
    #endregion
    private void PointerCaptureOut(PointerCaptureOutEvent evt)
    {

        var closestOverlappingSlot = FindClosestOverlappingSlot();
        Vector3 closestPos = Vector3.zero;
        if (closestOverlappingSlot != null)
        {

            closestPos = new Vector2(closestPos.x - 5, closestPos.y - 5);
            _ghostImage.transform.position = closestPos;

            if (CanSwapItems(closestOverlappingSlot, out ItemInstance itemInstance, out ItemInstance overlappingSlotItemInstance))
            {
                SwapSlotItems(itemInstance, overlappingSlotItemInstance, closestOverlappingSlot);

            }
        }
        else // if there are no close overlapping slots, reset to original
        {
            _ghostImage.transform.position = _ghostImageStartPosition;

        }

        _isDraggingElement = false;
    }

    private void CreateGhostImage(PointerDownEvent evt, VisualElement hoveredElement)
    {
        // position the ghost image
        _ghostImageStartPosition = RootSpaceOfSlot(_selectedItemSlot);
        _ghostImage.transform.position = _ghostImageStartPosition;
        _pointerStartPosition = evt.position; // get the initial pointer position

        target.CapturePointer(evt.pointerId);

        // style the ghost image
        _ghostImage.SetBackgroundImage(hoveredElement.resolvedStyle.backgroundImage);

        _ghostImage.Show();

        _isDraggingElement = true;
    }
    private void HideGhostImage(IEventHandler eventCurrentTarget)
    {
        if (GetCurrentTargetItemInstanceData(eventCurrentTarget, out ItemInstance itemInstance, out VisualElement hoveredElement))
        {
            _ghostImage.Hide();
            hoveredElement.SetBackgroundImage(itemInstance.Icon);

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
        if (IsDraggingItem(evt.pointerId))
        {
            MoveGhostImageToPointer(evt);

        }
    }
    private void MoveGhostImageToPointer(PointerMoveEvent evt)
    {
        Vector3 pointerDelta = evt.position - _pointerStartPosition;

        _ghostImage.transform.position = new Vector2(
            Mathf.Clamp(_ghostImageStartPosition.x + pointerDelta.x, 0, _ghostImage.panel.visualTree.worldBound.width),
            Mathf.Clamp(_ghostImageStartPosition.y + pointerDelta.y, 0, _ghostImage.panel.visualTree.worldBound.height));

    }

    #region
    /// <summary>
    /// Determines whether the selected item can be swapped with the item in the closest overlapping slot.
    /// </summary>
    /// <param name="closestOverlappingSlot"></param>
    /// <param name="itemInstance"></param>
    /// <param name="overlappingSlotItemInstance"></param>
    /// <returns></returns>
    #endregion
    private bool CanSwapItems(VisualElement closestOverlappingSlot, out ItemInstance itemInstance, out ItemInstance overlappingSlotItemInstance)
    {
        itemInstance = null;
        overlappingSlotItemInstance = null;

        if (_selectedItemSlot.TryGetUserData(out ItemInstance foundItemInstance))
        {
            itemInstance = foundItemInstance;
            if (closestOverlappingSlot.TryGetUserData(out ItemInstance foundOverlappingSlotItemInstance))
            {
                overlappingSlotItemInstance = foundOverlappingSlotItemInstance;
                return true;
            }
        }
        return false;

    }
    private void SwapSlotItems(ItemInstance itemInstance, ItemInstance overlappingSlotItemInstance, VisualElement closestOverlappingSlot)
    {
        closestOverlappingSlot.SetBackgroundImage(itemInstance.Icon);

        closestOverlappingSlot.userData = itemInstance;
        _selectedItemSlot.userData = null;
        _inventory.TrySwapItems(itemInstance, overlappingSlotItemInstance);


    }
    private bool OverlapsTarget(VisualElement slot)
    {
        return _ghostImage.worldBound.Overlaps(slot.worldBound);
    }
    private VisualElement FindClosestOverlappingSlot()
    {
        UQueryBuilder<VisualElement> overlappingSlots =
            _allSlots.Where(OverlapsTarget);
        return FindClosestSlot(overlappingSlots);

    }
    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
    {
        VisualElement closest = null;

        List<VisualElement> slotsList = slots.ToList();
        float bestDistanceSq = float.MaxValue;

        foreach (VisualElement slot in slotsList)
        {
            Vector3 displacement = RootSpaceOfSlot(slot) - _ghostImage.transform.position;
            float distanceSq = displacement.sqrMagnitude;
            if (distanceSq < bestDistanceSq)
            {
                bestDistanceSq = distanceSq;
                closest = slot;
            }
        }
        return closest;
    }
    /// <summary>
    /// The middle of the item slot in root space.
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    private Vector3 RootSpaceOfSlot(VisualElement slot)
    {
        Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
        return _root.WorldToLocal(slotWorldSpace);
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

    private bool GetCurrentTargetItemInstanceData(IEventHandler eventCurrentTarget, out ItemInstance itemInstance, out VisualElement hoveredElement)
    {

        itemInstance = null;
        hoveredElement = null;
        if (eventCurrentTarget is VisualElement foundHoveredElement)
        {
            hoveredElement = foundHoveredElement;
            if (hoveredElement.TryGetUserData(out ItemInstance foundItemInstance)
                && !foundItemInstance.IsEmpty())
            {
                itemInstance = foundItemInstance;
                return true;
            }
        }
        return false;
    }

    #region
    /// <summary>
    /// Determines whether the specified pointer is currently dragging an item and has pointer capture.
    /// </summary>
    /// <param name="pointerId">The unique identifier of the pointer to check.</param>
    /// <returns><see langword="true"/> if the pointer with the specified <paramref name="pointerId"/> is dragging an item;
    /// otherwise, <see langword="false"/>.</returns>
    #endregion
    private bool IsDraggingItem(int pointerId)
    {
        return _isDraggingElement && target.HasPointerCapture(pointerId);
    }
    #region
    /// <summary>
    /// Checks if the button number is the mouse button number. e.g. 0 for left mouse button.
    /// </summary>
    /// <param name="buttonNumber"></param>
    /// <returns></returns>
    #endregion
    private bool IsMouseButton(int buttonNumber)
    {
        return buttonNumber == MouseButton;
    }
}

