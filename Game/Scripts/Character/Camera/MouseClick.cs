
using UnityEngine;


public class MouseClick
{
    private Transform _camera;
    private GameObject hitGameObject;
    private HitDetect _hitDetect;

    private LayerMask _mask;
    public bool ButtonHeld { get; private set; }
    public MouseClick(Transform camera, HitDetect hitDetect, LayerMask mask)
    {
        _hitDetect = hitDetect;
        _camera = camera;
        _mask = mask;
    }

    public void StartLeftClick(float leftClickDistance, bool showDebugRayCast)
    {
        ButtonHeld = true;
        
        _hitDetect.ShootRayCastFromCamera(showDebugRayCast, leftClickDistance, _mask);

        if (!_hitDetect.HitSomething) return;

        hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        LeftClickAction(hitGameObject);
    }
    public void CancleLeftClick()
    {

        ButtonHeld = false;
    }
    
    private void LeftClickAction(GameObject hitGameObject)
    {

        // if you can't attack then do nothing

        if (hitGameObject.CompareTag("NPC") &&
            hitGameObject.GetComponent<Rigidbody>() != null)
        {

        //    hitGameObject.GetComponent<Rigidbody>().AddForce(cam.forward *hitForce, ForceMode.Impulse);
            

        } else if (hitGameObject.layer == 10) // ground layer
        {
            //hit ground
        }

    }
    
   
    

    // Optional: If you want to do something while holding

}

