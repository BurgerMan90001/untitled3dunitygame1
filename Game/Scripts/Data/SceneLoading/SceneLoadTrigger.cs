

using UnityEngine;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected bool _debugMode = true;

    protected void LoadScene(SceneLoadingSettings sceneLoadingSettings)
    {

        if (_debugMode)
        {
            Debug.Log(sceneLoadingSettings.PlayerSpawnPoint);
            Debug.Log(sceneLoadingSettings.UserInterface);
            Debug.Log(sceneLoadingSettings.SceneName);
        }
        if (sceneLoadingSettings.SceneName != null)
        {
            SceneLoader.LoadScene(sceneLoadingSettings);

        }
        else
        {
            Debug.LogError("The loading settings' scene name is null. ");
            return;
        }
    }

}
