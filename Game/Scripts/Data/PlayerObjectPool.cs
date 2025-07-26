using UnityEngine;
using UnityEngine.AddressableAssets;
// TODO INSTANTIATE SCRIPTABLE OBJECTS WITH GAME OBJECTS.
// MAYBE ADD MULTIPLAYER
/// <summary>
/// <br> Creates an instance of a player. </br>
/// </summary>
[CreateAssetMenu(menuName = "ObjectPool/PlayerObjectPool")]
public class PlayerObjectPool : ObjectPool
{

    [Header("Player Prefabs")]

    [SerializeField] private AssetReferenceGameObject _cameraPrefab;
    [SerializeField] private AssetReferenceGameObject _playerPrefab;


    private IGameCamera _camera;
    private IPlayerMovement _playerMovement;



    public override void InstantiatePoolObjects()
    {
        /*
        var playerInstance = _playerPrefab.InstantiateAsync();
        var cameraInstance = _cameraPrefab.InstantiateAsync();

        SceneManager.MoveGameObjectToScene(playerInstance, PoolScene);
        SceneManager.MoveGameObjectToScene(cameraInstance, PoolScene);

        _camera = cameraInstance.GetComponent<IGameCamera>();
        _playerMovement = playerInstance.GetComponent<IPlayerMovement>();



        _playerMovement.Initilize();

        _camera.Initilize(_playerMovement.GameObject, _playerMovement.Orientation);
        */
    }

}
