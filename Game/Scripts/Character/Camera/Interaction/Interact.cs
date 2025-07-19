
using UnityEngine;

// TODO OPTIMIZE TAG CHECKING AND suthf


#region
/// <summary>
/// Provides functionality for interacting with game objects in the scene, such as NPCs, doors, and ladders.
/// </summary>
/// <remarks>The <see cref="Interact"/> class enables interaction with game objects by detecting hits using
/// raycasting and invoking specific behaviors based on the type of object hit.</remarks>
#endregion
public class Interact
{
    
    private HitDetect _hitDetect;

    private Inventory _inventory;

    private LayerMask _mask;
    public bool ButtonHeld { get; private set; }

    
    public Interact(HitDetect hitDetect, Inventory Inventory, LayerMask mask)
    {
        _hitDetect = hitDetect;
        _inventory = Inventory;
        _mask = mask;

    }
    
    public void StartInteract(float interactDistance, bool showDebugRayCast)
    {
        ButtonHeld = true;

        _hitDetect.ShootRayCastFromCamera(showDebugRayCast, interactDistance, _mask);

        if (!_hitDetect.HitSomething) return; // if it didn't hit anything do nothing

        GameObject hitGameObject = _hitDetect.HitInfo.transform.gameObject;

        FindObjectType(hitGameObject); 

    }
    public void StartPickup()
    {

    }
    private void FindObjectType(GameObject hitGameObject)
    {
        if (hitGameObject.CompareTag("NPC"))
        {
            
            NPCInteraction(hitGameObject); 
        } 
        else if (hitGameObject.CompareTag("Shop"))
        {
            ShopInteraction(hitGameObject);
        }
        else if (hitGameObject.CompareTag("Card"))
        {
            CardInteraction(hitGameObject);
        }
        else if (hitGameObject.CompareTag("Door"))
        {
            DoorInteraction(hitGameObject);
        }
        else if (hitGameObject.CompareTag("Ladder"))
        {
            Debug.Log("HI IM LADDER");

        }
        else
        {
            // hit nothing
        }
    }
    #region //interactions with game objects
    private void NPCInteraction(GameObject hitGameObject)
    {
        if (hitGameObject.TryGetComponent(out NPCInteraction component))
        {
            component.NPCDialogue();
        }
        else
        {
            Debug.LogError("This NPC does not have an NPC Dialogue script attached to them");
        }
    }
    private void ShopInteraction(GameObject hitGameObject)
    {
        if (hitGameObject.TryGetComponent(out NPCShop component))
        {
            component.ShopInteraction();
        }
        else
        {
            Debug.LogError("This NPC does not have an NPC Dialogue script attached to them");
        }
    }
    private void CardInteraction(GameObject hitGameObject)
    {

        if (hitGameObject.TryGetComponent(out ItemInstanceContainer component))
        {
            
            ItemInstance item = component.TakeItem();
            
            if (_inventory.AddItem(item)) // if the item was added to the inventory successfully
            {
                component.DestroyGameObject(); // destroy the game object after taking the item
            }
            
        }
        else
        {
            Debug.LogError("This card does not have an ItemInstanceContainer script attached to it.");
        }
    }
    private void DoorInteraction(GameObject gameObject)
    {
        
        Debug.Log("Interacting with door: " + gameObject.name);
    }
    #endregion
    public void CancelInteract()
    {
        ButtonHeld = false;
    }

}

public enum IInteractable
{

}

public class Interactable 

{ 

}
