
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<ScriptableObject> _inputs;
    private Dictionary<string, InputActionMap> _actionMaps = new Dictionary<string, InputActionMap>();
    public PlayerInput PlayerInput { get; private set; }

    
    private void Awake()
    { 
        PlayerInput = GetComponent<PlayerInput>();
        
    }
    private void Start()
    {
        
        foreach (var map in PlayerInput.actions.actionMaps)
        {
            PlayerInput.SwitchCurrentActionMap(map.name);
        }
        
        
    }
    private void OnEnable()
    {
        
    }

    public void SwitchToActionMap(string mapName)
    {
        Debug.Log(mapName);
        PlayerInput.SwitchCurrentActionMap(mapName);
        

        /*
        if (_actionMaps.ContainsKey(mapName))
        {
            PlayerInput.SwitchCurrentActionMap(mapName);
            

            Debug.Log($"Switched to {mapName} action map");
        }
        else
        {
            Debug.LogWarning("Can't find action map from mapName.");
        }
        */
    }
    
    

}
