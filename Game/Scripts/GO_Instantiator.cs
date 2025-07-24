using MyBox;
using UnityEngine;



public class GO_Instantiator : MonoBehaviour
{
    [SerializeField] private bool _instantiate;
    [SerializeField] private SceneLoadTrigger _sceneTrigger;

    [Header("Pool")]
    [DisplayInspector]
    [ConditionalField(nameof(_instantiate))]
    [SerializeField]
    private ObjectPool _pool;


    private void Start()
    {
        if (_instantiate)
        {
            _pool.CreatePool();
            _pool.InstantiatePoolObjects();
            _sceneTrigger.Trigger(); // trigger scene load after it's done loading the pool objects

        }
        else
        {
            Debug.LogWarning("Instantiate for the GO_Instantiator is set to false.");

        }



    }

}
