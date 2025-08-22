using UnityEngine.InputSystem;

public interface IInjectable
{

}

public interface IPlayerMovement : IInjectable
{

    void OnMove(InputAction.CallbackContext ctx);
    void OnJump(InputAction.CallbackContext ctx);
    void OnSprint(InputAction.CallbackContext ctx);
    void OnCrouch(InputAction.CallbackContext ctx);

}

public interface IGameCamera : IInjectable
{

    void OnLeftClick(InputAction.CallbackContext ctx);
    void OnLook(InputAction.CallbackContext ctx);
    void OnInteract(InputAction.CallbackContext ctx);
    void OnPickup(InputAction.CallbackContext ctx);

}