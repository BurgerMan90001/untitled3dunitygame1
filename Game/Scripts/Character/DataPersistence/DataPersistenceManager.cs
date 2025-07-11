
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
// profileIDs are directories which store a json save file
public class DataPersistenceManager : MonoBehaviour // not a singleton
{
    [Header("File Storage")]
    [SerializeField] private string _fileName;

    [Header("Debugging")]
    [SerializeField] private bool _loadNewGameIFDataIfNull = false;
    [SerializeField] private bool _disableDataPersistence = false;
    [SerializeField] private bool _overrideSelectedProfileID = false;
    [SerializeField] private string _testSelectedProfileID = "test";

    

    private GameData _gameData;
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

    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
    public void NewGame()
    {
        
        _gameData = new GameData();
        

    }
    public void LoadGame()
    {
        if (_disableDataPersistence)
        { // if saving and loading is disabled
            return;
        }
        _gameData = _fileDataHandler.Load(selectedProfileID);
        if (_gameData == null)
        {
            Debug.Log("No data was found. A new game needs to be started to load");
            return;
        }
        if (_gameData == null && _loadNewGameIFDataIfNull)
        {
            NewGame();
        }
            foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
        
    }
    public void SaveGame()
    {
        if (_disableDataPersistence)
        { 
            return;
        }
        if (_gameData == null)
        {
            Debug.LogWarning("No save game data was found. A new game needs to be started to load");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(_gameData);
            
        }
        _gameData.LastUpdated = System.DateTime.Now.ToBinary();
        _fileDataHandler.Save(_gameData, selectedProfileID);
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
    public bool HasSaveGameData()
    {
        return _gameData != null;
    }
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return _fileDataHandler.LoadAllProfiles();
    }
}
