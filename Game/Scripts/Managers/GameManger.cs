
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MainManger : MonoBehaviour, ISingleton
{
    [Header("Managers")]
    [SerializeField] private List<ManagerSettings> _managerSettings;
    [SerializeField] private PlayerInput PlayerInput;
    [SerializeField] private Transform _userInterface;
    [SerializeField] private Transform _npcs; // the NPCs gameObject is itself

    [SerializeField] private GameObject test;
    
    public static MainManger Instance;


    private Transform _managers; // the Managers gameObject is itself

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogWarning("There was another Main Manager instance in the scene. Destroying duplicate.");
            Destroy(gameObject);
        }

            _managers = GetComponent<Transform>();

        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        ToggleManagers();

    //    ToggleUserInterface();

     //   _npcs.gameObject.SetActive(true); 
                                           

    }
    
    private void OnDisable()
    {
    
    //    Addressables.ReleaseInstance(test);
    }
    private void OnDestroy()
    {
        
    }
    #region
    /// <summary>
    /// </br> loops throgh all of the managers  </br>
    /// </br>the order in the gameObject hierarchy from top to bottom is the order in which the managers will be activated </br>
    /// </summary>
    #endregion

    private void ToggleManagers()
    {
        foreach (ManagerSettings managerSetting in _managerSettings)
        {
            if (managerSetting.Enabled)
            {
                Debug.Log(managerSetting.ManagerType.name);
                Transform manager = transform.Find(managerSetting.ManagerType.name);

                manager.gameObject.SetActive(managerSetting.Enabled);
            }
            
        }
        
    }
    private void ToggleNPCs()
    {
        foreach (Transform npc in _npcs) // loops through all of the NPCs and activates them
        {
            npc.gameObject.SetActive(true);
        }
    }
    private void ToggleUserInterface()
    {
        _userInterface.gameObject.SetActive(true);
    }
    

}

