using MyBox;
using UnityEngine;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] private bool _triggerOnStart = false;
    [ConditionalField(nameof(_triggerOnStart))][SerializeField] private SceneType _triggerOnStartScene;
    [ConditionalField(nameof(_triggerOnStart))][SerializeField] private UserInterfaceType _triggerOnStartInterface;
    [ConditionalField(nameof(_triggerOnStart))][SerializeField] private Vector3 _triggerOnStartPosition = Vector3.zero;

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private CombatData _combatData;

    [Header("Debug")]
    [SerializeField] private bool _showDebugLoadingLogs = true;


    //private Vector3 _spawnPoint;

    private void Start()
    {
        if (_triggerOnStart)
        {
            SceneLoader.LoadScene(new(_triggerOnStartScene, _triggerOnStartInterface, _triggerOnStartPosition));
        }
    }

    private void OnEnable()
    {
        if (_triggerOnStart) return;

        SceneLoader.OnSceneLoadComplete += OnSceneLoadComplete;

        //    _dialogueData.Events.OnExitDialogue += OnExitDialogue;


    }
    private void OnDisable()
    {
        if (_triggerOnStart) return;

        SceneLoader.OnSceneLoadComplete -= OnSceneLoadComplete;

        //   _dialogueData.Events.OnExitDialogue -= OnExitDialogue;



    }
    private void OnSceneLoadComplete(SceneLoadingSettings sceneLoadingSettings)
    {

    }



}
