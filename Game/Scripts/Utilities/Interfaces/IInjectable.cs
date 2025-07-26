using UnityEngine;

public interface IInjectable
{
    GameObject GameObject { get; }
}

public interface IPlayerMovement : IInjectable
{
    Transform Orientation { get; }
    //    Transform GetOrientation();
    //   GameObject GetGameObject();
    void Initilize();
}

public interface IGameCamera : IInjectable
{

    void Initilize(GameObject player, Transform orientation);


}