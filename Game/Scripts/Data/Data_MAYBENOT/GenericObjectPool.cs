using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
/// <summary>
/// <br> A generic gameobject pool that just moves all of the gameobjects to the scene pool.</br>
/// </summary>
[CreateAssetMenu(menuName = "ObjectPool/GenericObjectPool")]
public class GenericObjectPool : ObjectPool
{
    //    public List<GameObject> PoolObjects;
    public List<AssetReferenceGameObject> PoolObjectKeys;
    /*
    public async override void InstantiatePoolObjects()
    {
        foreach (var poolObject in PoolObjectKeys)
        {

            var poolObjectInstance = await InstantiateObject(poolObject);

            SceneManager.MoveGameObjectToScene(poolObjectInstance, PoolScene);



        }


    }
    */
}
