using MyBox;
using UnityEngine;



public class Initialize : MonoBehaviour
{
    [SerializeField] private bool _active;

    [SerializeField] private bool _triggerSceneLoad;
    [ConditionalField(nameof(_triggerSceneLoad))][SerializeField] private SceneLoadTrigger _sceneTrigger;

    [Header("Initilize Pool")]
    [DisplayInspector]
    [ConditionalField(nameof(_active))]
    [SerializeField]
    private ObjectPool _pool;


    private void Start()
    {
        if (_active)
        {
            _pool.CreatePool();
            _pool.InstantiatePoolObjects();
            if (_triggerSceneLoad)
            {
                _sceneTrigger.Trigger(SceneLoadingSettings.MainMenu); // trigger scene load after it's done loading the pool objects
            }

        }
        else
        {
            Debug.LogWarning("Instantiate for the GO_Instantiator is set to false.");

        }



    }

}
