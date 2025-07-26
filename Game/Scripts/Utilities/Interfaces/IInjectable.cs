using UnityEngine;

public interface IInjectable
{
    //  void Initilize();
}

public interface IPlayerMovement : IInjectable
{
    void Initilize();
}

public interface IGameCamera : IInjectable
{

    void Initilize(GameObject player, Transform orientation);


}