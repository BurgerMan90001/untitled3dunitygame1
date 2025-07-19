using System;
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

    [Header("Prefabs For Debuging")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;


    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
  //  [SerializeField] private InputData _inputData;
    [Header("Settings")]


    [Header("Spawn Points")]
    [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; } = new Vector3(3f, 4f, 0);
    [field: SerializeField] public Vector3 EnemySpawnPoint { get; private set; } = new Vector3(-3f, 4f, 0);
    [SerializeField] private bool _debug = true;

    public event Action<GameObject> OnEnterCombat;
    public event Action OnExitCombat;
    
 //   public event Action OnTurnChanged;

    [field: SerializeField] public CombatStates CombatState { get ; private set; }


    private void OnEnable()
    {
        _dialogueData.OnExitDialogue += CheckIfBattleEntered;
       
    }
    private void OnDisable()
    {
        _dialogueData.OnExitDialogue -= CheckIfBattleEntered;
    }
    private void CheckIfBattleEntered(GameObject npc)
    {
        bool battleEntered = (bool) _dialogueData.Story.variablesState["battleEntered"];
        if (battleEntered)
        {
            EnterCombat(npc);
        }
        
    }
    #region
    /// <summary>
    /// <br> Switches the combat state. </br>
    /// </summary>
    /// <param name="combatState"></param>
    #endregion
    public void SwitchCombatState(CombatStates combatState)
    {
        CombatState = combatState;
    }
    
    #region
    /// <summary>
    ///  <br> Doesn't do anything if enteredCombat is false. </br>
    /// <br> Triggers the OnEnterCombat event. </br>
    /// </summary>
    #endregion
    public void EnterCombat(GameObject enemy)
    {


        OnEnterCombat?.Invoke(enemy);

        if (_debug)
        {
            Debug.Log("ENTERED COMBAT");
        }

        SceneLoadingManager.LoadScene("Combat", UserInterfaceType.Combat);
        SceneLoadingManager.SetSpawnPoint(PlayerSpawnPoint);

        

    }
    /// <summary>
    /// <br> Triggers the OnExitCombat event. </br>
    /// </summary>
    public void ExitCombat()
    {
        OnExitCombat?.Invoke();

        if (_debug)
        {
            Debug.Log("EXITED COMBAT");
        }

        SceneLoadingManager.LoadScene("Main Game", UserInterfaceType.HUD);
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
