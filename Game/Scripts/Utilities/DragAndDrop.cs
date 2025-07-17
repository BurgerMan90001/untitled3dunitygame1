
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private bool _isDragging = false;
    private Camera _mainCamera;
    private Rigidbody _rigidBody;
    private Vector3 offset;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            _rigidBody = rigidbody;
        } else
        {
            Debug.LogError("This draggable gameobject does not have a rigid body.");
            
        }

            
    }

    private void OnMouseDown()
    {
        Debug.Log("GAPODPASd");
        _isDragging = true;
        _rigidBody.useGravity = false;

        _rigidBody.linearDamping = 10f; // Add drag for smoother movement

   //     Vector3 mousePos = _mainCamera.ScreenToWorldRay(Mouse.current.position.ReadValue());
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {


            // Show info in scene view
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red);
            //    offset = transform.position - mousePos;
        }
    }

    private void OnMouseDrag()
    {
        if (_isDragging)
        {
            Vector3 mousePos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector3 targetPos = mousePos + offset;

            _rigidBody.MovePosition(targetPos);
            Debug.Log("ASDJOIOJIOWD");
        }
    }

    private void OnMouseUp()
    {
        _isDragging = false;
        _rigidBody.useGravity = true;
        _rigidBody.linearDamping = 0f;
        Debug.Log("Ddddddddddd");
    }
}