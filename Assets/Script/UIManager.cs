using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject popupPanel; // Panel Popup yang berisi slider & tombol quit
    public Slider masterSlider;   // Slider volume
    public Slider musicSlider; // Slider untuk Group A
    public Slider effectSlider; // Slider untuk Group B
    public Slider dialogSlider; // Slider untuk Group B

    [Header("Scene Settings")]
    public string menuSceneName = "MenuScene";

    [Header("Audio Settings")]
    public AudioMixer AudioMixer; // Masukkan MainMixer di sini
    private bool result;

    void Start()
    {
        // 1. Pastikan Popup tertutup saat game mulai
        if (popupPanel != null)
            popupPanel.SetActive(false);

        // 2. Setup Slider agar sesuai volume saat ini
        if (masterSlider != null)
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
            masterSlider.onValueChanged.AddListener(SetLevelMaster);
        }
        if (musicSlider != null)
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
            musicSlider.onValueChanged.AddListener(SetLevelMusic);
        }
        if (effectSlider != null)
        {
            effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 1f);
            effectSlider.onValueChanged.AddListener(SetLevelEffect);
        }
        if (dialogSlider != null)
        {
            dialogSlider.value = PlayerPrefs.GetFloat("DialogVolume", 1f);
            dialogSlider.onValueChanged.AddListener(SetLevelDialog);
        }
    }

    // Fungsi untuk Mixer
    public void SetLevelMaster(float sliderValue)
    {
        AudioMixer.SetFloat("VolMaster", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetLevelMusic(float sliderValue)
    {
        AudioMixer.SetFloat("VolMusic", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetLevelEffect(float sliderValue)
    {
        AudioMixer.SetFloat("VolEffects", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
        PlayerPrefs.Save();
    }
    public void SetLevelDialog(float sliderValue)
    {
        AudioMixer.SetFloat("VolDialog", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("DialogVolume", sliderValue);
        PlayerPrefs.Save();
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
    // 5. Dipasang di BackButton (Lobby)
    public void BackButton()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); // Tutup panel
            Time.timeScale = 1f;         // Pastikan game jalan
        }
    }

}

