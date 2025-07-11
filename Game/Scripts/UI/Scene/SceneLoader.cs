
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;



// TODO PLAY SUM FADE ANIMATION
#region
/// <summary>
/// <br> Handles the scene loading logic. </br>
/// </summary>
#endregion
public class SceneLoader
{
    private AsyncOperationHandle<SceneInstance> _loadedSceneHandle;
    
    
    
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
                Debug.Log("UNLOADING");
            }

                
        }
    }
    /// <summary>
    /// <br> Load scene with interface. </br>
    /// </summary>
    /// <param name="sceneName"></param>
    /// <param name="userInterface"></param>
    public async void LoadScene(string sceneName, UserInterfaces userInterface)
    {
        var handle = Addressables.LoadSceneAsync(sceneName);

        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            if (!_loadedSceneHandle.IsValid())
            {
                _loadedSceneHandle = handle;
                SceneLoadingManager.OnSceneLoaded.Invoke(userInterface);
                Debug.Log("LOADED SUCCESSFULLY");
            }
            else
            {
                UnloadScene();
                Debug.Log("UNLOADING");
            }


        }
    }
    /// <summary>
    /// <br> Unloads the currently loaded scene. </br>
    /// </summary>
    public void UnloadScene()
    {
        if (_loadedSceneHandle.IsValid())
        {
            Addressables.UnloadSceneAsync(_loadedSceneHandle);

        }
    }
}
