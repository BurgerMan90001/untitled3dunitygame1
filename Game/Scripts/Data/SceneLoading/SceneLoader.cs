
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

    private static readonly bool _showDebugLoadingLogs = true;


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

            if (!LoadedSceneHandle.IsValid())
            {
                LoadedSceneHandle = handle;

                OnSceneLoaded?.Invoke(sceneLoadingSettings);
                //    SceneLoadingManager.SceneLoaded(userInterfaceToBeLoaded);
                if (_showDebugLoadingLogs)
                {
                    Debug.Log(sceneLoadingSettings.UserInterface.ToString());
                    Debug.Log("LOADED SUCCESSFULLY");
                }

            }
            else
            {
                UnloadScene();
                Debug.LogError("UNLOADING BECAUSE THE HANDLE IS INVALID.");
            }


        }
    }
    #region
    /// <summary>
    /// <br> Unloads the currently loaded scene with addressables. </br>
    /// </summary>
    #endregion
    public static void UnloadScene()
    {
        if (LoadedSceneHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(LoadedSceneHandle);
            if (_showDebugLoadingLogs)
            {
                Debug.Log("UNLOADING");
            }

        }
    }
}
