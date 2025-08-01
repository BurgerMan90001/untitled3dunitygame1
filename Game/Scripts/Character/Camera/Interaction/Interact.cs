
using UnityEngine;



#region
/// <summary>
/// Provides functionality for interacting with game objects in the scene, such as NPCs, doors, and ladders.
/// </summary>
/// <remarks>The <see cref="Interact"/> class enables interaction with game objects by detecting hits using
/// raycasting and invoking specific behaviors based on the type of object hit.</remarks>
#endregion
public class Interact
{

    private readonly HitDetect _hitDetect;
    private LayerMask _mask;

    public bool InteractButtonHeld { get; private set; }
    public bool PickupButtonHeld { get; private set; }

    private readonly bool _debugMode;

    public Interact(HitDetect hitDetect, LayerMask mask)
    {
        _hitDetect = hitDetect;
        _mask = mask;


    }

    public void StartInteract(float interactDistance, bool showDebugRayCast, GameObject player)
    {
        InteractButtonButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(showDebugRayCast, interactDistance, _mask);

        if (!_hitDetect.HitSomething) return; // if it didn't hit anything do nothing

        GameObject hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        if (hitGameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(player);
        }



    }
    public void StartPickup(float interactDistance, bool showDebugRayCast, GameObject player)
    {
        PickupButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(showDebugRayCast, interactDistance, _mask);

    }
    public void CancelPickup()
    {

    }
    public void CancelInteract()
    {
        ButtonHeld = false;
    }

}
