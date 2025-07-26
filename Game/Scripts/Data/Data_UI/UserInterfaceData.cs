/*
#region
/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
#endregion
[CreateAssetMenu(menuName = "Data/userInterfaceEvents")]
public class UserInterfaceData : Data
{
    
    [Header("Data")]
    [SerializeField] private Inventory _inventory;

    
    [Header("Debug")]
    [SerializeField] private bool _debugMode = true;

    public Stack<UserInterfaceType> ShownInterfaces = new Stack<UserInterfaceType>();


    public float Test;
    // [ReadOnly][SerializeField] private List<UserInterfaceType> ShownInterfacesStack;




    public override void LoadData(GameData data)
    {
        throw new NotImplementedException();
    }

    public override void SaveData(GameData data)
    {
        throw new NotImplementedException();
    }

}

*/
public class UserInterfaceData
{

    public UserInterfaceData()
    {

    }
}

/*
// singletonn that holds data references 
public class DataStore : MonoBehaviour
{
    public static DataStore Instance;

    public UserInterfaceData UserInterface;
    public UserInterfaceEvents UserInterfaceEvents;

    public InputData InputData;

    public DialogueData DialogueData;
    public DialogueEvents DialogueEvents;


    public List<Data> data;
    private void Awake()
    {

    }

}

*/