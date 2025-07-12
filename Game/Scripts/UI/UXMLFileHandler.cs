
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UIElements;

// TODO ADD FULL PATHS FOR OTHER UIS OR OPTIMIZE 
public class UXMLFileHandler
{
    private AssetLabelReference _labelReference;

    private bool _showLoadingResults = false;


    private VisualElement _root;
    private VisualElement _addedUserInterfaceElement;
    private VisualTreeAsset _visualTree;
    private UserInterfaces[] _userInterfacesToBeLoaded;

    private readonly string _pathToUI_Elements = "Game/UI_Elements";

    private AsyncOperationHandle _loadedUserInterfaces;

    public Dictionary<UserInterfaces, VisualElement> UserInterfaceElements { get; private set; }
   

    public UXMLFileHandler(VisualElement root, AssetLabelReference uxmlAssetLabelReference)
    {
        _root = root;
        
        _labelReference = uxmlAssetLabelReference;

        UserInterfaceElements = new Dictionary<UserInterfaces, VisualElement>();
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
    /// <summary>
    /// <br> Releases all uxml addressables assets that have been loaded. </br>
    /// </summary>
    public void ReleaseInterfaces() // callded in ondestroy 
    {
        
        if (_loadedUserInterfaces.IsValid())
        {
            Addressables.Release(_loadedUserInterfaces);
        } else
        {
            Debug.LogWarning("The _loadedUserInterfaces handle wasn't valid.");
        }
        
        
    }



    private void SetUserInterfaceElementStyle(VisualTreeAsset visualTree)
    {
        _addedUserInterfaceElement = visualTree.CloneTree();

        _addedUserInterfaceElement.style.position = Position.Absolute;
        _addedUserInterfaceElement.style.flexGrow = 1;
        _addedUserInterfaceElement.style.flexShrink = 1;
        _addedUserInterfaceElement.style.alignSelf = Align.Stretch;
        _addedUserInterfaceElement.style.width = new Length(100, LengthUnit.Percent);
        _addedUserInterfaceElement.style.height = new Length(100, LengthUnit.Percent);

        _addedUserInterfaceElement.name = visualTree.name;
        _addedUserInterfaceElement.style.display = DisplayStyle.None;

        _root.Add(_addedUserInterfaceElement);

        UpdateUserInterfaceData(visualTree);
    }
    private void UpdateUserInterfaceData(VisualTreeAsset visualTree)
    {
        UserInterfaces userInterface = FindMatchingInterfaceType(visualTree.name);
        if (userInterface == UserInterfaces.None)
        {
            Debug.LogWarning("Can't find a user interface type from the visual tree name.");
            return;
        }
        else
        {
            UserInterfaceElements.Add(userInterface, _addedUserInterfaceElement);


        }
    }
    

    #region
    /// <summary>
    /// <br> Finds the first matching interface type by its exact name. </br>
    /// <br> If it can't find anything, it uses the default, which is the first  </br>
    /// </summary>
    /// <param name="name"></param>
    #endregion
    private UserInterfaces FindMatchingInterfaceType(string name)
    {
        var firstMatch = System.Enum.GetValues(typeof(UserInterfaces))
                .Cast<UserInterfaces>()
                .FirstOrDefault(g => g.ToString().Contains(name));
        if (_showLoadingResults)
        {
            Debug.Log(firstMatch);
        }
        return firstMatch;
    }
}
    


