
using System.Net.NetworkInformation;
using UnityEngine;


public class MainManger : MonoBehaviour 
{
    [SerializeField] private Transform _userInterface;
    [SerializeField] private Transform _npcs; // the NPCs gameObject is itself

    [SerializeField] private GameObject test;
    
    private static MainManger _instance;


    private Transform _managers; // the Managers gameObject is itself

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
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
        foreach (Transform manager in _managers) 
        {
            manager.gameObject.SetActive(true);

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
