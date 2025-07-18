
// TODO PLAY SUM FADE ANIMATION, IMPROVE CODE
using System;

using UnityEngine;

#region
/// <summary>
/// <br> Manages scene loading and calls SceneLoader to load and unload scenes. </br>
/// </summary>
#endregion
public static class SceneLoadingManager
{
    private const string LoadingSceneName = "Loading";
    public static event Action<UserInterfaceType, bool> OnSceneLoaded;

    public static string SceneToLoad { get; private set; }
    public static Vector3 SpawnPoint { get; private set; }
    public static UserInterfaceType UserInterfaceToLoad { get; private set; }

    private static SceneLoader _sceneLoader = new SceneLoader();

    

    private static void LoadLoadingScene()
    {
        _sceneLoader.LoadScene(LoadingSceneName);
    } 
    #region
    /// <summary>
    /// <br> Can load a scene with a loading scene in between or without. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterfaceToBeLoaded"></param>
    /// <param name="loadLoadingScene"></param>
    #endregion
    public static void LoadScene(string sceneName, UserInterfaceType userInterfaceToBeLoaded, bool loadLoadingScene)
    {
        SceneToLoad = sceneName;
        UserInterfaceToLoad = userInterfaceToBeLoaded;

        if (loadLoadingScene)
        {
            LoadLoadingScene();

        } else // loads directly without a loading scene.
        {
            _sceneLoader.LoadScene(SceneToLoad, UserInterfaceToLoad);
        }
    }
    public static void SetSpawnPoint(Vector3 spawnPoint)
    {
        SpawnPoint = spawnPoint;

        
    }
    public static void SceneLoaded(UserInterfaceType userInterfaceToBeLoaded)
    {
        OnSceneLoaded?.Invoke(userInterfaceToBeLoaded, true);
    }
    #region
    /// <summary>
    /// <br> Automatically loads with a loading scene. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterfaceToBeLoaded"></param>
#endregion
    public static void LoadScene(string sceneName, UserInterfaceType userInterfaceToBeLoaded)
    {
        SceneToLoad = sceneName;
        UserInterfaceToLoad = userInterfaceToBeLoaded;

        LoadLoadingScene();
    }

    
}


