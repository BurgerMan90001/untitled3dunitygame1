using UnityEngine;

public class PickupInteraction : HoldableButton
{
    private readonly HitDetect _hitDetect;
    private readonly float _distance;
    private readonly bool _showDebugRayCast;
    private readonly GameObject _player;
    private LayerMask _mask;
    public PickupInteraction(HitDetect hitDetect, LayerMask mask, float distance, bool showDebugRayCast, GameObject player)
    {
        _hitDetect = hitDetect;
        _mask = mask;
        _distance = distance;
        _player = player;

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
        throw new System.NotImplementedException();
    }
}