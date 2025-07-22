
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;



// TODO PLAY SUM FADE ANIMATION
#region
/// <summary>
/// <br> Loads and unloads scenes. Uses addressables to load and unload scenes. </br>
/// </summary>
#endregion
public static class SceneLoader
{
    public static AsyncOperationHandle<SceneInstance> LoadedSceneHandle { get; private set; }

    public static event Action<SceneLoadingSettings> OnSceneLoaded;

    private static readonly bool _showDebugLoadingLogs = false;

    //    private static 
    #region
    /// <summary>
    /// <br> Load scene with interface. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion
    public static async void LoadScene(SceneLoadingSettings sceneLoadingSettings)
    {
        var handle = Addressables.LoadSceneAsync(sceneLoadingSettings.SceneName);

        await handle.Task;


        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (LoadedSceneHandle.IsValid()) // if there is a currently loaded scene
            {
                UnloadScene(); // unload the currently loaded scene
                Debug.Log("Unloading scene.");
            }

            else // if there is no currently loaded scene
            {
                Debug.Log("Loading new scene.");

            }
            if (_showDebugLoadingLogs)
            {
                Debug.Log($" Scene : {sceneLoadingSettings.SceneName}\n");
                Debug.Log($" Loaded scene interface : {sceneLoadingSettings.UserInterface}");
                Debug.Log($" Spawned at : {sceneLoadingSettings.PlayerSpawnPoint}");
                Debug.Log("LOADED SUCCESSFULLY");

            }
            LoadedSceneHandle = handle;
            OnSceneLoaded?.Invoke(sceneLoadingSettings);



        }
    }
    #region
    /// <summary>
    /// <br> Unloads the currently loaded scene with addressables. </br>
    /// </summary>
    #endregion
    public static void UnloadScene()
    {
        if (!LoadedSceneHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(LoadedSceneHandle);
            if (_showDebugLoadingLogs)
            {
                Debug.Log("UNLOADING");
            }

        }
        else
        {
            Debug.LogWarning("There currently is no scene loaded. ");
        }
    }
}
