
using System.Collections.Generic;
using UnityEngine;

// data that will persist
[CreateAssetMenu(menuName = "Stats/GeneralStats")]
public class GeneralStats : Stats
{
 //   public List<ItemInstance> Items;

    public float Stamina;
    public float MaxStamina;

    public float Money;

    public float Energy;
    public float MaxEnergy;


    public float Experience;
    public float Level;

    
    public override void SaveData(GameData data)
    {
    //    data.
        
        data.MaxStamina = MaxStamina;

        data.MaxEnergy = MaxEnergy;
        data.Energy = Energy;


        data.Money = Money;
        data.Experience = Experience;
        data.Level = Level;

        Debug.Log("SAVED STATS");

    }
    public override void LoadData(GameData data)
    {
        MaxStamina = data.MaxStamina;

        MaxEnergy = data.MaxEnergy;
        Energy = data.Energy;


        Money = data.Money;


        Experience = data.Experience;
        Level = data.Level;

        
    }
}

