using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "ObjectPool")]
public class ObjectPool : ScriptableObject
{
    public Scene PoolScene; // IM GOONUING
    public string PoolName;

    public List<GameObject> PoolObjects;

    public void CreatePool()
    {

        PoolScene = SceneManager.CreateScene(PoolName);
    }

    public void InstantiatePoolObjects()
    {

        foreach (var gameObject in PoolObjects)
        {
            var instantiatedManager = Instantiate(gameObject);
            SceneManager.MoveGameObjectToScene(instantiatedManager, PoolScene);

        }
        //    PoolObjectsInstantiated = true;

    }
}
