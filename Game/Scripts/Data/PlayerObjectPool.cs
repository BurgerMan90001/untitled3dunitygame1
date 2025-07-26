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
    [Header("Player Prefabs")]

    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private GameObject _playerPrefab;



    private IGameCamera _camera;
    private IPlayerMovement _playerMovement;



    public override void InstantiatePoolObjects()
    {


        var playerInstance = Instantiate(_playerPrefab);
        var cameraInstance = Instantiate(_cameraPrefab);

        SceneManager.MoveGameObjectToScene(playerInstance, PoolScene);
        SceneManager.MoveGameObjectToScene(cameraInstance, PoolScene);

        _camera = cameraInstance.GetComponent<IGameCamera>();
        _playerMovement = playerInstance.GetComponent<IPlayerMovement>();



        _playerMovement.Initilize();

        _camera.Initilize(_playerMovement.GameObject, _playerMovement.Orientation);
    }

}
