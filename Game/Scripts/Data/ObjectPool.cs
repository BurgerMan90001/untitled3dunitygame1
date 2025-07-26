using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "ObjectPool/ObjectPool")]
public class ObjectPool : ScriptableObject
{
    public Scene PoolScene; // IM GOONUING
    public string PoolName;

    public List<GameObject> PoolObjects;

    public void CreatePool()
    {
        PoolScene = SceneManager.CreateScene(PoolName);

    }
    /// <summary>
    /// <br> Move game objects to the pool scene. </br>
    /// </summary>
    public virtual void InstantiatePoolObjects()
    {

        foreach (var gameObject in PoolObjects)
        {

            SceneManager.MoveGameObjectToScene(Instantiate(gameObject), PoolScene);

        }
        //    PoolObjectsInstantiated = true;

    }
}
