using UnityEngine;


// UNUSED MAYBE
public class Elevator : MonoBehaviour
{
    private bool movingDown = false;
    private bool movingUp = false;
    public float minHeight = 0.025f;
    public float maxHeight = 8.723f;

    public void CallUp()
    {
        movingUp = true;
        movingDown = false;
    }

    public void CallDown()
    {
        movingUp = false;
        movingDown = true;
    }
    private void MoveElevator()
    {

        if (movingDown)
        {
            if (transform.position.y >= minHeight)
            {
                Vector3 tempPos = new Vector3(transform.position.x, transform.position.y - Time.deltaTime, transform.position.z);
                transform.position = tempPos;
            }
        }
        if (movingUp)
        {
            if (transform.position.y <= maxHeight)
            {
                Vector3 tempPos = new Vector3(transform.position.x, transform.position.y + Time.deltaTime, transform.position.z);
                transform.position = tempPos;
            }
        }
    }
    private void FixedUpdate()
    {
        MoveElevator();
    }
}