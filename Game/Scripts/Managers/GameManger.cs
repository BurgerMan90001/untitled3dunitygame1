using UnityEngine;


public class MainManger : MonoBehaviour
{
    private static MainManger _Instance;
    public static MainManger Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<MainManger>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }
    [Header("Managers")]
    /*
    [SerializeField] private List<ManagerSettings> _managerSettings;
    [SerializeField] private PlayerInput PlayerInput;
    [SerializeField] private Transform _userInterface;
    [SerializeField] private Transform _npcs; // the NPCs gameObject is itself
    */

    [SerializeField] private GameObject test;


    private Transform _managers; // the Managers gameObject is itself

    private void Awake()
    {

    }
    private void Start()
    {

    }

    private void OnDisable()
    {

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

    /*
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
    */


}

