
using UnityEngine;

public abstract class HoldableButton
{
    public bool ButtonHeld { get; protected set; }
    public abstract void StartAction();

    public abstract void CancelAction();
}

#region
/// <summary>
/// Provides functionality for interacting with game objects in the scene, such as NPCs, doors, and ladders.
/// </summary>
/// <remarks>The <see cref="Interact"/> class enables interaction with game objects by detecting hits using
/// raycasting and invoking specific behaviors based on the type of object hit.</remarks>
#endregion
public class Interact : HoldableButton
{

    private readonly HitDetect _hitDetect;
    private readonly float _distance;
    private readonly bool _showDebugRayCast;
    private readonly GameObject _player;
    private LayerMask _mask;
    public Interact(HitDetect hitDetect, LayerMask mask, float distance, bool showDebugRayCast, GameObject player)
    {
        _hitDetect = hitDetect;
        _mask = mask;
        _distance = distance;
        _player = player;
        _showDebugRayCast = showDebugRayCast;
    }

    public override void StartAction()
    {
        ButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(_showDebugRayCast, _distance, _mask);

        if (!_hitDetect.HitSomething) return; // if it didn't hit anything do nothing

        GameObject hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        if (hitGameObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact(_player);
        }
    }

    public override void CancelAction()
    {
        ButtonHeld = false;
    }
}

