using UnityEngine;

public interface IDataPersistence // an interface that classes can inherit from to make their data persist
{
    void LoadData(GameData data);
        
    void SaveData(GameData data); //modifing data

}