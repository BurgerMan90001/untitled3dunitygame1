using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private bool movementEnabled = false;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidBody;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (movementEnabled)
        {
            navMeshAgent.enabled = true;
        }

                
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
    
    private void MoveTowardsPlayer()
    {
        if (movementEnabled)
        {
            navMeshAgent.destination = playerRigidBody.position;
        }
    }
}
