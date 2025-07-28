using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

// NOTES EVENT MANAGER IS INSTATIATED FIRST

//TODO OPTIMIZE AND SHORTEN 
[CreateAssetMenu(menuName = "ObjectPool/ManagerObjectPool")]
public class ManagerObjectPool : ObjectPool
{
    [Header("Data")]
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private GameInput _gameInput;


    [Header("Managers")]
    [SerializeField] private AssetReferenceGameObject _userInterfacePrefabKey;
    [SerializeField] private AssetReferenceGameObject _eventManagerPrefabKey;
    [SerializeField] private AssetReferenceGameObject _dialogueManagerPrefabKey;
    [SerializeField] private AssetReferenceGameObject _combatManagerPrefabKey;
    [SerializeField] private AssetReferenceGameObject _inputManagerPrefabKey;
    [SerializeField] private AssetReferenceGameObject _dataPersistenceManagerPrefabKey;
    [SerializeField] private AssetReferenceGameObject _gameTimeManagerPrefabKey;

    private IEventManager _eventManager;


    public async override void InstantiatePoolObjects()
    {
        var eventManagerInstanceGO = await InstantiateObject(_eventManagerPrefabKey);
        _eventManager = eventManagerInstanceGO.GetComponent<IEventManager>();

        _eventManager.Inject(); // just creates a static singleton instance

        eventManagerInstanceGO.SetActive(true);

        var userInterfaceGO = await InstantiateObject(_userInterfacePrefabKey);
        userInterfaceGO.GetComponent<IUserInterfaceManager>().Inject(_eventManager.DataPersistenceEvents, _eventManager.UserInterfaceEvents, _eventManager.DialogueEvents, _playerInventory);
        userInterfaceGO.SetActive(true);


        var dataPersistenceManagerGO = await InstantiateObject(_dataPersistenceManagerPrefabKey);
        dataPersistenceManagerGO.GetComponent<IDataPersistenceManager>().Inject(_eventManager.DataPersistenceEvents);
        dataPersistenceManagerGO.SetActive(true);

        var dialogueManagerGO = await InstantiateObject(_dialogueManagerPrefabKey);
        dialogueManagerGO.GetComponent<IDialogueManager>().Inject(_eventManager.DialogueEvents);
        dialogueManagerGO.SetActive(true);

        var gameTimeManagerGO = await InstantiateObject(_gameTimeManagerPrefabKey);
        gameTimeManagerGO.GetComponent<IGameTimeManager>().Inject(_eventManager.GameTimeEvents);
        gameTimeManagerGO.SetActive(true);

        var combatManagerInstanceGO = await InstantiateObject(_combatManagerPrefabKey);
        combatManagerInstanceGO.GetComponent<ICombatManager>().Inject(_eventManager.DialogueEvents, _eventManager.CombatEvents);
        combatManagerInstanceGO.SetActive(true);

        var inputManagerInstanceGO = await InstantiateObject(_inputManagerPrefabKey);
        inputManagerInstanceGO.GetComponent<IInputManager>().Inject(_eventManager.DialogueEvents, _eventManager.CombatEvents, _gameInput, _eventManager.UserInterfaceEvents);
        inputManagerInstanceGO.SetActive(true);

        SceneManager.MoveGameObjectToScene(eventManagerInstanceGO, PoolScene);

        SceneManager.MoveGameObjectToScene(userInterfaceGO, PoolScene);

        SceneManager.MoveGameObjectToScene(dataPersistenceManagerGO, PoolScene);

        SceneManager.MoveGameObjectToScene(dialogueManagerGO, PoolScene);

        SceneManager.MoveGameObjectToScene(gameTimeManagerGO, PoolScene);

        SceneManager.MoveGameObjectToScene(combatManagerInstanceGO, PoolScene);

        SceneManager.MoveGameObjectToScene(inputManagerInstanceGO, PoolScene);




    }
}
