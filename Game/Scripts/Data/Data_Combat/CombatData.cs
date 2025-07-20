using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public enum CombatStates
{
    None,
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost,

}

public enum CombatAction
{
    Attack,
    Block,
    Flee,
}
public enum BlockType
{
    Normal,
    Damage, // damages the attacker back
}
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

    [field: SerializeField] public CombatStates CombatState { get ; private set; }

    
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
        SceneLoadingManager.LoadScene("Combat", UserInterfaceType.Combat);
        SceneLoadingManager.SetSpawnPoint(PlayerSpawnPoint);
    }
    private void OnExitCombat()
    {
        SceneLoadingManager.LoadScene("Main Game", UserInterfaceType.HUD);
    }
    public void SwitchCombatState(CombatStates combatState)
    {
        CombatState = combatState;
        Events.SwitchCombatState(CombatState);
    }
    private void CheckIfCombatEntered()
    {
        bool combatEntered = (bool) _dialogueData.Story.variablesState["combatEntered"];
        if (combatEntered)
        {
       //     EnterCombat(npc);
        }
        
    }
    

    /*
    /// <summary>
    /// <br> Triggers the OnTurnChanged  event. </br>
    /// </summary>
    public void ChangeTurn() 
    {
        OnTurnChanged?.Invoke();

        if (_debug)
        {
            Debug.Log("CHANGED TURNS");
        }

    }
    */
}


public class CombatEvents : IEvent
{
    public event Action<CombatUnit> OnEnterCombat;
    public event Action OnExitCombat;
    public event Action<CombatStates> OnCombatStateSwitched;

    #region
    /// <summary>
    /// <br> Switches the combat state. </br>
    /// </summary>
    /// <param name="combatState"></param>
    #endregion
    public void SwitchCombatState(CombatStates combatState)
    {
        OnCombatStateSwitched?.Invoke(combatState);

    }

    #region
    /// <summary>
    /// <br> Loads the Combat scene with a combat interface. </br>
    ///  <br> Doesn't do anything if enteredCombat is false. </br>
    /// <br> Triggers the OnEnterCombat event. </br>
    /// </summary>
    #endregion
    public void EnterCombat(CombatUnit enemyUnit)
    {


        OnEnterCombat?.Invoke(enemyUnit);


    }
    #region
    /// <summary>
    /// <br> Triggers the OnExitCombat event. </br>
    /// <br> Loads the previous scene. </br>
    /// </summary>
    #endregion
    public void ExitCombat()
    {
        OnExitCombat?.Invoke();
    }
}
