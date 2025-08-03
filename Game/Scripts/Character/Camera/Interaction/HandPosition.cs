
using UnityEngine;


public class Hand : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private Transform _camera;

    [Header("Position Settings")]
    [SerializeField] private float _xOffset = 0f;
    [SerializeField] private float _yOffset = 0f;
    [SerializeField] private float _zOffset = 1f;
    [SerializeField] private float _followSpeed = 10f;

    [SerializeField] private float _dist = 1f;

    private Vector3 totalOffset;
    private Vector3 cameraEndPoint;

    // private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }

    public void SetHandMesh(ItemInstance itemInstance)
    {

        _meshFilter.mesh = itemInstance.ItemType.Mesh;

    }
    #region
    /// <summary>
    /// Follows the orientation.
    /// </summary>
    #endregion
    private void MoveHand()
    {
        totalOffset = new Vector3(_xOffset, _yOffset, _zOffset);

        Vector3 end = _camera.position + (_camera.forward * _dist);


        transform.position = Vector3.Lerp(transform.position, end, Time.deltaTime * _followSpeed);

    }
    private void LateUpdate()
    {
        MoveHand();
    }
}
