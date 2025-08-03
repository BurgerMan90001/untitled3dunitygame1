
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

//TODO MAYBE make tooltips automaitcally reapear after dragging
#region
/// <summary>
/// A class for tooltips.
/// </summary>
#endregion
public class TooltipManipulator : PointerManipulator
{

    private readonly StringBuilder _tooltipString;

    private Label _tooltipLabel;

    private Vector2 _tooltipSize = new Vector2(200, 200);
    private Vector2 _screenSize = new Vector2(Screen.width, Screen.height);

    private float _horizontalOffset;
    private float _verticalOffset;



    public TooltipManipulator(VisualElement target, VisualElement root)
    {
        this.target = target;

        _tooltipString = new StringBuilder();

        QueryElements(root);

        _tooltipLabel.Hidden();
    }
    private void QueryElements(VisualElement root)
    {
        _tooltipLabel = root.Q<Label>("Tooltip");
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerEnterEvent>(PointerEnter);
        target.RegisterCallback<PointerOutEvent>(PointerOut);
        target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOut);
    }

    protected override void UnregisterCallbacksFromTarget()
    {

        target.UnregisterCallback<PointerEnterEvent>(PointerEnter);
        target.UnregisterCallback<PointerOutEvent>(PointerOut);
        target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOut);
    }

    private void PointerEnter(PointerEnterEvent evt)
    {
        if (evt.currentTarget is VisualElement hoveredElement)
        {

            if (hoveredElement.TryGetUserData(out ItemInstance itemInstance)
                && itemInstance.ItemType != null)
            {
                ShowTooltip(itemInstance);
            }
        }
    }


    private void ShowTooltip(ItemInstance itemInstance)
    {

        _tooltipLabel.style.height = _tooltipSize.y; // in pixels
        _tooltipLabel.style.width = _tooltipSize.x;

        _tooltipLabel.text = itemInstance.GetToolTipString();

        PositionTooltip();

        StyleText(_tooltipSize);

        _tooltipLabel.Show(); // Makes the tooltip visible.
    }
    private void PointerDown(PointerDownEvent evt)
    {
        if (evt.button == 0 || evt.button == 1)
        {
            _tooltipString.Clear();
            _tooltipLabel.Hide(); // Hides the tooltip when mouse capture is lost.

        }

    }

    private void PointerCaptureOut(PointerCaptureOutEvent evt) // mouse capture out
    {
        //    _tooltipString.Clear();
        _tooltipLabel.Hide(); // Hides the tooltip when mouse capture is lost.

        /*
        if (evt.target is VisualElement hoveredElement)
        {

            if (hoveredElement.userData is ItemInstance itemInstance
                && itemInstance.ItemType != null)
            {
                ShowTooltip(itemInstance);
            }
        }
        */
    }
    private void PointerOut(PointerOutEvent evt) // mouse leave
    {

        _tooltipLabel.Hide(); // Hides the tooltip when mouse leaves the element.
    }
    // positions increase to the right and down, so the top left corner is (0,0) and the bottom right corner is (width, height)
    private void SetOffset(VisualElement element, Vector2 tooltipSize)
    {
        _horizontalOffset = (element.worldBound.size.x / 2f); // ajustable 
        _verticalOffset = (element.worldBound.size.y / 2f);

        if (element.worldBound.position.y + tooltipSize.y >= _screenSize.y)
        {
            _verticalOffset -= tooltipSize.y;
        }
        if (element.worldBound.position.x + tooltipSize.x >= _screenSize.x)
        {
            _horizontalOffset -= tooltipSize.x;
        }
    }
    private void PositionTooltip()
    {
        _screenSize.x = Screen.width;
        _screenSize.y = Screen.height;

        SetOffset(target, _tooltipSize);


        _tooltipLabel.style.left = target.worldBound.x + _horizontalOffset;
        _tooltipLabel.style.top = target.worldBound.y + _verticalOffset;

        _tooltipLabel.style.bottom = _screenSize.y - (target.worldBound.y + _tooltipSize.y + _verticalOffset);
        _tooltipLabel.style.right = _screenSize.x - (target.worldBound.x + _tooltipSize.x + _horizontalOffset);

    }


    private void StyleText(Vector2 tooltipSize)
    {
        _tooltipLabel.style.fontSize = tooltipSize.y * 0.1f; // the tooltip text is set to 10% of the tooltip size.

    }
    public void UpdateText() // called in Update in a monobehaviour
    {

        if (_tooltipLabel.Shown()) // if the tooltip is visible
        {
            _tooltipLabel.text = _tooltipString.ToString();

        }
    }

}
#region MABYE
/*
#region
    /// <summary>
    /// <br> Tooltip size can be ajusted at runtime.</br>
    /// </summary>
    /// <param name="size"></param>
    #endregion
    public void SetToolTipSize(Vector2 toolTipsize)
    {
        _toolTipsize = toolTipsize; // update the size of the tooltip
    }
private void PointerDown(PointerDownEvent evt) // click start
{
    if (evt.button == 0)
    {

        evt.StopPropagation();
    }
}
private void PointerUp(PointerUpEvent evt) // click end
{
    if (evt.button == 0)
    {

        evt.StopPropagation();
    }
}
private void PointerMove(PointerMoveEvent evt) // mouse move MABYE
{
}
*/
#endregion




# region
/*
float mouseXPosition = Mouse.current.position.x.magnitude;
float mouseYPosition = Mouse.current.position.y.magnitude;
*/
#endregion

