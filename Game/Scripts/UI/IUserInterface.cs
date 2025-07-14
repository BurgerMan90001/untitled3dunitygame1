using UnityEngine;
using UnityEngine.UIElements;

public interface IUserInterface
{
    #region
    //   VisualElement Root { get; protected set; }
    /// <summary>
    /// <br> Register events like clicking. </br>
    /// <br> Usually called in OnEnable. </br>
    /// </summary>
    /// <param name="root"></param>
    #endregion
    void Register(VisualElement root);
    #region
    /// <summary>
    /// <br> Unregister events like clicking. </br>
    /// <br> Usually called in OnDisable. </br>
    /// </summary>
    /// <param name="root"></param>
    #endregion
    void Unregister();

    void QueryElements(VisualElement root);
}