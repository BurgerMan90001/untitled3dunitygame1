using MyBox;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO MAKE COMBAT SYSTEM BETTER
//TODO MAKE BETTER ENEMY AI WITH ENEMY TURN.



/// <summary>
/// <br> Manages in-game combat. </br>
/// <br> Listens for the combat event trigger. </br>
/// </summary>
public class CombatManager : MonoBehaviour
{
    [Header("HurtEffects")]
    public List<HurtEffect> HurtEffects;

    [Header("Prefabs")]
    public CombatUnit PlayerUnit;
    [ReadOnly] public CombatUnit EnemyUnit;


    [Header("Spawn Points")]
    [field: SerializeField] public Vector3 PlayerSpawnPoint { get; private set; } = new Vector3(3f, 4f, 0);
    [field: SerializeField] public Vector3 EnemySpawnPoint { get; private set; } = new Vector3(-3f, 4f, 0);


    [Header("Debugging")]
    [SerializeField] public bool DebugMode = true;


    [Header("Events")]
    [SerializeField] private CombatEvents _combatEvents;
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;

    public Stack<CombatStates> CombatState { get; private set; } = new Stack<CombatStates>();

    [SerializeField] private List<CombatStates> _combatStateStack = new List<CombatStates>();

    private void Awake()
    {

    }
    private void OnEnable()
    {

        _combatEvents.OnEnterCombat += OnEnterCombat;

    }

    private void OnDestroy()
    {
        _combatEvents.OnEnterCombat -= OnEnterCombat;
    }

    public void OnAttackButton(InputAction.CallbackContext ctx)
    {
        if (!IsPlayerTurn()) return;

        if (ctx.started)
        {
            StartCoroutine(PlayerAttack());
        }

    }
    public void OnBlockButton(InputAction.CallbackContext ctx)
    {
        if (!IsPlayerTurn()) return;
        if (ctx.started)
        {
            StartCoroutine(PlayerBlock());
        }


    }
    /// <summary>
    /// Triggered when the combat entered event is triggered.
    /// </summary>
    /// <param name="enemy"></param>
    private void OnEnterCombat(CombatUnit enemy)
    {

        EnemyUnit = enemy;

        _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Combat);

        PushCombatState(CombatStates.Start);

        PlayerTurn();
    }


    private IEnumerator PlayerAttack()
    {

        bool isDead = EnemyUnit.Hurt(PlayerUnit.CombatStats.Damage);

        Debug.Log($" ENEMY HEALTH {EnemyUnit.CombatStats.Health}"); // UPDATE HUD

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            PushCombatState(CombatStates.Won);
            EndBattle();
        }
        else
        {
            PushCombatState(CombatStates.EnemyTurn);
            StartCoroutine(EnemyTurn());
        }

    }
    private IEnumerator PlayerBlock()
    {
        Debug.Log("BLOCK");
        yield return new WaitForSeconds(2f);
    }



    public void PushCombatState(CombatStates combatState)
    {
        CombatState.Push(combatState);
        _combatStateStack = CombatState.ToList();
    }

    private bool IsPlayerTurn()
    {
        if (CombatState.TryPeek(out CombatStates combatState))
        {
            if (combatState == CombatStates.PlayerTurn)
            {
                return true;
            }
        }
        return false;
    }

    private CombatStates CheckIfCombatWon()
    {
        if (CombatState.TryPeek(out CombatStates combatState))
        {
            if (combatState == CombatStates.Won ||
                combatState == CombatStates.Lost)
            {
                return combatState;
            }
            else
            {
                Debug.LogWarning($" Found a combat state of : {combatState}, instead of a win condition.");
                return CombatStates.None;
            }
        }

        Debug.LogError("Could not find the most recent combat state.");
        return CombatStates.None;
    }
    private void PlayerTurn()
    {
        PushCombatState(CombatStates.PlayerTurn);
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


        bool isDead = PlayerUnit.Hurt(EnemyUnit.CombatStats.Damage);

        Debug.Log($" PLAYER HEALTH{PlayerUnit.CombatStats.Health}");

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            PushCombatState(CombatStates.Lost);
        }
        else
        {

            PlayerTurn();
        }

    }


    private void EndBattle()
    {
        PlayerUnit.ResetStats();
        EnemyUnit.ResetStats();


        var combatState = CheckIfCombatWon();




        _combatEvents.ExitCombat(combatState);



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


