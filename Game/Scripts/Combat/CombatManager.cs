using System.Collections;
using UnityEngine;




//TODO MAKE COMBAT SYSTEM BETTER
//TODO MAKE BETTER ENEMY AI WITH ENEMY TURN.
/// <summary>
/// <br> Manages in-game combat. </br>
/// <br> Used in the combat scene. </br>
/// </summary>
public class CombatManager : MonoBehaviour, ISingleton
{

    [Header("Data")]
    [SerializeField] private DialogueData _dialogueData;
    [SerializeField] private CombatData _combatData;


    private CombatUnit _playerUnit;
    private CombatUnit _enemyUnit;

    public static CombatManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Debug.LogWarning("There is a duplicate CombatManager in the scene. Destroying duplicate. ");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if (_combatData.DebugMode)
        {
            Debug.Log("IN COMBAT DEBUG MODE");
            _combatData.Events.EnterCombat(_combatData.EnemyUnit);
        }
    }

    private void OnEnable()
    {
        _dialogueData.Events.OnExitDialogue += CheckIfEnteredCombat;

    }
    private void OnDisable()
    {

        _dialogueData.Events.OnExitDialogue -= CheckIfEnteredCombat;

    }
    private void CheckIfEnteredCombat(GameObject npc)
    {
        if (_dialogueData.CombatEntered)
        {
            if (npc.TryGetComponent(out CombatUnit combatUnit))
            {
                EnterCombat(combatUnit);
            }
            else
            {
                Debug.LogError("Combat was entered, but the npc does not have a CombatUnit monobehaviour.");
            }

        }
    }




    private IEnumerator PlayerAttack()
    {

        bool isDead = _enemyUnit.CombatStats.Hurt(_playerUnit.CombatStats.Damage);

        Debug.Log(_enemyUnit.CombatStats.Health); // UPDATE HUD

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            _combatData.PushCombatState(CombatStates.Won);
            EndBattle();
        }
        else
        {
            _combatData.PushCombatState(CombatStates.EnemyTurn);
            StartCoroutine(EnemyTurn());
        }

    }
    private IEnumerator PlayerBlock()
    {
        Debug.Log("BLOCK");
        yield return new WaitForSeconds(2f);
    }
    public void OnAttackButton()
    {
        if (!_combatData.IsPlayerTurn()) return;

        StartCoroutine(PlayerAttack());
    }
    public void OnBlockButton()
    {
        if (!_combatData.IsPlayerTurn()) return;

        StartCoroutine(PlayerBlock());

    }

    private void PlayerTurn()
    {
        _combatData.PushCombatState(CombatStates.PlayerTurn);
        Debug.Log("YOUR TURN");
    }
    #region
    /// <summary>
    /// <br> COULD MAKE BETTER AI.</br>
    /// </summary>
    #endregion
    private IEnumerator EnemyTurn()
    {
        Debug.Log("THE ENEMY ATTACKS!");

        yield return new WaitForSeconds(1f);


        bool isDead = _playerUnit.CombatStats.Hurt(_enemyUnit.CombatStats.Damage);

        Debug.Log(_playerUnit.CombatStats.Health);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            _combatData.PushCombatState(CombatStates.Lost);
        }
        else
        {

            PlayerTurn();
        }

    }

    private void EnterCombat(CombatUnit enemy)
    {

        DontDestroyOnLoad(enemy.gameObject);

        _combatData.Events.EnterCombat(enemy);


        _combatData.PushCombatState(CombatStates.Start);

        _enemyUnit = _combatData.EnemyUnit;
        _playerUnit = _combatData.EnemyUnit;

        PlayerTurn();
    }
    private void EndBattle()
    {
        if (_combatData.DidWin())
        {
            Debug.Log("WINNER");
        }
        else
        {
            Debug.Log("DEFEAT");
        }
        _combatData.Events.ExitCombat();
    }


    /*
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
    */
}


