using UnityEngine;

public class RigidBodySettings : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Rigidbody playerRigidBody;
    private void Awake()
    {
        playerRigidBody.freezeRotation = true;
    }
}
