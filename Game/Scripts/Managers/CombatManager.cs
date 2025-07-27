using System.Collections;
using UnityEngine;




//TODO MAKE COMBAT SYSTEM BETTER
//TODO MAKE BETTER ENEMY AI WITH ENEMY TURN.
/// <summary>
/// <br> Manages in-game combat. </br>
/// <br> Used in the combat scene. </br>
/// </summary>
public class CombatManager : MonoBehaviour, ICombatManager
{


    private CombatData _combatData;


    private CombatEvents _combatEvents;
    private DialogueEvents _dialogueEvents;


    private void Awake()
    {
        _combatData = GetComponent<CombatData>();

    }
    private void Start()
    {
        /*
        if (_combatData.DebugMode)
        {
            Debug.Log("IN COMBAT DEBUG MODE");
            _combatEvents.EnterCombat(_combatData.EnemyUnit);
        }
        */
    }
    public void Initialise(DialogueEvents dialogueEvents, CombatEvents combatEvents)
    {
        _dialogueEvents = dialogueEvents;
        _combatEvents = combatEvents;
    }
    private void OnEnable()
    {
        _dialogueEvents.OnExitDialogue += CheckIfEnteredCombat; // TODO MOVE COMBAT CHECK INTO DIALOGUE EVENTS  

    }
    private void OnDisable()
    {

        _dialogueEvents.OnExitDialogue -= CheckIfEnteredCombat;

    }
    private void CheckIfEnteredCombat(GameObject npc)
    {
        /*
        if (_dialogueEvents.CombatEntered)
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
        */
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

    private void EnterCombat(CombatUnit enemy)
    {

        DontDestroyOnLoad(enemy.gameObject);

        _combatEvents.EnterCombat(enemy);


        _combatData.PushCombatState(CombatStates.Start);


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


