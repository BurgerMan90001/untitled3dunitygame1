using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "ObjectPool/PlayerObjectPool")]
public class PlayerObjectPool : ObjectPool
{
    /*
    [Header("Dependancies")]
    [SerializeField] private Transform _orientation;
    [SerializeField] private GameObject _player;
    */
    [Header("Player Objects")]

    [SerializeField] private GameObject _cameraGameObject;
    [SerializeField] private GameObject _playerGameObject;



    private IGameCamera _camera;
    private IPlayerMovement _playerMovement;



    public override void InstantiatePoolObjects()
    {
        _camera = _cameraGameObject.GetComponent<IGameCamera>();
        _playerMovement = _playerGameObject.GetComponent<IPlayerMovement>();
        Debug.Log("joigdfigpoksdfgps");
        _playerMovement.Initilize();
        _camera.Initilize(_playerMovement.GameObject, _playerMovement.Orientation);


        SceneManager.MoveGameObjectToScene(Instantiate(_playerMovement.GameObject), PoolScene);
        SceneManager.MoveGameObjectToScene(Instantiate(_camera.GameObject), PoolScene);
    }
}
