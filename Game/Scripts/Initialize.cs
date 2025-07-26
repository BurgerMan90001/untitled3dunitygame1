using MyBox;
using UnityEngine;

public enum InitializeType
{
    None,
    TriggerSceneLoad,
    TriggerOnSceneLoad,
}

public class Initialize : MonoBehaviour
{
    [SerializeField] private bool _active;

    [ConditionalField(nameof(_active))]
    [SerializeField] private InitializeType _initializeType = InitializeType.None;


    [ConditionalField(nameof(_initializeType), false, InitializeType.TriggerSceneLoad)]
    [SerializeField] private SceneLoadTrigger _sceneTrigger;

    [ConditionalField(nameof(_initializeType), false, InitializeType.TriggerOnSceneLoad)]
    [SerializeField] private bool test;

    [Header("Initilize Pool")]
    [DisplayInspector]
    [ConditionalField(nameof(_active))]
    [SerializeField]
    private ObjectPool _pool;

    private Test Test;

    private void Start()
    {
        if (_active)
        {
            _pool.CreatePool();
            _pool.InstantiatePoolObjects();
            if (_initializeType is InitializeType.TriggerSceneLoad)
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

public class Test
{

}

