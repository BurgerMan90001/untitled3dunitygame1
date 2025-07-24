
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;



// TODO PLAY SUM FADE ANIMATION
#region
/// <summary>
/// <br> Loads and unloads scenes. Uses addressables to load and unload scenes. </br>
/// </summary>
#endregion
public static class SceneLoader
{
    public static Stack<AsyncOperationHandle<SceneInstance>> LoadedSceneHandles { get; private set; } = new Stack<AsyncOperationHandle<SceneInstance>>();

    /// <summary>
    /// <br> Is invoked when the async scene load is completed.</br>
    /// </summary>
    public static event Action<SceneLoadingSettings> OnSceneLoadComplete;

    private static readonly bool _debugMode = false;

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

        //   await Task.Delay(1000);

        var handle = Addressables.LoadSceneAsync(sceneLoadingSettings.Key, LoadSceneMode.Additive);



        await handle.Task;




        if (handle.Status == AsyncOperationStatus.Succeeded)
        {

            if (_debugMode)
            {
                Debug.Log($" Scene : {sceneLoadingSettings.SceneType}");
                Debug.Log($" Loaded scene interface : {sceneLoadingSettings.UserInterface}");
                Debug.Log($" Spawned at : {sceneLoadingSettings.PlayerSpawnPoint}");
                Debug.Log("LOADED SUCCESSFULLY");

            }
            UnloadRecentScene();

            LoadedSceneHandles.Push(handle);

            OnSceneLoadComplete?.Invoke(sceneLoadingSettings);


        }
    }
    private static async Task LoadLoadingScene()
    {

        var handle = Addressables.LoadSceneAsync(SceneLoadingSettings.Loading.Key, LoadSceneMode.Additive);
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            UnloadRecentScene();

            LoadedSceneHandles.Push(handle); // adds to loaded scenes stack when done loading
        }


    }
    #region
    /// <summary>
    /// <br> Unloads the recently loaded scene. </br>
    /// </summary>
    #endregion
    public static void UnloadRecentScene()
    {

        if (LoadedSceneHandles.TryPop(out AsyncOperationHandle<SceneInstance> sceneHandle))
        {
            if (sceneHandle.IsValid())
            {
                Addressables.UnloadSceneAsync(sceneHandle);
                if (_debugMode)
                {
                    Debug.Log("UNLOADING");
                }

            }
            else
            {
                Debug.LogWarning("The scene handle is not loaded. ");
            }
        }

    }
}
