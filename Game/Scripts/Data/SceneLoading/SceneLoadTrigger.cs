

using UnityEngine;
/// <summary>
/// <br> When the loading scene is triggered, this class is activated. </br>
/// <br> It loads the scene . </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{


    [Header("Loading Settings")]


    [Header("Initilize Settings")]
    [SerializeField] private bool _showSettings = true;
    [SerializeField] private string _loadedScene;
    [SerializeField] private UserInterfaceType _loadedUserInterface;


    [Header("Debug")]

    /*
    [SerializeField] private bool _debugLoadingScreen = false;
    
    [SerializeField] private string _defaultSceneToLoad = "Main Game";
    [SerializeField] private string _debugSceneToLoad = "Main Game";
    */
    private SceneLoadingSettings _loadingSettings; // unable to serilize structs in editor

    private void Awake()
    {
        _loadingSettings = new SceneLoadingSettings(_loadedScene, _loadedUserInterface);


    }
    private void Start()
    {
        if (_showSettings)
        {
            Debug.Log(_loadingSettings.PlayerSpawnPoint);
            Debug.Log(_loadingSettings.UserInterface);
            Debug.Log(_loadingSettings.SceneName);
        }
        if (_loadingSettings.SceneName != null)
        {
            SceneLoader.LoadScene(_loadingSettings);

        }
        else
        {
            Debug.LogError("The loading settings' scene name is null. ");
            return;
        }
        //  SceneLoadingManager.LoadScene();

        /*
        if (_initilize)
        {
            //    SceneLoadingManager.LoadScene();

        }
        */
        /*
        if (_debugLoadingScreen)
        {
            //   _sceneLoader.LoadScene(_debugSceneToLoad);
            Debug.LogWarning("Loading debug scene");

        }
        else
        {
            //    _sceneLoader.LoadScene(SceneLoadingManager.SceneToLoad, SceneLoadingManager.UserInterfaceToLoad);

        }
        */
    }

}
