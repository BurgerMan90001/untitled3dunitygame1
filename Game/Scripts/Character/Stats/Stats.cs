
using UnityEngine;

// data that will persist
[CreateAssetMenu(menuName = "Character/Stats")]
public class SO_Stats : ScriptableObject, IDataPersistence
{
  
    public float Stamina;
    public float MaxStamina;

    public float Money;

    public float Energy;
    public float MaxEnergy;

    public float MaxHealth;
    public float Health; // used in battle

    public float Experience;
    public float Level;


    public void SaveData(GameData data)
    {
        
        
        data.MaxStamina = MaxStamina;

        data.MaxEnergy = MaxEnergy;
        data.Energy = Energy;

        data.MaxHealth = MaxHealth;

        data.Money = Money;
        data.Experience = Experience;
        data.Level = Level;

        Debug.Log("SAVED STATS");

    }
    public void LoadData(GameData data)
    {
        MaxStamina = data.MaxStamina;

        MaxEnergy = data.MaxEnergy;
        Energy = data.Energy;

        MaxHealth = data.MaxHealth;

        Money = data.Money;


        Experience = data.Experience;
        Level = data.Level;

    }


}

