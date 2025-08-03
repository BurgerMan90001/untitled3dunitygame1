
using UnityEngine;
/// <summary>
/// For gameobjects that are interactable
/// </summary>
public interface IInteractable
{
    void Interact(GameObject interactor);
}
/// <summary>
/// For gameobjects that can be held and carried.
/// </summary>
public interface IHoldable
{
    bool Held { get; set; }
    void StartHold(Transform positionHeldAt);
}