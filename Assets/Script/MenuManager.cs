using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Button References")]
    public Button btnStart;
    public Button btnOption;
    public Button btnQuit;

    [Header("Panel References")]
    public GameObject optionPanel; 

    [Header("Scene Settings")]
    public string gameSceneName = "MapScene"; 

    void Start()
    {
        // Assign button listeners
        if (btnStart != null)
            btnStart.onClick.AddListener(StartGame);
        
        if (btnOption != null)
            btnOption.onClick.AddListener(OpenOptions);
        
        if (btnQuit != null)
            btnQuit.onClick.AddListener(QuitGame);

        
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    public void StartGame()
    {
        Debug.Log("Starting game...");
        GameState.GameStarted = true;
        SceneManager.LoadScene("NarrationScene");
    }

    public void OpenOptions()
    {
        Debug.Log("Opening options...");
        
        if (optionPanel != null)
            optionPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("Closing options...");
        
        if (optionPanel != null)
            optionPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnDestroy()
    {
        
        if (btnStart != null)
            btnStart.onClick.RemoveListener(StartGame);
        
        if (btnOption != null)
            btnOption.onClick.RemoveListener(OpenOptions);
        
        if (btnQuit != null)
            btnQuit.onClick.RemoveListener(QuitGame);
    }
}