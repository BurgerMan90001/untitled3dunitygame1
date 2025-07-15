using UnityEngine;

public interface ISingleton
{
    /*
    public static Singleton Instance;
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"[Singleton] Instance of {typeof(T)} already exists, destroying duplicate!");
            Destroy(gameObject);
        }
    }
    */

}
