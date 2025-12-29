using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject popupPanel; // Panel Popup yang berisi slider & tombol quit
    public Slider volumeSlider;   // Slider volume

    [Header("Scene Settings")]
    public string menuSceneName = "MenuScene";

    void Start()
    {
        // 1. Pastikan Popup tertutup saat game mulai
        if (popupPanel != null)
            popupPanel.SetActive(false);

        // 2. Setup Slider agar sesuai volume saat ini
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // --- FUNGSI UNTUK TOMBOL ---

    // 1. Dipasang di 'Btn_OpenMenu'
    public void OpenMenu()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true); // Munculkan Popup
            Time.timeScale = 0f;        // BEKU-kan waktu (Pause Game)
        }
    }

    // 2. Dipasang di 'Btn_Resume' (Di dalam Popup)
    public void ResumeGame()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); // Sembunyikan Popup
            Time.timeScale = 1f;         // JALAN-kan waktu kembali (Unpause)
        }
    }

    // 3. Dipasang di 'Btn_Quit' (Di dalam Popup)
    public void QuitToMainMenu()
    {
        // PENTING: Kembalikan waktu ke normal sebelum pindah scene
        // Kalau tidak, scene Menu nanti akan ikut beku (freeze)
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene(menuSceneName);
    }

    // 4. Logika Slider Volume
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}