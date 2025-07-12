

using UnityEngine;
/// <summary>
/// <br> When the loading scene is triggered, this class is activated. </br>
/// <br> It loads the scene . </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{
    
    [Header("Dependancies")]
    

    [Header("Loading Settings")]
    [SerializeField] private string _defaultSceneToLoad = "Main Game";

    [Header("Debug")]
    [SerializeField] private bool _debugLoadingScreen = false;
    [SerializeField] private string _debugSceneToLoad = "Main Game";

    private SceneLoader _sceneLoader;


    
    private void Awake()
    {
        _sceneLoader = new SceneLoader();
        
    }
    private void Start()
    {
        
        if (_debugLoadingScreen)
        {
            _sceneLoader.LoadScene(_debugSceneToLoad);
            Debug.LogWarning("Loading debug scene");

        } else if (SceneLoadingManager.SceneToLoad == null)
        {
            _sceneLoader.LoadScene(_defaultSceneToLoad);
            Debug.LogWarning("Loading default scene");
        } else
        {
            _sceneLoader.LoadScene(SceneLoadingManager.SceneToLoad, SceneLoadingManager.UserInterfaceToLoad);

        }
            
    }
    private void OnDestroy()
    {
        _sceneLoader.UnloadScene();
    }
}
