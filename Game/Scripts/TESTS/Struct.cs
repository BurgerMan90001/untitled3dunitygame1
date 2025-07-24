using UnityEngine;

public struct Struct
{
    public float x;
    public float y;
}
public class Plays : MonoBehaviour {
    public Struct Stuff;
    private void Awake()
    {
        Stuff = new Struct();
    }

}
