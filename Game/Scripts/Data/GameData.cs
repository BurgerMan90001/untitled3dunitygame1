
using Ink.Runtime;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData // game data is the data that will be serialized and saved into a json file.
{


    public long LastUpdated;

    public Vector3 PlayerPosition;


    public float MaxStamina;

    public float MaxHealth;

    public float Energy;
    public float MaxEnergy;

    public float Money;

    public float Experience;
    public float Level;


    public float BaseSpeed;


    public Inventory Inventory;

    public Story Story; // INK STORY





    public GameData() // new game stats
    {

        PlayerPosition = new Vector3(-21.9972f, 54.65f, -37.326f); // area where player will spawn at in newgame

        MaxStamina = 20f;


        Energy = 10f; // default values
        MaxEnergy = 10f;

        Money = 0f;

        MaxHealth = 20f;

        Experience = 0f;
        Level = 0f;

        BaseSpeed = 7.5f;


        // Story = StoryStateSerialization.Deserialize(ref Story);

    }
}

public static class StoryStateSerialization
{
    // Set a path to save and restore StoryState.
    private static readonly string _fileName = "StoryState.json";
    private static readonly string _savePath = Application.persistentDataPath + "/" + _fileName;

    /// <summary>
    /// Convert a StoryState into a JSON string and save file.
    /// </summary>
    public static void Serialize(StoryState storyState)
    {
        // Either create or overwrite an existing story file.
        File.WriteAllText(_savePath, storyState.ToJson());
    }

    /// <summary>
    /// Update referenced Story object based on saved StoryState (if it exists)
    /// </summary>
    /// <param name="story"></param>
    /// <returns></returns>
    public static Story Deserialize(ref Story story)
    {

        string JSONContents; // create internal JSON string.


        if (File.Exists(_savePath))
        {

            JSONContents = File.ReadAllText(_savePath); // reads the entire json file

            story.state.LoadJson(JSONContents); // overwrite current Story based on saved StoryState data.
        }


        return story; // return either referenced or restored story.
    }
}