
using UnityEngine;


public class Hand : MonoBehaviour
{
    [Header("Dependancies")]
    [SerializeField] private Transform _camera;



    [Header("Position Settings")]
    [SerializeField] private Vector3 _totalOffset;
    [SerializeField] private float _followSpeed = 10f;


    private MeshFilter _meshFilter;


    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
    }
    public void SetHandMesh(ItemInstance itemInstance)
    {

        _meshFilter.mesh = itemInstance.ItemType.Mesh;

    }


    private void LateUpdate()
    {

        transform.localPosition = Vector3.Lerp(transform.localPosition, _totalOffset, _followSpeed);
    }

}
