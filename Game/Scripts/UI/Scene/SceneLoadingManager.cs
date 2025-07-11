
// TODO PLAY SUM FADE ANIMATION
using System;


public static class SceneLoadingManager
{
    public static Action<UserInterfaces> OnSceneLoaded;

    public static string SceneToLoad;
    public static UserInterfaces UserInterfaceToLoad;

    private static SceneLoader _sceneLoader = new SceneLoader();

    public static string LoadingSceneName = "Loading";
    public static void LoadLoadingScreen()
    {
        OnSceneLoaded.Invoke(UserInterfaces.Loading);
        _sceneLoader.LoadScene(LoadingSceneName, UserInterfaces.Loading);
       
        
    }
    
}


