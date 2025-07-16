using UnityEngine;

public static class GamePause
{
    public static void PauseGame(bool pauseGame)
    {
        if (pauseGame) //then unPause game and bring up HUD
        {
            Time.timeScale = 1.0f;
            pauseGame = false; //unPause
            AudioListener.pause = true;
            
            //  auidoSource.ignoreListenerPause = true; to ignore
        }
        else // then pause game and bring up inventory
        {
            Time.timeScale = 0f;
            pauseGame = true;
            AudioListener.pause = false;
            
        }
    }
}
