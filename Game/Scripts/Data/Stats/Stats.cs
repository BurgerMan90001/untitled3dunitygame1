using UnityEngine;

public abstract class Stats : ScriptableObject, IDataPersistence
{
    public abstract void LoadData(GameData data);
    public abstract void SaveData(GameData data);
}
