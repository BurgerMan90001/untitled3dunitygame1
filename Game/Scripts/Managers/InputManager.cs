
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _playerMovement;
    [SerializeField] private GameObject _playerCamera;

    private PlayerInput _playerInput;

    private void Awake()
    { 
        _playerInput = GetComponent<PlayerInput>();
        
    }
    private void OnEnable()
    {
        
     //   _playerCamera.SetActive(true);
    }
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

}
