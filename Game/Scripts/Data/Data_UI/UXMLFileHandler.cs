
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

// TODO OPTIMIZE 
public class UxmlFileHandler
{
    private VisualElement _root;

    private AsyncOperationHandle _loadedInterfaces;

    private readonly bool _showLoadingResults = false;

    private Dictionary<UserInterfaceType, VisualElement> _userInterfaces = new Dictionary<UserInterfaceType, VisualElement>();

    public UxmlFileHandler(VisualElement root)
    {
        _root = root;


    }

    private void ShowLoadingResults(VisualTreeAsset visualTreeAsset, bool showLoadingResults)
    {
        if (showLoadingResults)
        {
            Debug.Log($"Loaded UXML: {visualTreeAsset.name}");

        }
    }
    public async Task<Dictionary<UserInterfaceType, VisualElement>> LoadInterfacesAsync(AssetLabelReference _labelReference)
    {
        var uxmlLabelHandle = Addressables.LoadAssetsAsync<Object>(_labelReference.labelString);

        await uxmlLabelHandle.Task;

        if (uxmlLabelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedInterfaces = uxmlLabelHandle;

            SetupIntefaces(uxmlLabelHandle);

            return _userInterfaces;
        }

        return null;
    }

    private void SetupIntefaces(AsyncOperationHandle<IList<Object>> uxmlLabelHandle)
    {
        foreach (object result in uxmlLabelHandle.Result)
        {
            if (result is VisualTreeAsset visualTree)
            {
                ShowLoadingResults(visualTree, _showLoadingResults);
                var element = SetupInterface(visualTree);
                UpdateInterfaceDictionary(element);
            }
            else
            {
                Debug.LogError("The loaded asset is not a VisualTreeAsset! Unable to add to userinterfaces.");
            }

        }

    }
    #region
    /// <summary>
    /// <br> Releases all uxml addressables assets that have been loaded. </br>
    /// </summary>
    #endregion
    public void ReleaseInterfaces() // callded in ondestroy 
    {

        if (_loadedInterfaces.IsValid())
        {
            Addressables.Release(_loadedInterfaces);
        }
        else
        {
            Debug.LogWarning("The _loadedUserInterfaces handle wasn't valid.");
        }


    }



    private VisualElement SetupInterface(VisualTreeAsset visualTree)
    {
        VisualElement interfaceElement = visualTree.CloneTree();

        interfaceElement.style.position = Position.Absolute;
        interfaceElement.style.flexGrow = 1;
        interfaceElement.style.flexShrink = 1;
        interfaceElement.style.alignSelf = Align.Stretch;
        interfaceElement.style.width = Length.Percent(100);
        interfaceElement.style.height = Length.Percent(100);

        interfaceElement.name = visualTree.name;
        interfaceElement.style.display = DisplayStyle.None;

        interfaceElement.pickingMode = PickingMode.Ignore;

        _root.Add(interfaceElement);
        return interfaceElement;

    }
    private void UpdateInterfaceDictionary(VisualElement interfaceElement)
    {
        UserInterfaceType userInterface = FindMatchingInterfaceType(interfaceElement.name);
        if (userInterface == UserInterfaceType.None)
        {
            Debug.LogWarning($"Can't find a user interface type from the visual tree name : {interfaceElement.name}");
            return;
        }
        else
        {
            _userInterfaces.Add(userInterface, interfaceElement);
        }
    }


    #region
    /// <summary>
    /// <br> Finds the first matching interface type by its exact name. </br>
    /// <br> If it can't find anything, it uses the default, which is the first  </br>
    /// </summary>
    /// <param name="name"></param>
    #endregion
    private UserInterfaceType FindMatchingInterfaceType(string name)
    {
        var firstMatch = System.Enum.GetValues(typeof(UserInterfaceType))
                .Cast<UserInterfaceType>()
                .FirstOrDefault(g => g.ToString().Contains(name));
        if (_showLoadingResults)
        {
            Debug.Log(firstMatch);
        }
        return firstMatch;
    }
}




public struct UserInterfaceStyle
{
    public VisualElement Target;
    public UserInterfaceType UserInterfaceType;

    public float FlexGrow;
    public float FlexShrink;

    public Position Position;
    public Align Align;

    public Length Width;
    public Length Height;

    public static readonly UserInterfaceStyle Overlay = new();
    /*
    public UserInterfaceStyle(VisualElement target)
    {
        Target = target;
    }
    */
    //   public string 

}
