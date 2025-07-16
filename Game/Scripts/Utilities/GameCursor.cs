using UnityEngine;
/// <summary>
/// <br> A class to handle cursor states. </br>
/// </summary>
public static class GameCursor
{

    private static bool _disableLock = true;
    public static CursorLockMode LockMode => Cursor.lockState = CursorLockMode.Locked;
    /// <summary>
    /// <br> Locks the cursor into the middle and hides it.</br>
    /// </summary>
    public static void Lock()
    {
        if (_disableLock) { return; }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    /// <summary>
    /// <br> Unlocks the cursor and shows it.</br>
    /// </summary>
    public static void Unlock() 
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /// <summary>
    /// <br> Can control and set the state of the cursor.</br>
    /// </summary>
    /// <param name="lockMode"></param>
    /// <param name="visible"></param>
    public static void SetState(CursorLockMode lockMode, bool visible)
    {
        Cursor.lockState = lockMode;
        Cursor.visible = visible;
    }

    /// <summary>
    /// Optional: Confined mode for windowed games
    /// </summary>
    public static void Confine()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
