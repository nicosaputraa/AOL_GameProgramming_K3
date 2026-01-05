using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Components")]
    public AudioMixer AudioMixer;
    public Slider MusicSlider;
    public Slider EffectSlider;
    public Slider DialogSlider;
    public Slider MasterSlider;

    [Header("Keys")]
    
    private const string MIXER_MASTER = "VolMaster";
    private const string MIXER_MUSIC = "VolMusic";
    private const string MIXER_EFFECT = "VolEffect";
    private const string MIXER_DIALOG = "VolDialog";
    
    private const string PREF_MASTER = "MasterVolume";
    private const string PREF_MUSIC = "MusicVolume";
    private const string PREF_EFFECT = "EffectVolume";
    private const string PREF_DIALOG = "DialogVolume";

    private void Start()
    {
        
        LoadAndApplyVolumes();
    }

    private void LoadAndApplyVolumes()
    {
        
        float masterVol = PlayerPrefs.GetFloat(PREF_MASTER, 1f);
        if (MasterSlider != null)
        {
            MasterSlider.value = masterVol;
            MasterSlider.onValueChanged.AddListener(SetMasterVolume);
        }
        AudioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(masterVol) * 20);

        
        float musicVol = PlayerPrefs.GetFloat(PREF_MUSIC, 1f);
        if (MusicSlider != null)
        {
            MusicSlider.value = musicVol;
            MusicSlider.onValueChanged.AddListener(SetMusicVolume);
        }
        AudioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(musicVol) * 20);

        
        float effectVol = PlayerPrefs.GetFloat(PREF_EFFECT, 1f);
        if (EffectSlider != null)
        {
            EffectSlider.value = effectVol;
            EffectSlider.onValueChanged.AddListener(SetEffectVolume);
        }
        AudioMixer.SetFloat(MIXER_EFFECT, Mathf.Log10(effectVol) * 20);

        
        float dialogVol = PlayerPrefs.GetFloat(PREF_DIALOG, 1f);
        if (DialogSlider != null)
        {
            DialogSlider.value = dialogVol;
            DialogSlider.onValueChanged.AddListener(SetDialogVolume);
        }
        AudioMixer.SetFloat(MIXER_DIALOG, Mathf.Log10(dialogVol) * 20);
    }

    public void SetMasterVolume(float value)
    {
        AudioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_MASTER, value);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        AudioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_MUSIC, value);
        PlayerPrefs.Save();
    }

    public void SetEffectVolume(float value)
    {
        AudioMixer.SetFloat(MIXER_EFFECT, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_EFFECT, value);
        PlayerPrefs.Save();
    }

    public void SetDialogVolume(float value)
    {
        AudioMixer.SetFloat(MIXER_DIALOG, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_DIALOG, value);
        PlayerPrefs.Save();
    }
}