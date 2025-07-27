using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public List<Event> Events;


    public static EventManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("There is another Event Manager in the scene.");
        }

    }
}
