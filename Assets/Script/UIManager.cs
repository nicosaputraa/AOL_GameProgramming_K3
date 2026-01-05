using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject popupPanel; 
    public Slider masterSlider;   
    public Slider musicSlider; 
    public Slider effectSlider; 
    public Slider dialogSlider; 

    [Header("Scene Settings")]
    public string menuSceneName = "MenuScene";

    [Header("Audio Settings")]
    public AudioMixer AudioMixer; 
    private bool result;

    void Start()
    {
        
        if (popupPanel != null)
            popupPanel.SetActive(false);

        
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
    

    
    public void OpenMenu()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(true); 
            Time.timeScale = 0f;        
        }
    }

    
    public void ResumeGame()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); 
            Time.timeScale = 1f;         
        }
    }

    
    public void QuitToMainMenu()
    {
        
        
        Time.timeScale = 1f; 
        
        SceneManager.LoadScene(menuSceneName);
    }

    
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
    
    public void BackButton()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false); 
            Time.timeScale = 1f;         
        }
    }

}

