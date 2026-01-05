using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    [Header("UI References")]
    public GameObject gameOverPanel; 

    void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerGameOver()
    {
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        
        Time.timeScale = 0f; 
    }

    
    public void RetryGame()
    {
        
        Time.timeScale = 1f;

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
    public void ExitGame()
    {
        Debug.Log("Keluar dari Game...");
        Application.Quit();
    }
}