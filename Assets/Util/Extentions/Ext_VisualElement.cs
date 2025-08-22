using UnityEngine;
using UnityEngine.UIElements;

public static class VisualElementExtensions
{
    #region
    /// <summary>
    /// Sets the visual element display style to flex, making it visible.
    /// </summary>
    /// <param name="element"></param>
    #endregion
    public static void Show(this VisualElement element)
    {
        element.style.display = DisplayStyle.Flex;

    }
    #region
    /// <summary>
    /// Returns true if the VisualElement is currently displayed (i.e., its display style is set to Flex).
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    #endregion
    public static bool Shown(this VisualElement element)
    {
        return element.style.display == DisplayStyle.Flex;
    }

    #region
    /// <summary>
    /// Sets the visual element display style to none, making it hidden.
    /// </summary>
    /// <param name="element"></param>
    #endregion
    public static void Hide(this VisualElement element)
    {
        element.style.display = DisplayStyle.None;
    }
    #region
    /// <summary>
    /// Returns true if the VisualElement is currently hidden (i.e., its display style is set to None).
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    #endregion
    public static bool Hidden(this VisualElement element)
    {
        return element.style.display == DisplayStyle.None;
    }
    #region
    /// <summary>
    /// Checks if the VisualElement has a userData of type ItemInstance.
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    #endregion
    public static bool TryGetUserData<T>(this VisualElement element, out T itemInstance)
    {
        itemInstance = default;
        if (element.userData is T foundItemInstance)
        {
            itemInstance = foundItemInstance;
            return true;
        }
        return false;
    }

    public static void SetPosition(this VisualElement element, Vector3 position)
    {
        element.transform.position = position;
    }
    #region
    /// <summary>
    /// Uses a sprite to set the element's background image.
    /// </summary>
    /// <param name="element"></param>
    /// <param name="backgroundImage"></param>
    #endregion
    public static void SetBackgroundImage(this VisualElement element, Sprite backgroundImage)
    {
        element.style.backgroundImage = Background.FromSprite(backgroundImage);
    }
    public static void SetBackgroundImage(this VisualElement element, Background background)
    {
        element.style.backgroundImage = background;
    }

}