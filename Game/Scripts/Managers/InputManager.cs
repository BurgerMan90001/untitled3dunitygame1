using UnityEngine;

public class InputManager : Manager
{
    private static InputManager _Instance;
    public static InputManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<InputManager>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }



    private void Awake()
    {


    }
    private void Start()
    {




    }
    private void OnEnable()
    {

    }





}
