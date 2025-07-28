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
    /// <summary>
    /// FOR NOW DOES NOTHING
    /// </summary>
    void Inject();



}

public interface IGameCamera : IInjectable
{

    void Inject(GameObject player, Transform orientation);


}