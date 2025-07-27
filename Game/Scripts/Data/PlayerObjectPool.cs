using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
// TODO INSTANTIATE SCRIPTABLE OBJECTS WITH GAME OBJECTS.
// MAYBE ADD MULTIPLAYER
/// <summary>
/// <br> Creates an instance of a player. </br>
/// </summary>
[CreateAssetMenu(menuName = "ObjectPool/PlayerObjectPool")]
public class PlayerObjectPool : ObjectPool
{

    [Header("Player Prefabs Keys")]

    [SerializeField] private AssetReferenceGameObject _cameraPrefabKey;
    [SerializeField] private AssetReferenceGameObject _playerPrefabKey;


    private IGameCamera _camera;
    private IPlayerMovement _playerMovement;



    public async override void InstantiatePoolObjects()
    {
        var playerInstance = await InstantiateObject(_playerPrefabKey);
        var cameraInstance = await InstantiateObject(_cameraPrefabKey);


        SceneManager.MoveGameObjectToScene(playerInstance, PoolScene);
        SceneManager.MoveGameObjectToScene(cameraInstance, PoolScene);

        _camera = cameraInstance.GetComponent<IGameCamera>();
        _playerMovement = playerInstance.GetComponent<IPlayerMovement>();



        _playerMovement.Initilize();

        _camera.Initilize(_playerMovement.GameObject, _playerMovement.Orientation);

    }

}
