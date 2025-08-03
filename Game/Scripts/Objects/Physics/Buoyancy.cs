using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Buoyancy
{
    private readonly Rigidbody _rigidBody;
    private GameObject GameObject => _rigidBody.gameObject;

    private WaterSearchParameters _searchParameters = new WaterSearchParameters();
    private WaterSearchResult _searchResult = new WaterSearchResult();

    private IWaterBody _targetSurface = null;

    private const bool DrawBuoyancyRay = true;
    public Buoyancy(Rigidbody rigidBody, float error, int maxInterations)
    {
        _rigidBody = rigidBody;

        _searchParameters.startPositionWS = _searchResult.candidateLocationWS;
        _searchParameters.targetPositionWS = GameObject.transform.position;

        _searchParameters.error = error;
        _searchParameters.maxIterations = maxInterations;

    }

    public void StartFloat(Collider other)
    {
        if (other.TryGetComponent(out IWaterBody waterSurface))
        {
            if (waterSurface == null)
            {
                Debug.LogError($"{waterSurface} is null");
                return;
            }
            _targetSurface = waterSurface;

            _rigidBody.SetLinearDamping(waterSurface.LinearDamping);
        }
    }
    public void FloatObject()
    {
        if (_targetSurface.WaterSurface.ProjectPointOnWaterSurface(_searchParameters, out _searchResult))
        {
            var depth = -(_searchResult.projectedPositionWS.y - GameObject.transform.position.y);

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
            if (DrawBuoyancyRay)
            {
                _rigidBody.DrawRay(buoyantForce, Color.red);

            }
        }
    }
}