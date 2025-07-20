
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using System;
//TODO MAYBE OPTIMIZE INVENTORY UPDATES TO ONLY UPDATE CHANGED ELEMENTS
// TODO MAYBE USE BINDINGS TO OPTIMIZE

public class UI_Inventory : IUserInterface // animation and stuff
{

    private Inventory _inventory;

    private List<VisualElement> _itemVisualElements;

    private DragAndDropManipulator _dragAndDropManipulator;
    private TooltipManipulator _tooltipManipulator;


    private VisualElement _inventoryBackingPanel;

    private VisualElement _ghostImage; // the ghost image that will be used to show the item being dragged

    public UI_Inventory(Inventory inventory)
    {
        _inventory = inventory; // the dynamic inventory that this UI_Inventory will use

        _itemVisualElements = new List<VisualElement>();

    }

    public void QueryElements(VisualElement root)
    {
        _inventoryBackingPanel = root.Q<VisualElement>("Panel_Inventory");
        _ghostImage = root.Q<VisualElement>("GhostImage");
    }
    
    public void Register(VisualElement root)
    {

        
        for (int i = 0; i < _inventoryBackingPanel.childCount; i++)
        {

            var child = _inventoryBackingPanel[i];

            _itemVisualElements.Add(child);

            _dragAndDropManipulator = new DragAndDropManipulator(child, _ghostImage, _inventoryBackingPanel, root, _inventory);
            _tooltipManipulator = new TooltipManipulator(child,root); // tooltip manipulator will be used to show the tooltip
           
            
            child.AddManipulator(_tooltipManipulator);
            child.AddManipulator(_dragAndDropManipulator);

            if (i < _inventory.Items.Count && _inventory.Items[i] != null) // set the user data
            {
                child.userData = _inventory.Items[i]; // Store directly in the element
            }


        }
        

    }
    
    public void Unregister()
    {


        for (int i = 0; i < _inventoryBackingPanel.childCount; i++)
        {
            
            var child = _inventoryBackingPanel[i];
            if (child == null) continue;

            child?.RemoveManipulator(_tooltipManipulator);
            child?.RemoveManipulator(_dragAndDropManipulator);
        }


    }
    
    #region
    /// <summary>
    /// Updates the ItemSlots dictionary with the current items in the dynamic inventory. 
    /// </summary>
    #endregion
    public void UpdateInterface() // O(n)
    {
        
        for (int i = 0; i < _inventory.Items.Count; i++)
        {
            VisualElement child = _itemVisualElements[i];
            
            ItemInstance itemInstance = _inventory.Items[i]; // get the item instance from the dynamic inventory

            if (child == null || itemInstance == null)
            {
                Debug.LogWarning("A child or itemInstance is null");
                continue; // skip if the child is null
                
            }
            child.userData = itemInstance;
    
            child.style.backgroundImage = Background.FromSprite(itemInstance.Icon);

        }
        
    }

    
    
}









