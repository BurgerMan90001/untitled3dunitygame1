
using UnityEngine;
using UnityEngine.InputSystem;
//TODO MAYBE MAKE NOT SINGLETON FOR MULTI


/// <summary>
/// all of the actions that the camera will handle
/// </summary>

public class GameCamera : MonoBehaviour, IGameCamera
{
    private Interact _interact;
    private PositionCamera _positionCamera;
    private RotateCamera _rotateCamera;
    private MouseClick _mouseClick;
    private HitDetect _hitDetect;

    private Vector2 _lookInput;
    private Vector2 _mousePosition;

    private Transform _cameraTransform;

    [Header("Dependencies")]
    //  [SerializeField] private Inventory _inventory;

    [SerializeField] private CameraInput _input;

    // [SerializeField] private Transform _rainPosition;

    [Header("Hit Detect Settings")]
    [SerializeField] private LayerMask _mask;

    [Header("Look Settings")]
    [Range(0.1f, 100f)][SerializeField] private float _sensitivityY = 2f;
    [Range(0.1f, 100f)][SerializeField] private float _sensitivityX = 2f;
    [Range(0f, 90f)][SerializeField] private float _verticalRotationLimit = 88f;

    [Header("Interact Settings")]
    [SerializeField] private float _interactDistance = 5f;
    [SerializeField] private bool _showInteractDebugRayCast = true;
    [SerializeField] private LayerMask _interactMask;



    [Header("MouseClick Settings")]
    [SerializeField] private bool _showMouseClickDebugRayCast = true;
    [SerializeField] private float _leftClickDistance = 5f;
    [SerializeField] private LayerMask _leftClickMask;

    // injected dependancies
    private GameObject _player;
    private Transform _orientation;

    //   private bool _initialized = false;
    public void Initilize(GameObject player, Transform orientation)
    {
        _player = player;
        _orientation = orientation;

        //   _initialized = true;
    }

    private void Awake()
    {


        _cameraTransform = transform;


        // the camera transform is this script's transform because it will be on the cinemachine camera gameobject

        _hitDetect = new HitDetect(_cameraTransform);
        _interact = new Interact(_hitDetect, _interactMask);
        _rotateCamera = new RotateCamera(_verticalRotationLimit);
        _mouseClick = new MouseClick(_cameraTransform, _hitDetect, _leftClickMask);
        _positionCamera = new PositionCamera();

    }


    private void OnEnable()
    {
        _input.RegisterInputEvent(_input.LookAction, OnLook);
        _input.RegisterInputEvent(_input.InteractAction, OnInteract);
        _input.RegisterInputEvent(_input.LeftClickAction, OnLeftClick);
        _input.RegisterInputEvent(_input.PickupAction, OnPickup);
    }
    private void OnDisable()
    {
        _input.UnregisterInputEvent(_input.LookAction, OnLook);
        _input.UnregisterInputEvent(_input.InteractAction, OnInteract);
        _input.UnregisterInputEvent(_input.LeftClickAction, OnLeftClick);
        _input.UnregisterInputEvent(_input.PickupAction, OnPickup);
    }
    public void OnLeftClick(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            _mouseClick.StartLeftClick(_leftClickDistance, _showMouseClickDebugRayCast);
        }
        else if (ctx.canceled)
        {
            _mouseClick.CancleLeftClick();
        }

    }
    public void OnLook(InputAction.CallbackContext ctx)
    {
        _lookInput = ctx.ReadValue<Vector2>(); // the velocity direction and magnitude from a device, such as a mouse or joystick

        //    _mousePosition = Mouse.current.position.ReadValue(); // get the current mouse position

    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {

        if (ctx.started)
        {

            _interact.StartInteract(_interactDistance, _showInteractDebugRayCast, _player);

        }
        else if (ctx.canceled)
        {
            _interact.CancelInteract();
        }
    }
    public void OnPickup(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            throw new System.NotImplementedException();
        }
    }

    private void LateUpdate()
    {
        /*
        if (!_initialized)
        {
            Debug.LogError("CameraActions has not been initialized");
            return;
        }
        */
        _rotateCamera.Rotate(transform, _orientation, _lookInput, _sensitivityY, _sensitivityX);
        _positionCamera.MoveCameraPosition(_cameraTransform, _orientation);

    }
}


