
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;



// TODO PLAY SUM FADE ANIMATION
#region
/// <summary>
/// <br> Handles the scene loading logic. Uses addressables to load and unload scenes. </br>
/// </summary>
#endregion
public class SceneLoader
{
    private AsyncOperationHandle<SceneInstance> _loadedSceneHandle;

    private readonly bool _showDebugLoadingLogs = false;
    
    #region
    /// <summary>
    /// <br> Loads a single scene. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    #endregion
    public async void LoadScene(string sceneName)
    {
        var handle = Addressables.LoadSceneAsync(sceneName);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (!_loadedSceneHandle.IsValid())
            {
                _loadedSceneHandle = handle;
                
                Debug.Log("LOADED SUCCESSFULLY");
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
    /// <br> Load scene with interface. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    #endregion
    public async void LoadScene(string sceneName, UserInterfaceType userInterfaceToBeLoaded)
    {
        var handle = Addressables.LoadSceneAsync(sceneName);

        await handle.Task;

        
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (!_loadedSceneHandle.IsValid())
            {
                _loadedSceneHandle = handle;

                SceneLoadingManager.SceneLoaded(userInterfaceToBeLoaded);
                if (_showDebugLoadingLogs)
                {
                    Debug.Log(userInterfaceToBeLoaded.ToString());
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
    public void UnloadScene()
    {
        if (_loadedSceneHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(_loadedSceneHandle);
            if (_showDebugLoadingLogs)
            {
                Debug.Log("UNLOADING");
            }
            
        }
    }
}
