using MyBox;
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
    [ConditionalField(nameof(_preloadAssets))][SerializeField] private AssetLabelReference preloadAssetLabel;

    [SerializeField] private AssetReference _targetScene;
    [SerializeField] private AssetReference _loadingScene;

    private void Start()
    {
        if (_preloadAssets)
        {
            StartCoroutine(LoadScenePreload());
        }
        else
        {
            StartCoroutine(LoadScene());

        }

        //    SceneLoader.LoadScene(SceneLoadingSettings.City);
    }
    private IEnumerator LoadScenePreload()
    {
        Debug.Log("Preloading assets. ");
        var preloadHandle = Addressables.LoadAssetsAsync<ScriptableObject>(preloadAssetLabel, null);
        var loadingHandle = Addressables.LoadSceneAsync(_loadingScene, LoadSceneMode.Additive);

        yield return loadingHandle; // wait for load scene to load

        yield return preloadHandle; // after, wait for preload handle to finis

        Debug.Log("Done preloading assets");

        var handle = Addressables.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);
        yield return handle; // wait for the target scene to load

        //    SceneManager.UnloadSceneAsync(0); // unload the bootstrap scene after the target scene loads

        //    Addressables.UnloadSceneAsync(loadingHandle); // unload the loading scene after the target scene loads
    }
    private IEnumerator LoadScene()
    {
        var loadingHandle = Addressables.LoadSceneAsync(_loadingScene, LoadSceneMode.Additive);
        yield return loadingHandle; // wait for load scene to load

        var handle = Addressables.LoadSceneAsync(_targetScene, LoadSceneMode.Additive);
        yield return handle; // wait for the target scene to load

        //     SceneManager.UnloadSceneAsync(0); // unload the bootstrap scene after the target scene loads

        //   Addressables.UnloadSceneAsync(loadingHandle); // unload the loading scene after the target scene loads
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
