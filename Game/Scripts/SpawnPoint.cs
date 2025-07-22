using UnityEngine;
/// <summary>
/// <br> Spawns an object at the of the SpawnPoint's transform. </br>
/// </summary>
public class SpawnPoint : MonoBehaviour
{
    [Header("Game Object")]
    [SerializeField] private Transform _gameObject;
    private void Start()
    {
        SetPosition(_gameObject);
    }
    private void SetPosition(Transform gameObject)
    {

        gameObject.transform.position = transform.position;
    }

}