/// <summary>
/// <br> AI STUFF</br>
/// </summary>
/*
#region
public class AddressableTextureLoader
{
    private static Dictionary<string, Texture2D> _cache = new Dictionary<string, Texture2D>();
    private static Dictionary<string, AsyncOperationHandle<Texture2D>> _loadingOperations = new Dictionary<string, AsyncOperationHandle<Texture2D>>();

    /// <summary>
    /// Loads a Texture2D from Addressables with caching support
    /// </summary>
    /// <param name="addressableKey">The addressable key for the texture</param>
    /// <param name="useCache">Whether to use caching</param>
    /// <returns>The loaded Texture2D or null if failed</returns>
    public static async Task<Texture2D> LoadAsync(string addressableKey, bool useCache = true)
    {
        if (string.IsNullOrEmpty(addressableKey))
            return null;

        // Check cache first
        if (useCache && _cache.ContainsKey(addressableKey))
            return _cache[addressableKey];

        // Check if already loading
        if (_loadingOperations.ContainsKey(addressableKey))
        {
            await _loadingOperations[addressableKey].Task;
            return _loadingOperations[addressableKey].Result;
        }

        // Start loading
        var handle = Addressables.LoadAssetAsync<Texture2D>(addressableKey);
        _loadingOperations[addressableKey] = handle;

        try
        {
            var texture = await handle.Task;

            if (useCache && texture != null)
                _cache[addressableKey] = texture;

            return texture;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load addressable texture '{addressableKey}': {ex.Message}");
            return null;
        }
        finally
        {
            _loadingOperations.Remove(addressableKey);
        }
    }

    /// <summary>
    /// Loads multiple textures concurrently
    /// </summary>
    /// <param name="addressableKeys">List of addressable keys to load</param>
    /// <param name="useCache">Whether to cache the loaded textures</param>
    /// <returns>Dictionary of key-texture pairs</returns>
    public static async Task<Dictionary<string, Texture2D>> LoadMultipleAsync(List<string> addressableKeys, bool useCache = true)
    {
        var tasks = new List<Task<Texture2D>>();
        var result = new Dictionary<string, Texture2D>();

        foreach (var key in addressableKeys)
        {
            tasks.Add(LoadAsync(key, useCache));
        }

        var textures = await Task.WhenAll(tasks);

        for (int i = 0; i < addressableKeys.Count; i++)
        {
            if (textures[i] != null)
            {
                result[addressableKeys[i]] = textures[i];
            }
        }

        return result;
    }

    /// <summary>
    /// Loads textures by label/category
    /// </summary>
    /// <param name="label">Addressable label to load</param>
    /// <param name="useCache">Whether to cache loaded textures</param>
    /// <returns>List of loaded textures</returns>
    public static async Task<List<Texture2D>> LoadByLabelAsync(string label, bool useCache = true)
    {
        try
        {
            var handle = Addressables.LoadAssetsAsync<Texture2D>(label, null);
            var textures = await handle.Task;

            if (useCache)
            {
                // Cache by resource location key if possible
                foreach (var texture in textures)
                {
                    if (texture != null && !string.IsNullOrEmpty(texture.name))
                    {
                        _cache[texture.name] = texture;
                    }
                }
            }

            return new List<Texture2D>(textures);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to load textures by label '{label}': {ex.Message}");
            return new List<Texture2D>();
        }
    }

    /// <summary>
    /// Preloads textures for faster access later
    /// </summary>
    /// <param name="addressableKeys">Keys to preload</param>
    /// <param name="useCache">Whether to cache preloaded textures</param>
    public static async Task PreloadAsync(List<string> addressableKeys, bool useCache = true)
    {
        await LoadMultipleAsync(addressableKeys, useCache);
    }

    /// <summary>
    /// Preloads textures by label for faster access later
    /// </summary>
    /// <param name="label">Label to preload</param>
    /// <param name="useCache">Whether to cache preloaded textures</param>
    public static async Task PreloadByLabelAsync(string label, bool useCache = true)
    {
        await LoadByLabelAsync(label, useCache);
    }

    /// <summary>
    /// Gets a texture from cache without loading
    /// </summary>
    /// <param name="addressableKey">The key to get from cache</param>
    /// <returns>Cached texture or null if not found</returns>
    public static Texture2D GetFromCache(string addressableKey)
    {
        return _cache.TryGetValue(addressableKey, out var texture) ? texture : null;
    }

    /// <summary>
    /// Checks if a texture is cached
    /// </summary>
    /// <param name="addressableKey">The addressable key to check</param>
    /// <returns>True if cached, false otherwise</returns>
    public static bool IsCached(string addressableKey)
    {
        return _cache.ContainsKey(addressableKey);
    }

    /// <summary>
    /// Removes a specific texture from cache and releases it
    /// </summary>
    /// <param name="addressableKey">The key to remove from cache</param>
    public static void RemoveFromCache(string addressableKey)
    {
        if (_cache.TryGetValue(addressableKey, out var texture))
        {
            if (texture != null)
            {
                Addressables.Release(texture);
            }
            _cache.Remove(addressableKey);
        }
    }

    /// <summary>
    /// Clears the entire texture cache and releases all resources
    /// </summary>
    public static void ClearCache()
    {
        foreach (var kvp in _cache)
        {
            if (kvp.Value != null)
            {
                Addressables.Release(kvp.Value);
            }
        }
        _cache.Clear();
    }

    /// <summary>
    /// Gets all cached texture keys
    /// </summary>
    /// <returns>List of cached keys</returns>
    public static List<string> GetCachedKeys()
    {
        return new List<string>(_cache.Keys);
    }

    /// <summary>
    /// Gets cache statistics
    /// </summary>
    /// <returns>Number of cached textures</returns>
    public static int GetCacheCount()
    {
        return _cache.Count;
    }

    /// <summary>
    /// Releases a specific texture handle
    /// </summary>
    /// <param name="texture">Texture to release</param>
    public static void Release(Texture2D texture)
    {
        if (texture != null)
        {
            Addressables.Release(texture);
        }
    }
}

// Extension methods for common use cases
public static class AddressableTextureExtensions
{
    /// <summary>
    /// Sets Visual Element background from addressable texture
    /// </summary>
    public static async Task<bool> SetBackgroundFromAddressableAsync(this VisualElement element, string addressableKey, bool useCache = true)
    {
        if (element == null) return false;

        var texture = await AddressableTextureLoader.LoadAsync(addressableKey, useCache);
        if (texture != null)
        {
            element.style.backgroundImage = new StyleBackground(texture);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets SpriteRenderer sprite from addressable texture
    /// </summary>
    public static async Task<bool> SetSpriteFromAddressableAsync(this SpriteRenderer spriteRenderer, string addressableKey, bool useCache = true)
    {
        if (spriteRenderer == null) return false;

        var texture = await AddressableTextureLoader.LoadAsync(addressableKey, useCache);
        if (texture != null)
        {
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            spriteRenderer.sprite = sprite;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets UI Image sprite from addressable texture
    /// </summary>
    public static async Task<bool> SetImageFromAddressableAsync(this UnityEngine.UI.Image image, string addressableKey, bool useCache = true)
    {
        if (image == null) return false;

        var texture = await AddressableTextureLoader.LoadAsync(addressableKey, useCache);
        if (texture != null)
        {
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            image.sprite = sprite;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets RawImage texture from addressable
    /// </summary>
    public static async Task<bool> SetRawImageFromAddressableAsync(this UnityEngine.UI.RawImage rawImage, string addressableKey, bool useCache = true)
    {
        if (rawImage == null) return false;

        var texture = await AddressableTextureLoader.LoadAsync(addressableKey, useCache);
        if (texture != null)
        {
            rawImage.texture = texture;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets Material main texture from addressable
    /// </summary>
    public static async Task<bool> SetMaterialTextureFromAddressableAsync(this Material material, string addressableKey, bool useCache = true, string propertyName = "_MainTex")
    {
        if (material == null) return false;

        var texture = await AddressableTextureLoader.LoadAsync(addressableKey, useCache);
        if (texture != null)
        {
            material.SetTexture(propertyName, texture);
            return true;
        }
        return false;
    }
}
#endregion
*/