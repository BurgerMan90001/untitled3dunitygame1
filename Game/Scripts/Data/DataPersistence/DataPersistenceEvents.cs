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

    public Action OnSaveGame;
    public Action OnLoadSaveData;

    public Action OnStartNewGame;

    public Action OnLoadGame;

    public GameData GameData { get; private set; } 

    public Action<string> OnChangeDataProfileID;
    
    public Func<Dictionary<string, GameData>> OnGetAllProfilesGameData;
    public void StartNewGame()
    {
        OnStartNewGame?.Invoke();
    }
    public void SaveGameData()
    {
        OnSaveGame?.Invoke();
    }
    public void LoadSaveData()
    {
        OnLoadSaveData?.Invoke();
    }
    public void ChangeSelectedProfileID(string profileID)
    {
        OnChangeDataProfileID?.Invoke(profileID);
    }


    internal void SetGameData(GameData gameData)
    {
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
