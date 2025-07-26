using UnityEngine;

// TODO MAYBE MAKE UNIVERSAL MANAGER METHODES
/// <summary>
/// <br> For managers and such. Inherits from monobehaviour.</br>
/// <br> for now empty and for singletons </br>
/// <br> NOTES: Register events in onenable or start. unregister in ondestroy.</br>
/// </summary>
public abstract class Manager : MonoBehaviour
{
    public abstract void Initialize();
    /*
    protected void Instantiate()
    {
        var go = Instantiate(gameObject);

        //    MoveToManagers(go);
    }
    */

}