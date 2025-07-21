using UnityEngine;

public class SceneLoadStartTrigger : SceneLoadTrigger
{
    [Header("Scene Load Start Settings")]

    [SerializeField] protected string _scene;
    [SerializeField] protected UserInterfaceType _userInterface;
    [SerializeField] protected Vector3 _position;
    private void Start()
    {
        LoadScene(new SceneLoadingSettings(_scene, _userInterface, _position));
    }
}
