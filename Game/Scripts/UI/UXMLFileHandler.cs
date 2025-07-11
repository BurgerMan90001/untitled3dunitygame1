
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

    private VisualElement _root;
    private VisualElement _addedUserInterfaceElement;
    private VisualTreeAsset _visualTree;
    private UserInterfaces[] _userInterfacesToBeLoaded;

    private readonly string _pathToUI_Elements = "Game/UI_Elements";

    private AsyncOperationHandle _loadedUserInterfaces;

    private Dictionary<UserInterfaces, VisualElement> _userInterfaces = new Dictionary<UserInterfaces, VisualElement>();
   

    public UXMLFileHandler(VisualElement root, AssetLabelReference uxmlAssetLabelReference)
    {
        _root = root;
        
        _labelReference = uxmlAssetLabelReference;
    }
    #region
    /// <summary>
    /// <br> Searches for the user interface string name through the key pair value of userInterface. </br>
    /// <br> Then it returns the query, which is the corresponding visual element </br>
    /// </summary>
    /// <param name="userInterface"></param>
    /// <param name="toggled"></param>
    #endregion  
    
    public VisualElement GetVisualElement(UserInterfaces userInterface)
    {
        if (userInterface == UserInterfaces.None)
        {
            return null;
        }

        if (_userInterfaces.TryGetValue(userInterface, out VisualElement element))
        {
            Debug.Log("AsdASDASDA");
            return element;

        } else 
        { 

            Debug.LogError("The element " + userInterface.ToString() + " can't be found.");
            return null;
        }

        
    }
    
    private void ShowLoadingResults(VisualTreeAsset visualTreeAsset, bool showLoadingResults)
    {
        if (showLoadingResults)
        {
            Debug.Log($"Loaded UXML: {visualTreeAsset.name}");
            
        }
    }
    public async Task LoadInterfacesAsync(bool showLoadingResults)
    {
        var uxmlLabelHandle = Addressables.LoadAssetsAsync<Object>(_labelReference.labelString);

        await uxmlLabelHandle.Task;

        if (uxmlLabelHandle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedUserInterfaces = uxmlLabelHandle;

            foreach (object result in uxmlLabelHandle.Result)
            {
                if (result is VisualTreeAsset visualTree)
                {
                    ShowLoadingResults(visualTree, showLoadingResults);
                    SetUserInterfaceElementStyle(visualTree);

                } else
                {
                    Debug.LogError("The loaded asset is not a VisualTreeAsset!");
                }
                
            }
         
            test(false);

        }
        
    }
    private void test(bool active)
    {
        if (active)
        {
            foreach (var test in _userInterfacesToBeLoaded)
            {
                Debug.Log(test.ToString());
            }
        }
    }
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

        //    _addedUserInterfaceElement.name = visualTree.name;
        
        UserInterfaces userInterface = FindMatchingInterfaceType(visualTree.name);

        if (userInterface == UserInterfaces.None)
        {
            return;
        }
        else
        {
            _userInterfaces.Add(userInterface, _addedUserInterfaceElement);
        }
        _addedUserInterfaceElement.style.display = DisplayStyle.None;

        _root.Add(_addedUserInterfaceElement);
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
        Debug.Log(firstMatch);
        return firstMatch;
    }
}
    


