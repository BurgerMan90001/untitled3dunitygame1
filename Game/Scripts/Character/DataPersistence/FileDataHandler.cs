using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileDataHandler
{
    
    private string dataDirPath = "";
    private string dataFileName = "";


    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileID)
    {
        // base case, if the profileID is null
        if (profileID == null)
        {
            return null;
        }
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd(); // read json file
                    }
                }
                // Deserialize from json back into the c# object
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception exception)
            {
                Debug.LogError("Error occurred when saving data to file: " + fullPath + "\n" + exception);
            }
            
        }
        return loadedData;
    }

    
    

    public void Save(GameData data, string profileID)
    {
        // base case, if the profileID is null
        if (profileID == null)
        {
            return;
        }
        string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
        try
        {
            // if the file wasn't already created
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            //write serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    
                    writer.Write(dataToStore);
                }
            }
        } catch (Exception exception)
        {
            Debug.LogError("Error occurred when saving data to file: " + fullPath + "\n"+ exception);
        }
    }

    public Dictionary<string, GameData> LoadAllProfiles()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).GetDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos)
        {
            string profileID = dirInfo.Name;


            string fullPath = Path.Combine(dataDirPath, profileID, dataFileName);
            if (!File.Exists(fullPath))
            {
                Debug.LogWarning("Skipping directory because it does not have data");
                continue;
            }

            GameData profileData = Load(profileID);
            
            if (profileData != null)
            {
                profileDictionary.Add(profileID, profileData);
            } else
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
            { //then its the most recent one
                mostRecentProfileID= profileID;
            }
            else // else, compare to see which date is most recent
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileID].LastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.LastUpdated);

                if (newDateTime > mostRecentDateTime)
                {
                    mostRecentProfileID = profileID;
                }

            }
        }
        return mostRecentProfileID;
    }
    
}
