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
    public GameObject optionPanel; // Optional: jika ada panel option

    [Header("Scene Settings")]
    public string gameSceneName = "MapScene"; // ✅ Nama scene game Anda

    void Start()
    {
        // Assign button listeners
        if (btnStart != null)
            btnStart.onClick.AddListener(StartGame);
        
        if (btnOption != null)
            btnOption.onClick.AddListener(OpenOptions);
        
        if (btnQuit != null)
            btnQuit.onClick.AddListener(QuitGame);

        // Hide option panel at start (if exists)
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
        // Show option panel
        if (optionPanel != null)
            optionPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        Debug.Log("Closing options...");
        // Hide option panel
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
        // Remove listeners saat object dihancurkan
        if (btnStart != null)
            btnStart.onClick.RemoveListener(StartGame);
        
        if (btnOption != null)
            btnOption.onClick.RemoveListener(OpenOptions);
        
        if (btnQuit != null)
            btnQuit.onClick.RemoveListener(QuitGame);
    }
}