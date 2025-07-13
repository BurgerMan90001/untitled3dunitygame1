
// TODO PLAY SUM FADE ANIMATION
using System;


public static class SceneLoadingManager
{
    public static Action<UserInterfaces> OnSceneLoaded;

    public static string SceneToLoad { get; private set; }
    public static UserInterfaces UserInterfaceToLoad {  get; private set; }

    private static SceneLoader _sceneLoader = new SceneLoader();

    private static string LoadingSceneName = "Loading";
    private static void LoadLoadingScene()
    {
        _sceneLoader.LoadScene(LoadingSceneName);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterfaceToBeLoaded"></param>
    /// <param name="loadLoadingScene"></param>
    public static void LoadScene(string sceneName, UserInterfaces userInterfaceToBeLoaded, bool loadLoadingScene)
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
    /// <summary>
    /// <br> Loads with a loading scene. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterfaceToBeLoaded"></param>
    public static void LoadScene(string sceneName, UserInterfaces userInterfaceToBeLoaded)
    {
        SceneToLoad = sceneName;
        UserInterfaceToLoad = userInterfaceToBeLoaded;

        LoadLoadingScene();
    }
}


