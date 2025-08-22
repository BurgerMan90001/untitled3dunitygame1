
using UnityEngine;

public class RotateCamera
{
    private float _verticalRotation = 0f;
    private float _horizontalRotation = 0f;
    private readonly float _verticalRotationLimit;

    public RotateCamera(float verticalRotationLimit)
    {
        
        _verticalRotationLimit = verticalRotationLimit;
    }

    public void Rotate(Transform camera, Transform orientation, Vector2 lookInput, float sensitivityY, float sensitivityX)
    {
        
        _verticalRotation -= lookInput.y * sensitivityY * Time.deltaTime;
        _horizontalRotation += lookInput.x * sensitivityX * Time.deltaTime;
        
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_verticalRotationLimit, _verticalRotationLimit);
        camera.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0.0f);
        orientation.transform.rotation = Quaternion.Euler(0f, _horizontalRotation, 0.0f);
    }

}