using UnityEngine;
using UnityEngine.InputSystem;

#region
/// <summary>
/// <br> Input for menus. e.g. inventory toggles. </br>
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Input/MenuInput")]
public class MenuInput : InputEvent
{
    public bool InventoryToggleEnabled { get; private set; }

    [Header("InputActionReferences")]
    [field: SerializeField] public InputActionReference InventoryToggleAction { get; private set; }

    public void EnableInventoryToggle(bool enabled)
    {
        InventoryToggleEnabled = enabled;
        EnableInputAction(enabled, InventoryToggleAction);
    }
}

