using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ada untuk Reload Scene

public class GameManager : MonoBehaviour
{
    // Singleton agar mudah dipanggil dari script PlayerStats
    public static GameManager instance;

    [Header("UI References")]
    public GameObject gameOverPanel; // Masukkan Panel Game Over di sini

    void Awake()
    {
        // Setup Singleton
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
        // 1. Munculkan layar Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // 2. Hentikan waktu (Game Pause otomatis)
        Time.timeScale = 0f; 
    }

    // Fungsi untuk tombol RETRY (Mengulang dari awal)
    public void RetryGame()
    {
        // PENTING: Kembalikan waktu ke normal sebelum reload scene
        Time.timeScale = 1f;

        // Reload scene yang sedang aktif (Reset total)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Fungsi untuk tombol EXIT
    public void ExitGame()
    {
        Debug.Log("Keluar dari Game...");
        Application.Quit();
    }
}