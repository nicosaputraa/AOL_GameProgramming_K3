using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

    public void LoadScene()
    {
        Debug.Log("Button ditekan!");
        SceneManager.LoadScene(sceneName);
    }
}
