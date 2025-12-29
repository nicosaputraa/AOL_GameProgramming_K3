using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupLoader
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void LoadMenu()
    {
        Debug.Log("StartupLoader running | GameStarted = " + GameState.GameStarted);

        if (GameState.GameStarted) return;

        if (SceneManager.GetActiveScene().name != "MenuScene")
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
}
