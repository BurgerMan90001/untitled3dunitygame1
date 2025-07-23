
using Ink.Runtime;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData // game data is the data that will be serialized and saved into a json file.
{
    // INK STORY
    public StoryState StoryState;


    public long LastUpdated;

    public Inventory Inventory;


    // a list of the class' properties like MaxStamina and Level
    //   private PropertyInfo[] gameDataProperties = typeof(GameData).GetProperties();
    // game data that will persist
    public float MaxStamina;

    public float MaxHealth;

    public float Energy;
    public float MaxEnergy;

    public float Money;

    public float Experience;
    public float Level;


    public float BaseSpeed;



    public Vector3 PlayerPosition;



    public GameData() // new game stats
    {
        Debug.Log("NEW GAME STARTED");

        PlayerPosition = new Vector3(-21.9972f, 54.65f, -37.326f); // area where player will spawn at in newgame

        Energy = 10f; // default values
        MaxEnergy = 10f;
        MaxStamina = 20f;

        Money = 0f;

        MaxHealth = 20f;

        var storyText = TextLoader.LoadTextFile("");

        StoryState = new StoryState();
    }
}

public static class StoryStateSerialization
{
    // Set a path to save and restore StoryState.
    private static readonly string fileName = "currentStoryState.json";
    static string savePath = Application.persistentDataPath + "/currentStoryState.json";

    // Convert a StoryState into a JSON string and save file.
    static public void Serialize(StoryState s)
    {
        // Either create or overwrite an existing story file.
        File.WriteAllText(savePath, s.ToJson());
    }

    // Update referenced Story object based on saved StoryState (if it exists)
    static public Story Deserialize(ref Story s)
    {
        // Create internal JSON string.
        string JSONContents;

        // Does the file exist?
        if (File.Exists(savePath))
        {
            // Read the entire file.
            JSONContents = File.ReadAllText(savePath);
            // Overwrite current Story based on saved StoryState data.
            s.state.LoadJson(JSONContents);
        }

        // Return either referenced or restored story.
        return s;
    }
}