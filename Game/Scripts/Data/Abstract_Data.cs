using UnityEngine;
// TODO ADD UNIVERSAL DATA PROPERTIES !!!
public abstract class Data : ScriptableObject, IDataPersistence
{
    public abstract void LoadData(GameData data);

    public abstract void SaveData(GameData data);
}

public interface IData
{ 

}
