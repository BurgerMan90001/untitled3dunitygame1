using UnityEngine;

[System.Serializable]
[CreateAssetMenu (menuName = "Time/GameTimeData")]
public class GameTimeData : ScriptableObject
{

    public Day Day;
    public float Time;
}

public enum Day 
{
    Monday, 
    Tuesday, 
    Wednesday, 
    Thursday, 
    Friday, 
    Saturday, 
    Sunday,
}

