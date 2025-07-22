using UnityEngine;
/// <summary>
/// <br> Triggers a scene load on start. </br>
/// </summary>
public class SceneLoadTrigger : MonoBehaviour
{


    [Header("Trigger On Start")]
    [SerializeField] private bool _triggerOnStart = false;
    [SerializeField] private string _loadedScene;
    [SerializeField] private UserInterfaceType _loadedUserInterface;
    [SerializeField] private Vector3 _loadedPosition = Vector3.zero;

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private CombatData _combatData;

    [Header("Debug")]
    [SerializeField] private bool _showDebugLoadingLogs = true;

    private void Start()
    {
        if (_triggerOnStart)
        {
            SceneLoader.LoadScene(new(_loadedScene, _loadedUserInterface, _loadedPosition));
        }
    }

    private void OnEnable()
    {
        if (_triggerOnStart) return;

        SceneLoader.OnSceneLoaded += test;
        /*
        _dialogueData.Events.OnExitDialogue += OnExitDialogue;

        _combatData.Events.OnEnterCombat += OnEnterCombat;
        _combatData.Events.OnExitCombat += OnExitCombat;
        */
    }

    private void test(SceneLoadingSettings sceneLoadingSettings)
    {
        if (_showDebugLoadingLogs)
        {
            Debug.Log($" Scene : {sceneLoadingSettings.SceneName}");
            Debug.Log($" Loaded scene interface : {sceneLoadingSettings.UserInterface}");
            Debug.Log($" Spawned at : {sceneLoadingSettings.PlayerSpawnPoint}");
            Debug.Log("LOADED SUCCESSFULLY");

        }
    }

    private void OnDisable()
    {
        if (_triggerOnStart) return;

        /*
        _dialogueData.Events.OnExitDialogue -= OnExitDialogue;

        _combatData.Events.OnEnterCombat -= OnEnterCombat;
        _combatData.Events.OnExitCombat -= OnExitCombat;
        */
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
        //    LoadScene(SceneLoadingSettings.Combat);

    }
    private void OnExitCombat()
    {
        //    LoadScene(SceneLoadingSettings.MainGame);
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
