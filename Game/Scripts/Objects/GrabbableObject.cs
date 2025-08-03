using UnityEngine;

/* 
    First Person Interaction Toolkit by Steven Harmon stevenharmongames.com
    Licensed under the MPL 2.0. https://www.mozilla.org/en-US/MPL/2.0/FAQ/
    Please use in your walking sims/horror/adventure/puzzle games! Drop me a line and share what make with it! :)    
 */


public class GrabbableObject : MonoBehaviour
{
    public bool Held { get; set; }


    public void CancelHold()
    {
        throw new System.NotImplementedException();
    }
    private void Update()
    {
        if (!Held) return; // if the game object is not held, do nothing


    }

    public void StartHold(Transform positionHeldAt)
    {
        throw new System.NotImplementedException();
    }
}
