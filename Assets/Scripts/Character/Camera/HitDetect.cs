using UnityEngine;

public class HitDetect
{
    private readonly Transform _camera;

    private RaycastHit _hitInfo;
    public bool HitSomething { get; private set; }

    public RaycastHit HitInfo { get; private set; }

    public HitDetect(Transform camera)
    {
        _camera = camera;
    }

    public void ShootRayCastFromCamera(bool showDebugRayCast, float distance, LayerMask mask)
    {
        ShowDebugRayCast(showDebugRayCast, distance);

        HitSomething = Physics.Raycast(_camera.position, _camera.forward, out _hitInfo, distance, mask); // camera.forward is where the camera is targeting.
        if (HitSomething)
        {
            HitInfo = _hitInfo; // update the public property if it hit something
        }

    }

    private void ShowDebugRayCast(bool showDebugRayCast, float distance)
    {
        if (showDebugRayCast)
        {
            Debug.DrawRay(_camera.position, _camera.forward * distance, Color.red, 2.5f);
        }
    }
}