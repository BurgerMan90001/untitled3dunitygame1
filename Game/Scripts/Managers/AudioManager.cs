using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

 //   public Sound[] Sounds;
    private void Awake()
    {
        if (Instance != null && Instance != this) // singleton 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);


    }
    
    
    

}
