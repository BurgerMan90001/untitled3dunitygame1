using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
[RequireComponent(typeof(Rigidbody))]
public class FloatingObject : MonoBehaviour
{

    private Rigidbody _rigidBody;

    [Header("Settings")]
    [SerializeField] private float _error = 0.01f;
    [SerializeField] private int _maxInterations = 8;
    [SerializeField] private float _depthThreshhold = 1f;

    [Header("Debug")]
    [SerializeField] private bool _drawRay = true;

    private WaterSearchParameters _searchParameters = new WaterSearchParameters();
    private WaterSearchResult _searchResult = new WaterSearchResult();


    private IWaterBody _targetSurface = null;
    private void Awake()
    {
        _searchParameters.startPositionWS = _searchResult.candidateLocationWS;
        _searchParameters.targetPositionWS = gameObject.transform.position;

        _searchParameters.error = _error;
        _searchParameters.maxIterations = _maxInterations;

        _rigidBody = GetComponent<Rigidbody>();


    }
    private void FloatObject()
    {
        if (_targetSurface.WaterSurface.ProjectPointOnWaterSurface(_searchParameters, out _searchResult))
        {
            var depth = -(_searchResult.projectedPositionWS.y - gameObject.transform.position.y);

            AddBuoyancyForce(depth);

        }
        else
        {
            Debug.LogError("Can't Find Projected Position");

        }
    }
    private void AddBuoyancyForce(float depth)
    {
        if (_rigidBody.TryAddBuoyancyForce(depth, _targetSurface.Density, out Vector3 buoyantForce))
        {
            if (_drawRay)
            {
                _rigidBody.DrawRay(buoyantForce, Color.red);

            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IWaterBody waterSurface))
        {
            if (waterSurface == null)
            {
                Debug.LogError($"{waterSurface} is null");
                return;
            }
            _targetSurface = waterSurface;

            _rigidBody.linearDamping = waterSurface.LinearDamping;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        FloatObject();
    }

}