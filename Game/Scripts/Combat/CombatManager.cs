using System.Collections.Generic;
using UnityEngine;

//TODO MAKE COMBAT SYSTEM BETTER
public class CombatManager : MonoBehaviour, ISingleton
{

    [Header("Prefabs")]
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GameObject _enemyPrefab;

    [Header("CombatStats")]
    [SerializeField] private CombatStats _playerCombatStats;
    [SerializeField] private CombatStats _enemyCombatStats;

    [Header("Data")]
    [SerializeField] private CombatData _combatData;

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
       //     SetupBattle();
        }
    }
    private void SetupBattle(GameObject enemy)
    {
        
        if (_debugMode) // if there is no player prefab
        {
            GameObject playerGO = Instantiate(_playerPrefab, _combatData.PlayerSpawnPoint, Quaternion.identity); // GO is gameobject

            _playerUnit = playerGO.GetComponent<CombatUnit>();
        }
        if (enemy == null || _debugMode)

        {
            GameObject enemyGO = Instantiate(_enemyPrefab, _combatData.EnemySpawnPoint, Quaternion.identity);
            _enemyUnit = enemyGO.GetComponent<CombatUnit>();
        }
        
    }
    private void OnEnable()
    {
        _combatData.OnEnterCombat += EnterCombat;
     //   _combatData.OnExitCombat += ExitCombat;
    }

    private void OnDisable()
    {
        _combatData.OnEnterCombat -= EnterCombat;
   //     _combatData.OnExitCombat -= ExitCombat;
    }
    private void PlayerAction()
    {
        if (_combatData.CombatState != CombatStates.PlayerTurn) return;

        bool isDead = _enemyUnit.CombatStats.Hurt(_playerUnit.CombatStats.Damage);

        Debug.Log(_enemyUnit.CombatStats.Health);


        if (isDead)
        {
            _combatData.CombatState = CombatStates.Won;
            _combatData.ExitCombat();
        } else
        {
            _combatData.CombatState = CombatStates.EnemyTurn;
        }

    }
    private void EnterCombat(GameObject enemy)
    {
        SetupBattle(enemy);

        _combatData.CombatState = CombatStates.Start;

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

