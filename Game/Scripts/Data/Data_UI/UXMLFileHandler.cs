
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

    private AssetLabelReference _labelReference;
    private AsyncOperationHandle _loadedUserInterfaces;


    private readonly bool _showLoadingResults = false;


    private Dictionary<UserInterfaceType, VisualElement> _userInterfaceElements;

    private UserInterfaceData _userInterfaceData;

    public UxmlFileHandler(VisualElement root, AssetLabelReference uxmlAssetLabelReference, UserInterfaceData userInterfaceData)
    {
        _root = root;

        _labelReference = uxmlAssetLabelReference;

        _userInterfaceData = userInterfaceData;
    }

    private void ShowLoadingResults(VisualTreeAsset visualTreeAsset, bool showLoadingResults)
    {
        if (showLoadingResults)
        {
            Debug.Log($"Loaded UXML: {visualTreeAsset.name}");

        }
    }
    public async Task LoadInterfacesAsync()
    {
        var uxmlLabelHandle = Addressables.LoadAssetsAsync<Object>(_labelReference.labelString);

        await uxmlLabelHandle.Task;

        if (uxmlLabelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedUserInterfaces = uxmlLabelHandle;

            SetupIntefaces(uxmlLabelHandle);


        }

    }
    private void SetupIntefaces(AsyncOperationHandle<IList<Object>> uxmlLabelHandle)
    {
        foreach (object result in uxmlLabelHandle.Result)
        {
            if (result is VisualTreeAsset visualTree)
            {
                ShowLoadingResults(visualTree, _showLoadingResults);
                SetUserInterfaceElementStyle(visualTree);

            }
            else
            {
                Debug.LogError("The loaded asset is not a VisualTreeAsset!");
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

        if (_loadedUserInterfaces.IsValid())
        {
            Addressables.Release(_loadedUserInterfaces);
        }
        else
        {
            Debug.LogWarning("The _loadedUserInterfaces handle wasn't valid.");
        }


    }



    private void SetUserInterfaceElementStyle(VisualTreeAsset visualTree)
    {
        VisualElement addedUserInterfaceElement = visualTree.CloneTree();

        addedUserInterfaceElement.style.position = Position.Absolute;
        addedUserInterfaceElement.style.flexGrow = 1;
        addedUserInterfaceElement.style.flexShrink = 1;
        addedUserInterfaceElement.style.alignSelf = Align.Stretch;
        addedUserInterfaceElement.style.width = Length.Percent(100);
        addedUserInterfaceElement.style.height = Length.Percent(100);

        addedUserInterfaceElement.name = visualTree.name;
        addedUserInterfaceElement.style.display = DisplayStyle.None;

        addedUserInterfaceElement.pickingMode = PickingMode.Ignore;

        _root.Add(addedUserInterfaceElement);

        UpdateUserInterfaceData(visualTree, addedUserInterfaceElement);
    }
    private void UpdateUserInterfaceData(VisualTreeAsset visualTree, VisualElement addedUserInterfaceElement)
    {
        UserInterfaceType userInterface = FindMatchingInterfaceType(visualTree.name);
        if (userInterface == UserInterfaceType.None)
        {
            Debug.LogWarning($"Can't find a user interface type from the visual tree name : {visualTree.name}");
            return;
        }
        else
        {
            _userInterfaceData.UserInterfaceElements.Add(userInterface, addedUserInterfaceElement);


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

/*


public struct UserInterfaceStyle
{
    public VisualElement VisualElement;
    public UserInterfaceType UserInterfaceType;

    public StyleEnum<Position> Position { get; set; }
    public static readonly UserInterfaceStyle StyleOverlay = new UserInterfaceStyle();

    public UserInterfaceStyle()
    {
        VisualElement = visualElement;
    }
 //   public string 

}
*/