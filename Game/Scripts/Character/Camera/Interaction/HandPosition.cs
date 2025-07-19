
using UnityEngine;


public class HandPosition : MonoBehaviour
{
    
    [SerializeField] private float _xOffset = 0f;
    [SerializeField] private float _yOffset = 0f;
    [SerializeField] private float _zOffset = 1f;
    [SerializeField] private float _followSpeed = 10f;

    [SerializeField] private Transform _orientation;

    private Vector3 totalOffset;

    private Vector3 cameraEndPoint;
    
    
    private void MoveHand()
    {
        totalOffset = new Vector3(_xOffset, _yOffset, _zOffset);

        cameraEndPoint = _orientation.position;
        transform.rotation = _orientation.rotation;
        transform.position = Vector3.Lerp(transform.position, cameraEndPoint + _orientation.rotation * totalOffset, Time.deltaTime * _followSpeed);
    }
    private void Update()
    {
        
    }
}
