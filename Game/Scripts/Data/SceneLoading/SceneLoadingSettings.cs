using UnityEngine;
/// <summary>
/// <br> Settings and data that can be set for scene loading. </br>
/// </summary>

public struct SceneLoadingSettings
{
    private static readonly Vector3 _mainGameSpawnPoint = new Vector3(-21.9972f, 54.65f, -37.326f);
    private static readonly Vector3 _combatSpawnPoint = new Vector3(-3f, 4f, 0f);


    public string SceneName;

    public UserInterfaceType UserInterface;

    public Vector3 PlayerSpawnPoint;



    #region
    /// <summary>
    /// <br> Loads the loading screen and scene. Spawns the player at (0,0,0).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings LoadingScene = new SceneLoadingSettings("Loading", UserInterfaceType.Loading, Vector3.zero);

    #region
    /// <summary>
    /// <br> Loads the debug scene without an interface. Spawns the player at (0,0,0).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings Debug = new SceneLoadingSettings("Debug", UserInterfaceType.None, Vector3.zero);

    #region
    /// <summary>
    /// <br> Loads the main game scene with the HUD interface. Spawns the player at (-21.9972f, 54.65f, -37.326f).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings MainGame = new SceneLoadingSettings("Main Game", UserInterfaceType.HUD, _mainGameSpawnPoint);
    #region
    /// <summary>
    /// <br> Loads the combat scene with the combat interface. Spawns the player at (-3f, 4f, 0f).</br>
    /// </summary>
    #endregion
    public static readonly SceneLoadingSettings Combat = new SceneLoadingSettings("Combat", UserInterfaceType.Combat, _combatSpawnPoint);
    #region
    /// <summary>
    /// <br> Creates a new scene loading setting. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion

    public SceneLoadingSettings(string sceneName, UserInterfaceType userInterface, Vector3 playerSpawnPoint)
    {
        SceneName = sceneName;
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
    public SceneLoadingSettings(string sceneName, UserInterfaceType userInterface)
    {
        SceneName = sceneName;
        UserInterface = userInterface;
        PlayerSpawnPoint = Vector3.zero;
    }
}