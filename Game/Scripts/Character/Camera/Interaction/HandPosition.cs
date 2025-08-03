
using UnityEngine;


public class Hand : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private Transform _orientation;
    [Header("Position Settings")]
    [SerializeField] private float _xOffset = 0f;
    [SerializeField] private float _yOffset = 0f;
    [SerializeField] private float _zOffset = 1f;
    [SerializeField] private float _followSpeed = 10f;



    [SerializeField] private float _dist = 1f;

    private Vector3 totalOffset;
    private Vector3 cameraEndPoint;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }


    #region
    /// <summary>
    /// Follows the orientation.
    /// </summary>
    #endregion
    private void MoveHand()
    {
        totalOffset = new Vector3(_xOffset, _yOffset, _zOffset);

        cameraEndPoint = _orientation.forward * _dist;

        //   transform.rotation = _orientation.rotation;'
        Vector3 start = transform.position;
        Vector3 end = cameraEndPoint;
        transform.position = Vector3.Lerp(start, end, Time.deltaTime * _followSpeed);
    }
    private void Update()
    {
        MoveHand();
    }
}
