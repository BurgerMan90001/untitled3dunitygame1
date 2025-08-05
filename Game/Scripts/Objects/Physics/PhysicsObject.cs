using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    [Header("Float Settings")]
    [SerializeField] private float _error = 0.01f;
    [SerializeField] private int _maxInterations = 8;
    [SerializeField] private bool _drawBuoyancyRay = true;

    private Rigidbody _rigidBody;

    private Buoyancy _buoyancy;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();

        _buoyancy = new Buoyancy(_rigidBody, _error, _maxInterations);
    }

    private void OnTriggerEnter(Collider other)
    {
        _buoyancy.StartFloat(other);
    }
    private void OnTriggerStay(Collider other)
    {
        _buoyancy.FloatObject();
    }
    private void OnTriggerExit(Collider other)
    {
        _buoyancy.StopFloat();
    }
}

