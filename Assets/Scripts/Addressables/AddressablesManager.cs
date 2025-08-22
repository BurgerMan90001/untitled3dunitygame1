
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

//TODO IMPLEMENT FOR BETTER PERFORMANCE
// NOTES: uxml file loading is handled by UXMLFileHandler.cs
public class AddressablesManager : MonoBehaviour
{

    [SerializeField] private List<string> _labelsToBeLoaded = new List<string>();
    [SerializeField] private List<AssetLabelReference> _labelReferences;

    private List<AsyncOperationHandle<GameObject>> _handles;
    private void Awake()
    {
    }

    private void Start()
    {

    }

    private void OnEnable()
    {

    }


    private void OnDisable()
    {

    }
    private void OnDestroy()
    {

    }
    private void LoadByLabel()
    {
        foreach (string label in _labelsToBeLoaded)
        {

        }
    }
    


}
