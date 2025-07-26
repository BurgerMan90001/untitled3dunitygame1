using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(menuName = "ObjectPool/PlayerObjectPool")]
public class PlayerObjectPool : ObjectPool
{

    public override void InstantiatePoolObjects()
    {

        foreach (var gameObject in PoolObjects)
        {

            var playerObject = Instantiate(gameObject);

            SceneManager.MoveGameObjectToScene(playerObject, PoolScene);
        }

        /*
        foreach (var gameObject in PoolObjects)
        {
            if (gameObject.TryGetComponent(out IInjectable component))
            {

            }
            SceneManager.MoveGameObjectToScene(Instantiate(gameObject), PoolScene);

        }
        //    PoolObjectsInstantiated = true;
        */

    }
    private void Test(GameObject playerObject)
    {

    }

}
