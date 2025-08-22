using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public GameData GameData;

    private void Awake()
    {
        GameData = new GameData(); // Initialize with default values
    }
    /*
    public async void LoadDataAsync()
    {
        // Simulate an asynchronous data load
        await System.Threading.Tasks.Task.Delay(1000);
        Debug.Log("Player data loaded.");
    }
    */
}
