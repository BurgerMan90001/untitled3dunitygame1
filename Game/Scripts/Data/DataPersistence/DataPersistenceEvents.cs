using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
/// <summary>
/// <br> Unessecary wrappers. </br>
/// </summary>
[CreateAssetMenu(menuName = "DataPersistence/DataPersistenceData")]
public class DataPersistenceData : ScriptableObject 
{

    public event Action OnSaveGame;
    public event Action OnLoadSaveData;
    public event Action OnStartNewGame;
    public event Action OnLoadGame;

    [Header("Debug")]
    [SerializeField] private bool _debugMode = true;
    public GameData GameData { get; private set; } 

    public Action<string> OnChangeDataProfileID;
    
    public Func<Dictionary<string, GameData>> OnGetAllProfilesGameData;
    public void StartNewGame()
    {
        if (_debugMode)
        {
            Debug.Log("Started new game.");
        }
        OnStartNewGame?.Invoke();
    }
    public void SaveGameData()
    {
        if (_debugMode)
        {
            Debug.Log("Saved game data.");
        }
        OnSaveGame?.Invoke();
    }
    public void LoadSaveData()
    {
        if (_debugMode)
        {
            Debug.Log("Loaded save data.");
        }
        OnLoadSaveData?.Invoke();
    }
    public void ChangeSelectedProfileID(string profileID)
    {
        OnChangeDataProfileID?.Invoke(profileID);
    }


    public void SetGameData(GameData gameData)
    {
        if (_debugMode)
        {
            Debug.Log("Set game data.");
        }
        GameData = gameData;
    }
    // funcs return values
    public Dictionary<string, GameData> GetAllProfilesGameData()
    {
        return OnGetAllProfilesGameData.Invoke();
    }
    public bool SearchForSaveGameData()
    {
        return GameData != null;
    }
}
