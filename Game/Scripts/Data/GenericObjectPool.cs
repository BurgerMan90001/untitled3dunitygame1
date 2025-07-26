using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// <br> A generic gameobject pool that just moves all of the gameobjects to the scene pool.</br>
/// </summary>
[CreateAssetMenu(menuName = "ObjectPool/GenericObjectPool")]
public class GenericObjectPool : ObjectPool
{
    public List<GameObject> PoolObjects;
    public override void InstantiatePoolObjects()
    {
        foreach (var poolObject in PoolObjects)
        {
            SceneManager.MoveGameObjectToScene(Instantiate(poolObject), PoolScene);
        }

    }
}
