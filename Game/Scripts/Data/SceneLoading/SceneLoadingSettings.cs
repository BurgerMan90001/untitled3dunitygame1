using UnityEngine;


public enum SceneType
{
    Menu,
    Game,
    Debug,
}

// TODO ORGINIZE ADDRESSABLES IN FOLDERS

#region
/// <summary>
/// <br> Settings and data that can be set for scene loading. </br>
/// </summary>
#endregion
public struct SceneLoadingSettings
{
    private static readonly Vector3 _citySpawnPoint = new Vector3(-21.9972f, 54.65f, -37.326f);
    private static readonly Vector3 _combatSpawnPoint = new Vector3(-3f, 4f, 0f);

    //  private static readonly string Path = "Scenes/"+name+".unity";

    public SceneType SceneType;

    public UserInterfaceType UserInterface;

    public Vector3 PlayerSpawnPoint;

    public string Key;

    #region
    /// <summary>
    /// <br> Loads the loading screen and scene. Spawns the player at (0,0,0).</br>
    /// </summary>
    #endregion

    public static readonly SceneLoadingSettings Loading = new SceneLoadingSettings("Loading", SceneType.Menu, UserInterfaceType.Loading, Vector3.zero);
    #region
    /// <summary>
    /// <br> Loads the main menu interface and scene. Spawns the player at (0,0,0).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings MainMenu = new SceneLoadingSettings("MainMenu", SceneType.Menu, UserInterfaceType.MainMenu, Vector3.zero);

    #region
    /// <summary>
    /// <br> Loads the debug scene without an interface. Spawns the player at (0,0,0).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings Debug = new SceneLoadingSettings("Debug", SceneType.Debug, UserInterfaceType.None, Vector3.zero);

    #region
    /// <summary>
    /// <br> Loads the main game scene with the HUD interface. Spawns the player at (-21.9972f, 54.65f, -37.326f).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings City = new SceneLoadingSettings("City", SceneType.Game, UserInterfaceType.HUD, _citySpawnPoint);
    #region
    /// <summary>
    /// <br> Loads the combat scene with the combat interface. Spawns the player at (-3f, 4f, 0f).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings Combat = new SceneLoadingSettings("Combat", SceneType.Game, UserInterfaceType.Combat, _combatSpawnPoint);
    #region
    /// <summary>
    /// <br> Creates a new scene loading setting. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion

    public SceneLoadingSettings(string key, SceneType sceneType, UserInterfaceType userInterface, Vector3 playerSpawnPoint)
    {
        Key = key;
        SceneType = sceneType;
        UserInterface = userInterface;
        PlayerSpawnPoint = playerSpawnPoint;
    }

    #region
    /// <summary>
    /// <br> Creates a new scene loading setting. Loads the player at (0,0,0).</br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion
    public SceneLoadingSettings(string key, SceneType sceneType, UserInterfaceType userInterface)
    {
        Key = key;
        SceneType = sceneType;
        UserInterface = userInterface;
        PlayerSpawnPoint = Vector3.zero;
    }
}