
using UnityEngine;

// TODO OPTIMIZE TAG CHECKING AND suthf


#region
/// <summary>
/// Provides functionality for interacting with game objects in the scene, such as NPCs, doors, and ladders.
/// </summary>
/// <remarks>The <see cref="Interact"/> class enables interaction with game objects by detecting hits using
/// raycasting and invoking specific behaviors based on the type of object hit.</remarks>
#endregion
public class Interact
{
    
    private HitDetect _hitDetect;

    private LayerMask _mask;
    private GameObject _interactor;
    public bool ButtonHeld { get; private set; }

    
    public Interact(HitDetect hitDetect, GameObject interactor, LayerMask mask)
    {
        _hitDetect = hitDetect;
        _interactor = interactor;
        _mask = mask;

    }
    
    public void StartInteract(float interactDistance, bool showDebugRayCast)
    {
        ButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(showDebugRayCast, interactDistance, _mask);

        if (!_hitDetect.HitSomething) return; // if it didn't hit anything do nothing
        
        GameObject hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        if (hitGameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(_interactor);
        }


    }
    public void StartPickup()
    {

    }
    
    public void CancelInteract()
    {
        ButtonHeld = false;
    }

}
