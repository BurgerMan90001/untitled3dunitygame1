using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{

    private void OnEnable()
    {
        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;
    }
    /// <summary>
    /// Loads a scene.
    /// </summary>
    public void Trigger(SceneLoadingSettings sceneLoadingSettings)
    {
        SceneLoader.LoadScene(sceneLoadingSettings);

    }
    private void OnSceneLoadComplete(SceneLoadingSettings sceneLoadingSettings)
    {
        if (sceneLoadingSettings.SceneType is SceneType.Game)
        {
            SceneManager.UnloadSceneAsync(0); // unloads itself when in game and not in menu.
        }
    }



}
