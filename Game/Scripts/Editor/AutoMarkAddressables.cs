using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.IO;


// AI
public static class AutoMarkAllAddressables
{

    private static readonly string[] ValidExtensions = new[]
    {
        ".uxml", ".uss", ".prefab", ".png", ".jpg", ".mp3", ".wav", ".asset", ".mat", ".fbx", ".shader", ".txt"
    };

    [MenuItem("Tools/Addressables/Mark All Assets In Assets Folder")]
    public static void MarkAllAssets()
    {
        string targetFolder = "Assets/myprojectV1/Game"; // Change to any folder
        string groupName = "GeneralAssets"; // Create or reuse this Addressables Group

        var settings = AddressableAssetSettingsDefaultObject.Settings;
        var group = settings.FindGroup(groupName) ?? settings.CreateGroup(groupName, false, false, false, null, typeof(BundledAssetGroupSchema));

        string[] files = Directory.GetFiles(targetFolder, "*.*", SearchOption.AllDirectories)
                                  .Where(f => ValidExtensions.Contains(Path.GetExtension(f).ToLower()))
                                  .ToArray();

        int count = 0;

        foreach (string path in files)
        {
            string assetPath = path.Replace('\\', '/');
            string guid = AssetDatabase.AssetPathToGUID(assetPath);

            if (string.IsNullOrEmpty(guid)) continue;

            var entry = settings.CreateOrMoveEntry(guid, group);

            // Create a clean address, e.g., "UI/MainMenu" instead of "Assets/UI/MainMenu.uxml"
            string address = Path.ChangeExtension(assetPath.Replace("Assets/", ""), null);
            entry.address = address;

            // Add label based on file extension
            AddLabelForEntry(settings, entry, Path.GetExtension(assetPath));

            count++;
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"[Addressables] Marked {count} assets under '{targetFolder}' into group '{groupName}'.");
    }

    private static void AddLabelForEntry(AddressableAssetSettings settings, AddressableAssetEntry entry, string extension)
    {
        if (string.IsNullOrEmpty(extension)) return;

        string label = extension.TrimStart('.').ToLower(); // e.g., "uxml", "png"
        if (!settings.GetLabels().Contains(label))
        {
            settings.AddLabel(label);
        }
        entry.SetLabel(label, true);
    }
}
#endif

