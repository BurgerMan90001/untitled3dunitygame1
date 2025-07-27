using UnityEngine;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{
    private void Start()
    {
        Trigger(SceneLoadingSettings.City);
    }
    private void OnEnable()
    {
        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;
    }

    private void OnDestroy()
    {
        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;
    }
    /// <summary>
    /// <br> Triggers a scene load. </br> 
    /// </summary>
    public void Trigger(SceneLoadingSettings sceneLoadingSettings)
    {
        SceneLoader.LoadScene(sceneLoadingSettings);

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
