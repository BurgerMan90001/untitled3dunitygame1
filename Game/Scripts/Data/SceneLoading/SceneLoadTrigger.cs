using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField] private bool _preloadAssets = true;
    [SerializeField] private AssetReference scene;
    [SerializeField] private AssetLabelReference assetLabel;
    private IEnumerator Start()
    {

        if (_preloadAssets)
        {
            Debug.Log("Preloading assets. ");
            var preloadHandle = Addressables.LoadAssetsAsync<ScriptableObject>(assetLabel, null);
            yield return preloadHandle; // wait for preload handle to finish.
        }


        yield return null;

        var handle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Additive);
        yield return handle;
        //    SceneLoader.LoadScene(SceneLoadingSettings.City);
    }
    private void OnEnable()
    {
        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;
    }

    private void OnSceneLoadComplete(SceneLoadingSettings sceneLoadingSettings)
    {
        //   SceneManager.UnloadSceneAsync("Initilization");
        /*
        if (sceneLoadingSettings.SceneType is SceneType.Game)
        {
            SceneManager.UnloadSceneAsync("Initilization");
            // unloads itself when in game and not in menu.
        }
        */

    }

}
