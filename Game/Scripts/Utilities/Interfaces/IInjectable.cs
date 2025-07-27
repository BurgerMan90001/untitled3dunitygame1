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
    void Initilise();



}

public interface IGameCamera : IInjectable
{

    void Initilise(GameObject player, Transform orientation);


}