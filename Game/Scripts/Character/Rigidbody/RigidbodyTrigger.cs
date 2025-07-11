using UnityEngine;

public class RigidbodyTrigger
{
    private MovementStateManager _movementStateManager;
    private Rigidbody _rigidBody;

    public RigidbodyTrigger(Rigidbody rigidBody, MovementStateManager movementStateManager)
    {
        _rigidBody = rigidBody;
        _movementStateManager = movementStateManager;
    }

    public void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {
           
        }
    }
    public void OnTriggerEnter(Collider collider)
    { // ladder

        if (collider.gameObject.tag == "Ladder")
        {

            _movementStateManager.SetMovementState(MovementStates.Climbing);

        } 
        
    }
    public void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Ladder")
        {

            _movementStateManager.SetMovementState(MovementStates.Walking);
            
        }
    }
    
}
