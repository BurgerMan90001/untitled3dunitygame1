using UnityEngine;
public static class TransformExtentions
{
    public static void RotateX(this Transform transform, float x)
    {
        transform.rotation = Quaternion.Euler(x, transform.rotation.y, transform.rotation.z);

    }
    public static void RotateY(this Transform transform, float y)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);
    }
    public static void RotateZ(this Transform transform, float z)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, z);
    }
}