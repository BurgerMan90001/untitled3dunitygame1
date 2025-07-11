
using UnityEngine;


public class HandPosition : MonoBehaviour
{
    
    [SerializeField] private float xOffset = 0f;
    [SerializeField] private float yOffset = 0f;
    [SerializeField] private float zOffset = 1f;
    [SerializeField] private float followSpeed = 10f;

    [SerializeField] private Transform cam;

    private Vector3 totalOffset;

    private Vector3 cameraEndPoint;
    
    private void LateUpdate()
    {
        totalOffset = new Vector3(xOffset, yOffset, zOffset);

        cameraEndPoint = cam.position;
        transform.rotation = cam.rotation;
        transform.position = Vector3.Lerp(transform.position, cameraEndPoint + cam.rotation * totalOffset, Time.deltaTime * followSpeed);
        
    }
}
