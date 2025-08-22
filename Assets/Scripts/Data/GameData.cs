
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData // game data is the data that will be serialized and saved into a json file.
{
    //   public GeneralStats GeneralStats; MAYBE

    //  public CombatStats CombatStats;
    public long LastUpdated;
    public Vector3 PlayerPosition;

    public float MaxHealth;

    // DIALOGUE DATA
    public Story Story; // INK STORY


    // GENERAL STATS
    public Inventory inventory;
    public List<ItemInstance> Items;

    public float Stamina;
    public float MaxStamina;

    public float Money;

    public float Energy;
    public float MaxEnergy;

    public float Experience;
    public float Level;

    public float BaseSpeed;

    public GameData() // new game stats
    {
        PlayerPosition = new Vector3(-21.9972f, 54.65f, -37.326f); // area where player will spawn at in newgame

        //GENERAL STATS
        MaxStamina = 20f;

        Money = 0f;

        Energy = 10f; // default values
        MaxEnergy = 10f;

        Experience = 0f;
        Level = 0f;

        BaseSpeed = 7.5f;


        //    MaxHealth = 20f; COMBAT MAYBE




        // Story = StoryStateSerialization.Deserialize(ref Story);

    }

    /*
    public void LoadData(GameData data)
    {
        // GENERAL STATS
        data.MaxStamina = MaxStamina;

        data.MaxEnergy = MaxEnergy;
        data.Energy = Energy;

        data.Money = Money;
        data.Experience = Experience;
        data.Level = Level;

        Debug.Log("LOADED STATS");
    }

    public void SaveData(GameData data)
    {
        // GENERAL STATS
        MaxStamina = data.MaxStamina;

        MaxEnergy = data.MaxEnergy;
        Energy = data.Energy;


        Money = data.Money;


        Experience = data.Experience;
        Level = data.Level;

        Debug.Log("SAVED STATS");
    }
    */
}
