using System.Collections.Generic;
using UnityEngine;


//TODO MAKE COMBAT SYSTEM BETTER
public class CombatManager : MonoBehaviour, ISingleton
{
    [Header("Spawn Point")]
    [SerializeField] private Vector3 _spawnPoint;

    [Header("Data")]
    [SerializeField] private CombatData _combatData;
    [SerializeField] private InputData _inputData;

    [Header("HurtEffects")]
    [SerializeField] private Dictionary<HurtType, HurtEffect> _hurtEffects;
    

    [Header("Debug")]
    [SerializeField] private bool _debugMode;

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
        SceneLoadingManager.SetSpawnPoint(_spawnPoint);
    }
    private void ExitCombat()
    {
        Debug.Log("COMBAT EXITED");
        SceneLoadingManager.LoadScene("Main Game",UserInterfaceType.HUD);
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



