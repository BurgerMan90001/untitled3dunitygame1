
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
// profileIDs are directories which store a json save file
public class DataPersistenceManager : MonoBehaviour // not a singleton
{

    [Header("Dependancies")]
    [SerializeField] private DataPersistenceData _dataPersistenceData;

    [Header("File Storage")]
    [SerializeField] private string _fileName;

    [Header("Debugging")]
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

        selectedProfileID = _fileDataHandler.GetMostRecentlyUpdatedProfileID();

        if (_overrideSelectedProfileID) // for testing and debugging
        {
            selectedProfileID = _testSelectedProfileID;
            Debug.LogWarning("The selected profileID is being overridden with a test ID: "+ _testSelectedProfileID);
        }


    }
    
    public void OnEnable()
    {
        
        SceneManager.sceneLoaded += OnSceneLoaded;

        _dataPersistenceData.OnStartNewGame += NewGame;

        _dataPersistenceData.OnLoadGame += LoadGame;


        _dataPersistenceData.OnGetAllProfilesGameData += GetAllProfilesGameData;
    }

    

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        _dataPersistenceData.OnStartNewGame -= NewGame;
        //    _dataPersistenceData.OnLoadSaveData += 

        _dataPersistenceData.OnLoadGame -= LoadGame;



        _dataPersistenceData.OnGetAllProfilesGameData -= GetAllProfilesGameData;

    }
    #region
    /// <summary>
    /// <br> When a scene loads, finds all objects that implement IDataPersistence. </br>
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    #endregion
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _dataPersistenceObjects = FindAllDistancePersistenceObjects();
        LoadGame();
    }
    public void ChangeSelectedProfileID(string newProfileID)
    {
        selectedProfileID = newProfileID;
        LoadGame();
    }
    private void NewGame()
    {
        _dataPersistenceData.SetGameData(new GameData());
       
 
    }
    private void LoadGame()
    {
        if (_disableDataPersistence)
        { // if saving and loading is disabled
            return;
        }
        _dataPersistenceData.SetGameData(_fileDataHandler.Load(selectedProfileID));
        if (_dataPersistenceData.GameData == null)
        {
            Debug.Log("No data was found. A new game needs to be started to load");
            return;
        }
        if (_dataPersistenceData.GameData == null && _loadNewGameIFDataIfNull)
        {
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_dataPersistenceData.GameData);
        }
        
    }
    private void SaveGame()
    {
        if (_disableDataPersistence)
        { 
            return;
        }
        if (_dataPersistenceData.GameData == null)
        {
            Debug.LogWarning("No save game data was found. A new game needs to be started to load");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(_dataPersistenceData.GameData);
            
        }
        _dataPersistenceData.GameData.LastUpdated = System.DateTime.Now.ToBinary();
        _fileDataHandler.Save(_dataPersistenceData.GameData, selectedProfileID);
    }
    
private void OnApplicationQuit() 
    {
       SaveGame();
    }
    private List<IDataPersistence> FindAllDistancePersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = 
            FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include,FindObjectsSortMode.None)
            .OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    
    private Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }
}
