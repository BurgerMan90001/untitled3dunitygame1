
using UnityEngine;


[CreateAssetMenu(menuName = "Character/Stats")]
public class TimeOfDay : ScriptableObject, IDataPersistence
{
    public float Time;
    public int Month;



    public void SaveData(GameData data)
    {


        

        Debug.Log("SAVED TIME");

    }
    public void LoadData(GameData data)
    {
        /*
        MaxStamina = data.MaxStamina;
        MaxEnergy = data.MaxEnergy;
        CurrentEnergy = data.CurrentEnergy;
        Money = data.Money;
        Experience = data.Experience;
        Level = data.Level;
        */
        Debug.Log("LOADED TIME");
    }
}
