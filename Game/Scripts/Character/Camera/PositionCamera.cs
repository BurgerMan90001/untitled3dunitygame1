
using UnityEngine;

public class PositionCamera // stuff that will follow the camera position
{

    private Transform _camera;
    private Transform _orientation;
    private Transform _rain;
    public PositionCamera()
    {
    }
    public void MoveCameraPosition(Transform camera, Transform orientation)
    {
        camera.position = orientation.position; // the orientation is the position on the player body that will be followed by the camera
    //    DelayRainMovement().Forget();
    }
    private void DelayRainMovement()
    {
    //    await UniTask.Delay(500);
        _rain.position = _orientation.position;
    }
}
