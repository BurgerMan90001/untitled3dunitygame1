using UnityEngine;
using UnityEngine.InputSystem;
// MAYBE IMPLEMENT TOGGLEABILITY
[CreateAssetMenu(menuName = "Input/CombatInput")]
public class CombatInput : InputEvent
{
    [Header("InputActionReferences")]
    [field: SerializeField] public InputActionReference AttackAction { get; private set; } // W
    [field: SerializeField] public InputActionReference BlockAction { get; private set; } // D
   // [field: SerializeField] public InputActionReference InteractAction { get; private set; } // S
  //  [field: SerializeField] public InputActionReference LeftClickAction { get; private set; } // A
} 
