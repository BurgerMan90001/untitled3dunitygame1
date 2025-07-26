// TODO ADD UNIVERSAL DATA PROPERTIES !!!


using UnityEngine;

/// <summary>
/// <br> For data that is created during runtime. </br>
/// </summary>
public abstract class Data : MonoBehaviour, IDataPersistence
{

    public abstract void LoadData(GameData data);

    public abstract void SaveData(GameData data);
}

