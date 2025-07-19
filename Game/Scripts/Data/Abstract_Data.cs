using UnityEngine;
// TODO ADD UNIVERSAL DATA PROPERTIES !!!
public abstract class Data : ScriptableObject, IDataPersistence
{
    public void LoadData(GameData data)
    {
        throw new System.NotImplementedException();
    }

    public void SaveData(GameData data)
    {
        throw new System.NotImplementedException();
    }
}

public interface IData
{ 

}
