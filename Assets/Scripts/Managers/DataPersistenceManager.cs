using MyBox;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// profileIDs are directories which store a json save file
public class DataPersistenceManager : MonoBehaviour
{


    [Header("Events")]
    [SerializeField] private DataPersistenceEvents _dataPersistenceEvents;

    [Header("File Storage")]
    [ReadOnly][SerializeField] private string _selectedProfileID = "";

    [SerializeField] private string _fileName;
    [SerializeField] private string _storyStateFileName;


    [Header("Debugging")]
    [SerializeField] private bool _enableDataPersistence = false;
    [ConditionalField(nameof(_enableDataPersistence))][SerializeField] private bool _loadNewGameIFDataIfNull = false;
    [ConditionalField(nameof(_enableDataPersistence))][SerializeField] private bool _overrideSelectedProfileID = false;
    [ConditionalField(nameof(_overrideSelectedProfileID))][SerializeField] private string _testSelectedProfileID = "test";


    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _fileDataHandler;


    private void Awake()
    {

        if (!_enableDataPersistence)
        {
            Debug.LogWarning("Data persistence is off!");
            return;
        }

        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        Debug.Log(Application.persistentDataPath + _fileName);
        _selectedProfileID = _fileDataHandler.GetMostRecentlyUpdatedProfileID();

        if (_overrideSelectedProfileID) // for testing and debugging
        {
            _selectedProfileID = _testSelectedProfileID;
            Debug.LogWarning("The selected profileID is being overridden with a test ID: " + _testSelectedProfileID);
        }


    }
    private void Start()
    {

        _dataPersistenceObjects = FindAllDistancePersistenceObjects();


    }

    private void OnEnable()
    {
        _dataPersistenceEvents.OnStartNewGame += OnNewGame;

        _dataPersistenceEvents.OnLoadGame += OnLoadGame;

        _dataPersistenceEvents.OnGetAllProfilesGameData += GetAllProfilesGameData;

    }


    private void OnDisable()
    {

        _dataPersistenceEvents.OnStartNewGame -= OnNewGame;

        _dataPersistenceEvents.OnLoadGame -= OnLoadGame;

        _dataPersistenceEvents.OnGetAllProfilesGameData -= GetAllProfilesGameData;

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }
    /*
    public void ChangeSelectedProfileID(string newProfileID)
    {
        selectedProfileID = newProfileID;
        OnLoadGame();
    }
    */
    private void OnNewGame()
    {
        _dataPersistenceEvents.SetGameData(new GameData());


    }
    private void OnLoadGame()
    {
        if (!_enableDataPersistence)
        { // if saving and loading is disabled
            Debug.LogWarning("Data persistence is disabled. Cannot load game data.");
            return;
        }
        _dataPersistenceEvents.SetGameData(_fileDataHandler.Load(_selectedProfileID));
        if (_dataPersistenceEvents.GameData == null)
        {
            Debug.Log("No data was found. A new game needs to be started to load");
            return;
        }
        if (_dataPersistenceEvents.GameData == null && _loadNewGameIFDataIfNull)
        {
            OnNewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_dataPersistenceEvents.GameData);
        }

    }
    private void SaveGame()
    {
        if (!_enableDataPersistence)
        {
            Debug.LogWarning("Data persistence is disabled. Cannot save game data.");
            return;
        }
        if (_dataPersistenceEvents.GameData == null)
        {
            Debug.LogWarning("No save game data was found. A new game needs to be started to load");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(_dataPersistenceEvents.GameData);

        }

        Debug.Log(System.DateTime.Now.ToBinary());
        _dataPersistenceEvents.GameData.LastUpdated = System.DateTime.Now.ToBinary();
        _fileDataHandler.Save(_dataPersistenceEvents.GameData, _selectedProfileID);
    }


    private List<IDataPersistence> FindAllDistancePersistenceObjects()
    {
        var dataPersistenceObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IDataPersistence>().ToList();
        Debug.Log($" Data persistence objects : {dataPersistenceObjects.Count}");
        return dataPersistenceObjects;
    }

    private Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }


}


public class DataLoader
{
    public async Task<List<IDataPersistence>> FindAllDistancePersistenceObjects(AssetLabelReference labelReference)
    {
        var dataLabelHandle = Addressables.LoadAssetsAsync<IDataPersistence>(labelReference.labelString);

        await dataLabelHandle.Task;

        if (dataLabelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            return (List<IDataPersistence>)dataLabelHandle.Result;
        }
        else
        {
            Debug.LogError($"Could not load {labelReference}. ");
            return null;
        }

    }
}
