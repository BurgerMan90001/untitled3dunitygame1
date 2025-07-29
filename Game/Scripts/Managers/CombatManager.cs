using System.Collections;
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


    private CombatData _combatData;

    [Header("Events")]
    [SerializeField] private CombatEvents _combatEvents;
    [SerializeField] private DialogueEvents _dialogueEvents;
    [SerializeField] private UserInterfaceEvents _userInterfaceEvents;

    private void Awake()
    {
        _combatData = GetComponent<CombatData>();

    }


    private void OnEnable()
    {

        _combatEvents.OnEnterCombat += OnEnterCombat;

    }

    private void OnDestroy()
    {
        _combatEvents.OnEnterCombat -= OnEnterCombat;
    }


    private void OnEnterCombat(CombatUnit enemy)
    {



        _userInterfaceEvents.SwitchToUserInterface(UserInterfaceType.Combat);

        _combatData.PushCombatState(CombatStates.Start);


        PlayerTurn();
    }


    private IEnumerator PlayerAttack()
    {

        bool isDead = _combatData.EnemyUnit.CombatStats.Hurt(_combatData.PlayerUnit.CombatStats.Damage);

        Debug.Log(_combatData.EnemyUnit.CombatStats.Health); // UPDATE HUD

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
    public void OnAttackButton(InputAction.CallbackContext ctx)
    {
        if (!_combatData.IsPlayerTurn()) return;

        if (ctx.started)
        {
            StartCoroutine(PlayerAttack());
        }

    }
    public void OnBlockButton(InputAction.CallbackContext ctx)
    {
        if (!_combatData.IsPlayerTurn()) return;
        if (ctx.started)
        {
            StartCoroutine(PlayerBlock());
        }


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


        bool isDead = _combatData.PlayerUnit.CombatStats.Hurt(_combatData.EnemyUnit.CombatStats.Damage);

        Debug.Log(_combatData.PlayerUnit.CombatStats.Health);

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
        _combatEvents.ExitCombat();
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


