
#region
#endregion

using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// <br> Toggleable user interfaces. </br>
/// </summary>
public class UserInterfaceData : Data
{
    // for caching on assembely reload
    public MyDictionary<UserInterfaceType, VisualElement> UserInterfaceElements = new MyDictionary<UserInterfaceType, VisualElement>();
    public Stack<UserInterfaceType> ShownInterfaces = new Stack<UserInterfaceType>();

    public float Test;

    [Header("Events")]
    [SerializeField] private UserInterfaceEvents _events;

    /*
    [Header("Data")]
    [SerializeField] private Inventory _inventory;

    
    [Header("Debug")]
    [SerializeField] private bool _debugMode = true;
    */
    private void Start()
    {
        SceneLoader.OnSceneLoadComplete += test;
    }

    private void test(SceneLoadingSettings settings)
    {
        Debug.LogError(UserInterfaceElements.Count);
    }

    private void OnEnable()
    {
        _events.OnShowInterface += ShowInterface;
        _events.OnHideRecentInterface += HideRecentInterface;
    }
    private void OnDestroy()
    {
        _events.OnShowInterface -= ShowInterface;
        _events.OnHideRecentInterface -= HideRecentInterface;
    }
    #region
    /// <summary>
    /// <br> Switches to the UserInterfaceType userInterface. </br>
    /// </summary>
    /// <param name="userInterface"></param>
    #endregion
    public void SwitchToUserInterface(UserInterfaceType userInterface)
    {
        if (ShownInterfaces == null)
        {
            Debug.LogError("Shown interfaces is null.");
            return;
        }
        HideRecentInterface();
        ShowInterface(userInterface);

    }



    public void ShowInterface(UserInterfaceType userInterface)
    {
        if (userInterface == UserInterfaceType.None)
        {
            Debug.LogWarning($"Can't show {UserInterfaceType.None}");
            return;
        }

        ShownInterfaces.Push(userInterface);
        VisualElement elementToBeShown = UserInterfaceElements[userInterface];
        elementToBeShown.style.display = DisplayStyle.Flex;

        //  ShownInterfacesStack = ShownInterfaces.ToList();


    }
    /// <summary>
    /// <br> Hides the most recently shown interface. Does nothing if there is none. </br>
    /// </summary>
    public void HideRecentInterface()
    {

        if (ShownInterfaces.TryPop(out UserInterfaceType userInterface))
        {
            VisualElement elementToBeHiden = UserInterfaceElements[userInterface];
            elementToBeHiden.style.display = DisplayStyle.None;

            //   ShownInterfacesStack = ShownInterfaces.ToList<UserInterfaceType>();
        }
    }
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