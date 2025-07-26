using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ObjectPool : ScriptableObject
{
    public Scene PoolScene; // IM GOONUING
    public string PoolSceneName;


    /// <summary>
    /// <br> Creates a scene where the pool's objects are going to be moved to. </br>
    /// </summary>
    public virtual void CreatePool()
    {
        PoolScene = SceneManager.CreateScene(PoolSceneName);

    }

    /// <summary>
    /// <br> Instantiates and moves the game objects to the pool scene. </br>
    /// <br> Abstract so that implementers can do different things.</br>
    /// </summary>
    public abstract void InstantiatePoolObjects();
}
