using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO MAKE COMBAT SYSTEM BETTER
//TODO MAKE BETTER ENEMY AI WITH ENEMY TURN.
/// <summary>
/// <br> Manages in-game combat. </br>
/// <br> Used in the combat scene. </br>
/// </summary>
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
        _combatData.OnEnterCombat += EnterCombat; // only needs to subscribe to on enter combat because this class will do the exit combat

    }

    private void OnDisable()
    {
        _combatData.OnEnterCombat -= EnterCombat;

    }
    private void PlayerTurn()
    {
        if (_combatData.CombatState != CombatStates.PlayerTurn) return;

        bool isDead = _enemyUnit.CombatStats.Hurt(_playerUnit.CombatStats.Damage);

        Debug.Log(_enemyUnit.CombatStats.Health); // UPDATE HUD


        if (isDead)
        {
            _combatData.SwitchCombatState(CombatStates.Won);
            EndBattle();
        } else
        {
            _combatData.SwitchCombatState(CombatStates.EnemyTurn);
            EnemyTurn();
        }

    }
    
    /// <summary>
    /// <br> COULD MAKE BETTER AI.</br>
    /// </summary>
    IEnumerator EnemyTurn()
    {
        Debug.Log("THE ENEMY ATTACKS!");

        yield return new WaitForSeconds(1f);


        bool isDead = _playerUnit.CombatStats.Hurt(_enemyUnit.CombatStats.Damage);

        Debug.Log(_playerUnit.CombatStats.Health);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            _combatData.SwitchCombatState(CombatStates.Lost);
        } else
        {
            _combatData.SwitchCombatState(CombatStates.PlayerTurn);
            PlayerTurn();
        }

    }
    private void EnterCombat(GameObject enemy)
    {
        SetupBattle(enemy);

        _combatData.SwitchCombatState(CombatStates.Start);


    }
    private void EndBattle()
    {
        if (_combatData.CombatState == CombatStates.Won)
        {
            Debug.Log("WINNER");
        }
        else if (_combatData.CombatState == CombatStates.Lost)
        {
            Debug.Log("DEFEAT");
        }
        _combatData.ExitCombat();
    }

    
    
    public bool ApplyHurt(HurtType type, CombatUnit target, CombatUnit attacker, float damageAmount)
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
        
    }
    

    public void ApplyBlock(GameObject target, float blockAmount) 
    {
        
        

    }
}

