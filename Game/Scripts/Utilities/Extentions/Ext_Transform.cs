using UnityEngine;

public static class TransformExtensions
{
    #region
    /// <summary>
    /// Destroys all children of the given transform.
    /// </summary>
    /// <param name="transform"></param>
    #endregion
    public static void DestroyChildren(this Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }
    #region
    /// <summary>
    /// Sets ition to zero and  rotation to identity.
    /// </summary>
    /// <param name="transform"></param>
    #endregion
    public static void ResetTransform(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        //   transform.localScale = Vector3.one;
    }
}
