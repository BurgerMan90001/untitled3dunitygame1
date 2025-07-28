using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
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


    public virtual void LoadGameObjectAsync(AssetReferenceGameObject gameObjectReference)
    {
        var instanceHandle = gameObjectReference.InstantiateAsync();
    }

    /// <summary>
    /// <br> Instantiates and moves the game objects to the pool scene. </br>
    /// <br> Abstract so that implementers can do different things.</br>
    /// </summary>
    public abstract void InstantiatePoolObjects();

    protected async Task<GameObject> LoadObject(AssetReferenceGameObject GOreference)
    {
        if (GOreference != null)
        {

            var instance = GOreference.LoadAssetAsync();

            await instance.Task;

            instance.Result.SetActive(false);

            if (instance.Status == AsyncOperationStatus.Succeeded)
            {
                return instance.Result;

            }
            else
            {
                Debug.LogError($"Failed to load {GOreference} pool object");

            }

        }
        else
        {
            Debug.LogError($"The {GOreference} is null.");

        }
        return null;

    }
    protected async Task<GameObject> InstantiateObject(AssetReferenceGameObject GOreference)
    {
        if (GOreference != null)
        {

            var instance = GOreference.InstantiateAsync();


            await instance.Task;

            instance.Result.SetActive(false);

            if (instance.Status == AsyncOperationStatus.Succeeded)
            {
                return instance.Result;
                //    SceneManager.MoveGameObjectToScene(instance.Result, PoolScene);
            }
            else
            {
                Debug.LogError($"Failed to load {GOreference} pool object");

            }

        }
        else
        {
            Debug.LogError($"The {GOreference} is null.");

        }
        return null;
    }
}
