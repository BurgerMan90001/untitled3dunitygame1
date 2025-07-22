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


    private void OnExitDialogue(GameObject npc)
    {
        bool combatEntered = (bool)CheckVariableState("combatEntered");
        _combatData.CheckIfCombatEntered(npc, combatEntered);
    }

    private object CheckVariableState(string variableName)
    {
        return _dialogueData.Story.variablesState[variableName];
    }

    private void OnEnterCombat(CombatUnit npc)
    {
        SceneLoader.LoadScene(SceneLoadingSettings.Combat);

    }
    private void OnExitCombat()
    {
        SceneLoader.LoadScene(SceneLoadingSettings.MainGame);
    }

    /*
    private void LoadScene(SceneLoadingSettings sceneLoadingSettings)
    {

        if (_debugMode)
        {
            Debug.Log(sceneLoadingSettings.PlayerSpawnPoint);
            Debug.Log(sceneLoadingSettings.UserInterface);
            Debug.Log(sceneLoadingSettings.SceneName);
        }
        if (sceneLoadingSettings.SceneName != null)
        {
            SceneLoader.LoadScene(sceneLoadingSettings);

        }
        else
        {
            Debug.LogError("The loading settings' scene name is null. ");
            return;
        }
    }

    */

}
