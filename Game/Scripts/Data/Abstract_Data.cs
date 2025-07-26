// TODO ADD UNIVERSAL DATA PROPERTIES !!!


/// <summary>
/// <br> For data that is created during runtime. </br>
/// </summary>
public abstract class Data : IDataPersistence
{

    public abstract void LoadData(GameData data);

    public abstract void SaveData(GameData data);
}

