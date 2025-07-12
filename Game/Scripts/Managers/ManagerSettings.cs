using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Managers/ManagerSettings")]
public class ManagerSettings : ScriptableObject 
{
    public GameObject ManagerType;
    public bool Enabled;

    
}
