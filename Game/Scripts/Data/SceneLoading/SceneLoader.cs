
using System;
using System.Threading.Tasks;
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


    /// <summary>
    /// <br> Is invoked when the async scene load is completed.</br>
    /// </summary>
    public static event Action<SceneLoadingSettings> OnSceneLoadComplete;

    private static readonly bool _showDebugLoadingLogs = false;

    #region
    /// <summary>
    /// <br> Load scene with interface. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion
    public static async void LoadScene(SceneLoadingSettings sceneLoadingSettings)
    {

        await LoadLoadingScene();

        await Task.Delay(1000);

        var handle = Addressables.LoadSceneAsync(sceneLoadingSettings.SceneType.ToString());

        await handle.Task;




        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (LoadedSceneHandle.IsValid()) // if there is a currently loaded scene
            {
                UnloadScene(); // unload the currently loaded scene
                if (_showDebugLoadingLogs)
                {
                    Debug.Log("Unloading scene.");

                }

            }

            else // if there is no currently loaded scene
            {
                if (_showDebugLoadingLogs)
                {
                    Debug.Log("Loading new scene.");

                }
            }
            if (_showDebugLoadingLogs)
            {
                Debug.Log($" Scene : {sceneLoadingSettings.SceneType}");
                Debug.Log($" Loaded scene interface : {sceneLoadingSettings.UserInterface}");
                Debug.Log($" Spawned at : {sceneLoadingSettings.PlayerSpawnPoint}");
                Debug.Log("LOADED SUCCESSFULLY");

            }
            LoadedSceneHandle = handle;

            OnSceneLoadComplete?.Invoke(sceneLoadingSettings);


        }
    }
    private static async Task LoadLoadingScene()
    {

        var handle = Addressables.LoadSceneAsync(SceneLoadingSettings.Loading.SceneType.ToString());
        await handle.Task;
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
