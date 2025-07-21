


using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Input/CameraInput")]
public class DebugInput : InputEvent
{
    public bool Debug1Enabled { get; private set; }
    public bool Debug2Enabled { get; private set; }

    [Header("InputActionReferences")]
    [field: SerializeField] public InputActionReference Debug1Action { get; private set; }
    [field: SerializeField] public InputActionReference Debug2Action { get; private set; }

    public void EnableDebug1(bool enabled)
    {
        Debug1Enabled = enabled;
        EnableInputAction(enabled, Debug1Action);
    }
    public void EnableDebug2(bool enabled)
    {
        Debug2Enabled = enabled;
        EnableInputAction(enabled, Debug2Action);
    }
}
