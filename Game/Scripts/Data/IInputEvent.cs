using UnityEditor;
using UnityEngine;

public interface IInputEvent 
{
    public InputType InputType { get; }
    public bool Enabled { get; }
    public void SetActive(bool active);

}
public enum InputType
{
    Movement,
    Camera,
}