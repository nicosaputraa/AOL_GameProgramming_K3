using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseMemoryScene : MonoBehaviour
{
    public string sceneName = "MemoryScene1";
    public string sceneName2 = "MemoryScene2";
    public string sceneName3 = "MemoryScene3";

    public void CloseMemory()
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public void CloseMemory2()
    {
        SceneManager.UnloadSceneAsync(sceneName2);
    }

    public void CloseMemory3()
    {
        SceneManager.UnloadSceneAsync(sceneName3);
    }
}
