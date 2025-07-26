
#region
#endregion

using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
public class UserInterfaceData : Data
{

    public MyDictionary<UserInterfaceType, VisualElement> UserInterfaceElements = new MyDictionary<UserInterfaceType, VisualElement>();




    /*
    [Header("Data")]
    [SerializeField] private Inventory _inventory;

    
    [Header("Debug")]
    [SerializeField] private bool _debugMode = true;
    */

    public Stack<UserInterfaceType> ShownInterfaces = new Stack<UserInterfaceType>();

    public UserInterfaceData()
    {

    }
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



/*
// singletonn that holds data references 
public class DataStore : MonoBehaviour
{
    public static DataStore Instance;

    public UserInterfaceData UserInterface;
    public UserInterfaceEvents UserInterfaceEvents;

    public gameInput gameInput;

    public DialogueData DialogueData;
    public DialogueEvents DialogueEvents;


    public List<Data> data;
    private void Awake()
    {

    }

}

*/