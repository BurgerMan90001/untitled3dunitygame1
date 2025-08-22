
using UnityEngine;


public class MouseClick : HoldableButton
{
    private Transform _camera;
    private GameObject hitGameObject;
    private HitDetect _hitDetect;
    private float _leftClickDistance;
    private bool _showDebugRayCast;

    private LayerMask _mask;
    public MouseClick(Transform camera, HitDetect hitDetect, LayerMask mask, float leftClickDistance, bool showDebugRayCast)
    {
        _hitDetect = hitDetect;
        _camera = camera;
        _mask = mask;
    }

    public override void StartAction()
    {
        ButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(_showDebugRayCast, _leftClickDistance, _mask);

        if (!_hitDetect.HitSomething) return;

        hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        LeftClickAction(hitGameObject);
    }
    public override void CancelAction()
    {
        ButtonHeld = false;
    }
    private void LeftClickAction(GameObject hitGameObject)
    {
    
    }
}