using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private CombatData _combatData;

    [Header("HurtEffects")]
    [SerializeField] private Dictionary<HurtType, HurtEffect> hurtEffects;


    [Header("Debug")]
    [SerializeField] private bool debug;

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
    }
    private void ExitCombat()
    {
        Debug.Log("COMBAT EXITED");
    }
    public void ApplyHurt(HurtType type, GameObject target, float damage)
    {
        if (hurtEffects.ContainsKey(type))
        {
            hurtEffects[type].ApplyEffect(target, gameObject, damage);
        }
        else
        {
            Debug.LogError("There is no valid hurt effect.");

        }
    }
}



