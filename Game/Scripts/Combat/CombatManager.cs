using System.Collections.Generic;
using UnityEngine;

public enum CombatStates
{
    Start,
    PlayerTurn,
    EnemyTurn,
    Won,
    Lost,

}

public enum BlockType
{
    Normal,
    Damage, // damages the attacker back
}




//TODO MAKE COMBAT SYSTEM BETTER
public class CombatManager : MonoBehaviour, ISingleton
{
    public CombatStates CombatState;

    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("CombatStats")]
    [SerializeField] private CombatStats _playerCombatStats;
    [SerializeField] private CombatStats _enemyCombatStats;


    [Header("Spawn Points")]
    [SerializeField] private Vector3 _playerSpawnPoint;
    [SerializeField] private Vector3 _enemySpawnPoint;

    [Header("Data")]
    [SerializeField] private CombatData _combatData;
    [SerializeField] private InputData _inputData;
    [SerializeField] private UserInterfaceData userInterfaceData;

    [Header("HurtEffects")]
    [SerializeField] private List<HurtEffect> _hurtEffects;
    

    [Header("Debug")]
    [SerializeField] private bool _debugMode;


    private CombatUnit _playerUnit;
    private CombatUnit _enemyUnit;

    public static CombatManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        } else
        {
            Debug.LogWarning("There is a duplicate CombatManager in the scene. Destroying duplicate. ");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);

        
    }
    private void Start()
    {
        if (_debugMode)
        {
            Debug.Log("IN COMBAT DEBUG MODE");
            SetupBattle();
        }
    }
    private void SetupBattle()
    {

        GameObject playerGO = Instantiate(_playerPrefab, _playerSpawnPoint, Quaternion.identity);

        _playerUnit = playerGO.GetComponent<CombatUnit>();

        GameObject enemyGO = Instantiate(_enemyPrefab, _enemySpawnPoint, Quaternion.identity);

        _enemyUnit = enemyGO.GetComponent<CombatUnit>(); 
    }
    private void OnEnable()
    {
        _combatData.OnEnterCombat += EnterCombat;
        _combatData.OnExitCombat += ExitCombat;
    }

    private void OnDisable()
    {
        _combatData.OnEnterCombat -= EnterCombat;
        _combatData.OnExitCombat -= ExitCombat;
    }
    
    private void EnterCombat()
    {
        Debug.Log("COMBAT ENTERED");
        SceneLoadingManager.LoadScene("Combat",UserInterfaceType.Combat);
        SceneLoadingManager.SetSpawnPoint(_playerSpawnPoint);

        CombatState = CombatStates.Start;


    }
    private void ExitCombat()
    {
        Debug.Log("COMBAT EXITED");
        SceneLoadingManager.LoadScene("Main Game",UserInterfaceType.HUD);
    }
    public bool ApplyHurt(HurtType type, CombatStats target, CombatStats attacker, float damageAmount)
    {
        foreach (var kvp in _hurtEffects)
        {
            if (type == kvp.Type)
            {
                kvp.ApplyEffect(target, attacker, damageAmount);
                return true;
            }

        }
        return false;
        /*
        else
        {
            Debug.LogError("There is no valid hurt type with that hurt effect.");

        }
        */
    }


    public void ApplyBlock(GameObject target, float blockAmount) 
    {
        
        

    }
}

