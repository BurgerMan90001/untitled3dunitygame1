using UnityEngine;
// example singleton
public class Singleton : MonoBehaviour
{
    private static Singleton _Instance;
    public static Singleton Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<Singleton>();

                _Instance.name = _Instance.GetType().ToString();

                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

}
