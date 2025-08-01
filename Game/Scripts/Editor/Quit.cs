using UnityEditor;
using UnityEngine;

/* 

    First Person Interaction Toolkit by Steven Harmon stevenharmongames.com
    Licensed under the MPL 2.0. https://www.mozilla.org/en-US/MPL/2.0/FAQ/
    Please use in your walking sims/horror/adventure/puzzle games! Drop me a line and share what make with it! :)    

 */

public class Quit : MonoBehaviour
{
    private bool fullscreen = true;
    /*

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            #if UNITY_EDITOR
                        if (EditorApplication.isPlaying)
                        {
                            EditorApplication.isPlaying = false;
                        }
            #else
                                Application.Quit();
            #endif
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }
    */
    /// <summary>
    /// Quit application when the escape key is pressed
    /// </summary>
    public void OnEscape()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
#else
        Application.Quit();
#endif
    }
    /// <summary>
    /// Toggle fullscreen when the f11 key is pressed
    /// </summary>
    public void OnF11()
    {
        fullscreen = !fullscreen;
        if (fullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}

