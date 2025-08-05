using UnityEngine;

public static class RigidbodyExtentions
{
    public static float DepthThreshhold = 1f; // Threshold for depth to apply buoyancy force
    public static void NormalizeVelocity(this Rigidbody rigidBody, float magnitude = 1)
    {
        rigidBody.linearVelocity = rigidBody.linearVelocity.normalized * magnitude;
    }
    #region
    /// <summary>
    /// Does not set the velocity if the rigidbody's linear velocity is already equal to the one we want to set.
    /// </summary>
    /// <param name="rigidBody"></param>
    /// <param name="linearVelocity"></param>
    #endregion
    public static void SetVelocity(this Rigidbody rigidBody, Vector3 linearVelocity)
    {
        if (rigidBody.linearVelocity != linearVelocity) // if the rididbody's linear velocity is not equal to the one we want to set, then apply the linear velocity
        {
            rigidBody.linearVelocity = linearVelocity;
        }

    }
    #region
    /// <summary>
    /// Does not set the linear damping if the rigidbody's linear damping is already equal to the one we want to set.
    /// </summary>
    /// <param name="rigidBody"></param>
    /// <param name="linearDamping"></param>
    #endregion
    public static void SetLinearDamping(this Rigidbody rigidBody, float linearDamping)
    {

        if (rigidBody.linearDamping != linearDamping) // if the rididbody's linear damping is not equal to the one we want to set, then apply the linear damping
        {
            //    Debug.Log($"{rigidBody.linearDamping}, {linearDamping}");
            rigidBody.linearDamping = linearDamping;
        }

    }
    #region
    /// <summary>
    /// Tries to add a buoyancy force to the rigid body based on the depth of the object in the fluid.
    /// </summary>
    /// <param name="rigidBody"></param>
    /// <param name="depth"></param>
    /// <param name="fluidDensity"></param>
    /// <param name="buoyantForce"></param>
    /// <returns></returns>
    /// 
    #endregion
    public static bool TryAddBuoyancyForce(this Rigidbody rigidBody, float depth, float fluidDensity, out Vector3 buoyantForce)
    {
        buoyantForce = Vector3.zero;

        if (depth < DepthThreshhold) // if the object is submurged deep enough
        {
            buoyantForce = fluidDensity * depth * Physics.gravity;
            rigidBody.AddForce(buoyantForce, ForceMode.Force);

            return true; // can add buoyantForce
        }
        return false; // can't add buoyantForce because the object is not submerged deep enough
    }
    #region
    /// <summary>
    /// Draws a debug ray from the rigid body's gameobject position in the specified direction with the specified color.
    /// </summary>
    /// <param name="rigidBody"></param>
    /// <param name="direction"></param>
    /// <param name="color"></param>
    #endregion
    public static void DrawRay(this Rigidbody rigidBody, Vector3 direction, Color color)
    {
        Debug.DrawRay(rigidBody.gameObject.transform.position, direction, Color.red);

    }
}
