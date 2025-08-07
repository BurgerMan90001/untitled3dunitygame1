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

    [Header("Player Prefabs Keys")]

    [SerializeField] private AssetReferenceGameObject _cameraPrefabKey;
    [SerializeField] private AssetReferenceGameObject _playerPrefabKey;


    private IGameCamera _camera;
    private IPlayerMovement _playerMovement;


    /*
    public async override void InstantiatePoolObjects()
    {
        var playerInstanceGO = await InstantiateObject(_playerPrefabKey);
        var cameraInstanceGO = await InstantiateObject(_cameraPrefabKey);


        SceneManager.MoveGameObjectToScene(playerInstanceGO, PoolScene);
        SceneManager.MoveGameObjectToScene(cameraInstanceGO, PoolScene);

        _camera = cameraInstanceGO.GetComponent<IGameCamera>();
        _playerMovement = playerInstanceGO.GetComponent<IPlayerMovement>();

    }
    */

}
