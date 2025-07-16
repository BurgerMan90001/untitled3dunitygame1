using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private CombatData _combatData;

    [Header("HurtEffects")]
    [SerializeField] private Dictionary<HurtType, HurtEffect> _hurtEffects;


    [Header("Debug")]
    [SerializeField] private bool _debug;

    private void Awake()
    {
        
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
        SceneLoadingManager.LoadScene("Combat",UserInterfaceType.MainMenu);
    }
    private void ExitCombat()
    {
        Debug.Log("COMBAT EXITED");
    }
    public void ApplyHurt(HurtType type, GameObject target, float damageAmount)
    {
        if (_hurtEffects.ContainsKey(type))
        {
            _hurtEffects[type].ApplyEffect(target, gameObject, damageAmount);
        }
        else
        {
            Debug.LogError("There is no valid hurt type with that hurt effect.");

        }
    }


    public void ApplyBlock(GameObject target, float blockAmount) 
    {
        
        

    }
}


public enum BlockType 
{
    Normal,
    Damage, // damages the attacker back
}



