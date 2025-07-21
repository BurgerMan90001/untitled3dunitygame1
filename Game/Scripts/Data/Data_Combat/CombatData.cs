using System;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// <br> Combat events and data. <br>
/// <br> Initiates the combat. </br>
/// </summary>
[CreateAssetMenu(menuName = "Data/CombatData")]
public class CombatData : Data
{
    [Header("HurtEffects")]
    public List<HurtEffect> HurtEffects;

    [Header("Prefabs")]
    public CombatUnit PlayerUnit;
    public CombatUnit EnemyUnit;


    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    //  [SerializeField] private InputData _inputData;
    [Header("Settings")]


    [Header("Spawn Points")]
    [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; } = new Vector3(3f, 4f, 0);
    [field: SerializeField] public Vector3 EnemySpawnPoint { get; private set; } = new Vector3(-3f, 4f, 0);


    [Header("Debugging")]
    [SerializeField] public bool DebugMode = true;


    public CombatEvents Events { get; private set; } = new CombatEvents();

    [field: SerializeField] public CombatStates CombatState { get; private set; }


    private void OnEnable()
    {
        _dialogueData.Events.OnExitDialogue += CheckIfCombatEntered;

        Events.OnEnterCombat += OnEnterCombat;
        Events.OnExitCombat += OnExitCombat;

    }
    private void OnDisable()
    {
        _dialogueData.Events.OnExitDialogue -= CheckIfCombatEntered;

        Events.OnEnterCombat -= OnEnterCombat;
        Events.OnExitCombat -= OnExitCombat;
    }

    private void OnEnterCombat(CombatUnit npc)
    {
        SceneLoader.LoadScene(SceneLoadingSettings.Combat);

    }
    private void OnExitCombat()
    {
        SceneLoader.LoadScene(SceneLoadingSettings.MainGame);
    }
    public void SwitchCombatState(CombatStates combatState)
    {
        CombatState = combatState;
        Events.SwitchCombatState(CombatState);
    }
    private void CheckIfCombatEntered(GameObject npc)
    {
        bool combatEntered = (bool)_dialogueData.Story.variablesState["combatEntered"];
        if (combatEntered)
        {

            if (npc.TryGetComponent(out CombatUnit combatUnit))
            {
                Events.EnterCombat(combatUnit);
            }
            else
            {
                Debug.LogWarning("The story's combatEntered value is true, but the npc does not have a CombatUnit component! ");
                return;
            }

        }

    }

    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }
}


