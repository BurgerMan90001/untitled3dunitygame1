

using UnityEngine;
using UnityEngine.AddressableAssets;

public static class AddressablesLoader
{

}


public static class TextLoader
{
    public static async void LoadTextFile(string textAssetName)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(textAssetName);

        await handle.Task;
    }
}