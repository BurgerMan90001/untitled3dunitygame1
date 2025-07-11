using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DebugInput : MonoBehaviour
{
    public PlayerInput playerInput;
    public GameObject prefab; 
   // public SO_Stats stats;
    private Vector3 spawnPoint = new Vector3(-20.49013f, 71f, -32.76805f);

    private double _buttonDurationThreshold = 0.30d; // Threshold for button hold duration
    private bool buttonHeld = false;
    private bool show = false;
    public void DropItem(InputAction.CallbackContext ctx) //int itemIndex
    {
        if (ctx.started)
        {
            buttonHeld = true;
        }
        else if (ctx.canceled)
        {
            buttonHeld = false;
        }
        
        //    GameObject droppedItem = new GameObject();// Creates a new object and gives it the item data
        //    droppedItem.AddComponent<Rigidbody>();
        //    droppedItem.AddComponent<InstanceItemContainer>();

        // Removes the item from the inventory
     //   inventory.items.RemoveAt(itemIndex);

        // Updates the inventory again
    //    UpdateInventory();
    }

    public void Debug2(InputAction.CallbackContext ctx)
    {
        if (show)
        {

        } else
        {

        }
    }
    private void Update()
    {
        if (buttonHeld)
        {
            GameObject spawnedObject = Instantiate(prefab, spawnPoint, Quaternion.identity);
            
            
        }
    }
    
}
