using UnityEngine;

/// <summary>
/// <br> Triggers a scene load when an event is invoked. </br>
/// </summary>
public class SceneLoadEventTrigger : SceneLoadTrigger
{
    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private CombatData _combatData;
    private void OnEnable()
    {

        _dialogueData.Events.OnExitDialogue += OnExitDialogue;

        _combatData.Events.OnEnterCombat += OnEnterCombat;
        _combatData.Events.OnExitCombat += OnExitCombat;

    }
    private void OnDisable()
    {

        _dialogueData.Events.OnExitDialogue -= OnExitDialogue;

        _combatData.Events.OnEnterCombat -= OnEnterCombat;
        _combatData.Events.OnExitCombat -= OnExitCombat;

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
        LoadScene(SceneLoadingSettings.Combat);

    }
    private void OnExitCombat()
    {
        LoadScene(SceneLoadingSettings.MainGame);
    }
}
