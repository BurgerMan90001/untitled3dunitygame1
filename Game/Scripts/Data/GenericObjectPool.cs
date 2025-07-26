using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
/// <summary>
/// <br> A generic gameobject pool that just moves all of the gameobjects to the scene pool.</br>
/// </summary>
[CreateAssetMenu(menuName = "ObjectPool/GenericObjectPool")]
public class GenericObjectPool : ObjectPool
{
    public List<GameObject> PoolObjects;
    public List<AssetReferenceGameObject> PoolObjectKeys;
    public async override void InstantiatePoolObjects()
    {
        foreach (var poolObject in PoolObjectKeys)
        {
            if (poolObject != null)
            {
                var instance = poolObject.InstantiateAsync();

                await instance.Task;

                if (instance.Status == AsyncOperationStatus.Succeeded)
                {
                    SceneManager.MoveGameObjectToScene(instance.Result, PoolScene);
                }
                else
                {
                    Debug.LogError("Failed to load a pool object");
                }

            }
            else
            {
                Debug.LogError("The pool object reference is null.");
            }
        }


    }
}
