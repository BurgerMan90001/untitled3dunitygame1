
using UnityEngine;

[System.Serializable]
public class GameData // game data is the data that will be serialized and saved into a json file.
{
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

        
    }
}


