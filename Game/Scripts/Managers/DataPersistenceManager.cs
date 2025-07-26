using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// profileIDs are directories which store a json save file
public class DataPersistenceManager : Manager
{
    /*
    private static DataPersistenceManager _Instance;
    public static DataPersistenceManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<DataPersistenceManager>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
    */
    [Header("LabelReferences")]
    [SerializeField] private AssetLabelReference _dataLabelReference;

    [Header("Events")]
    [SerializeField] private DataPersistenceEvents _dataPersistenceEvents;

    [Header("File Storage")]
    [SerializeField] private string _fileName;
    [SerializeField] private string _storyStateFileName;

    [Header("Debugging")]
    [SerializeField] private bool _loadDataDebug = false;

    [SerializeField] private bool _loadNewGameIFDataIfNull = false;
    [SerializeField] private bool _disableDataPersistence = false;
    [SerializeField] private bool _overrideSelectedProfileID = false;
    [SerializeField] private string _testSelectedProfileID = "test";


    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _fileDataHandler;

    private string selectedProfileID = "";
    private void Awake()
    {

        if (_disableDataPersistence)
        {
            Debug.LogWarning("Data persistence is off!");
        }

        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        Debug.Log(Application.persistentDataPath + _fileName);
        selectedProfileID = _fileDataHandler.GetMostRecentlyUpdatedProfileID();

        if (_overrideSelectedProfileID) // for testing and debugging
        {
            selectedProfileID = _testSelectedProfileID;
            Debug.LogWarning("The selected profileID is being overridden with a test ID: " + _testSelectedProfileID);
        }


    }
    private async void Start()
    {
        _dataPersistenceObjects = await FindAllDistancePersistenceObjects(_dataLabelReference);


    }

    private void OnEnable()
    {
        //   SceneLoadingManager.OnSceneLoaded += OnSceneLoaded;

        _dataPersistenceEvents.OnStartNewGame += NewGame;

        _dataPersistenceEvents.OnLoadGame += LoadGame;

        _dataPersistenceEvents.OnGetAllProfilesGameData += GetAllProfilesGameData;

    }



    private void OnDisable()
    {

        //    SceneLoadingManager.OnSceneLoaded -= OnSceneLoaded;

        _dataPersistenceEvents.OnStartNewGame -= NewGame;

        _dataPersistenceEvents.OnLoadGame -= LoadGame;

        _dataPersistenceEvents.OnGetAllProfilesGameData -= GetAllProfilesGameData;

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    #region
    /// <summary>
    /// <br> When a scene loads, finds all objects that implement IDataPersistence. </br>
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    #endregion
    private async void OnSceneLoaded(UserInterfaceType _, bool _1)
    {

        //    _dataPersistenceObjects = await FindAllDistancePersistenceObjects(_dataLabelReference).Result;
        Debug.Log(_dataPersistenceObjects.Count);
        LoadGame();
    }
    public void ChangeSelectedProfileID(string newProfileID)
    {
        selectedProfileID = newProfileID;
        LoadGame();
    }
    private void NewGame()
    {
        _dataPersistenceEvents.SetGameData(new GameData());


    }
    private void LoadGame()
    {
        if (_disableDataPersistence)
        { // if saving and loading is disabled
            return;
        }
        _dataPersistenceEvents.SetGameData(_fileDataHandler.Load(selectedProfileID));
        if (_dataPersistenceEvents.GameData == null)
        {
            Debug.Log("No data was found. A new game needs to be started to load");
            return;
        }
        if (_dataPersistenceEvents.GameData == null && _loadNewGameIFDataIfNull)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_dataPersistenceEvents.GameData);
        }

    }
    private void SaveGame()
    {
        if (_disableDataPersistence)
        {
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
        _dataPersistenceEvents.GameData.LastUpdated = System.DateTime.Now.ToBinary();
        _fileDataHandler.Save(_dataPersistenceEvents.GameData, selectedProfileID);
    }


    private async Task<List<IDataPersistence>> FindAllDistancePersistenceObjects(AssetLabelReference labelReference)
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
    private Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }


    public override void Initialize()
    {
        Debug.Log("Datapersistence manager Initialized.");
        throw new System.NotImplementedException();
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
