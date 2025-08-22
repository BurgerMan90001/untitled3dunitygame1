using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FileDataHandler
{
    private readonly string _dataDirPath;
    private readonly string _dataFileName;
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public GameData Load(string profileID)
    {
        string fullPath = GetFullPath(profileID);
        GameData loadedData = null;
        // base case, if the profileID is null or the file does not exist at the full path
        if (profileID == null || !File.Exists(fullPath))
        {
            return null;
        }
        
        try
        {
            string dataToLoad;
            using (FileStream stream = new(fullPath, FileMode.Open))
            {
                using StreamReader reader = new(stream);
                dataToLoad = reader.ReadToEnd(); // read json file
            }

            loadedData = DeserializeGameDataJson(dataToLoad);
        }
        catch (Exception exception)
        {
            Debug.LogError("Error occurred when saving data to file: " + fullPath + "\n" + exception);
        }
        
        return loadedData;
    }
    /// <summary>
    /// Deserialize from json back into the c# object
    /// </summary>
    /// <param name="dataToLoad"></param>
    /// <returns></returns>
    private GameData DeserializeGameDataJson(string dataToLoad)
    {
        return JsonUtility.FromJson<GameData>(dataToLoad);
    }
    private string GetFullPath(string profileID)
    {
        return Path.Combine(_dataDirPath, profileID, _dataFileName);
    }


    public void Save(GameData data, string profileID)
    {
        // base case, if the profileID is null
        if (profileID == null)
        {
            return;
        }
        string fullPath = GetFullPath(profileID);
        try
        {
            // if the file wasn't already created
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            //write serialized data to the file
            using FileStream stream = new(fullPath, FileMode.Create);

            using StreamWriter writer = new(stream);
            writer.Write(dataToStore);


        }
        catch (Exception exception)
        {
            Debug.LogError("Error occurred when saving data to file: " + fullPath + "\n" + exception);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(_dataDirPath).GetDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;


            string fullPath = Path.Combine(_dataDirPath, profileID, _dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory because it does not have data");
                continue;
            }

            GameData profileData = Load(profileID);

            if (profileData != null)
            {
                profileDictionary.Add(profileID, profileData);
            }
            else
            {
                Debug.LogError("Tried to load profile but something went wrong");
            }
        }

        return profileDictionary;
    }
    public string GetMostRecentlyUpdatedProfileID()
    {
        string mostRecentProfileID = null;
        Dictionary<string, GameData> profilesGameData = LoadAllProfiles();
        foreach (KeyValuePair<string, GameData> pair in profilesGameData)
        {
            string profileID = pair.Key;
            GameData gameData = pair.Value;
            if (gameData == null) //skips if there is no gameData
            {
                continue;
            }

            // if this gameData is the first that exists, it's the most recent one

            if (mostRecentProfileID == null) // if its still null
            { 
                mostRecentProfileID = profileID;
            }
            else // else, compare to see which date is most recent
            {
                if (IsMoreRecent(profilesGameData[mostRecentProfileID].LastUpdated, gameData.LastUpdated))
                {
                    mostRecentProfileID = profileID;
                }

            }
        }
        return mostRecentProfileID;
    }
    private bool IsMoreRecent(long mostRecentLastUpdated, long newDateLastUpdated)
    {
        DateTime mostRecentDateTime = DateTime.FromBinary(mostRecentLastUpdated);
        DateTime newDateTime = DateTime.FromBinary(newDateLastUpdated);
        return newDateTime > mostRecentDateTime;
    }
    
}