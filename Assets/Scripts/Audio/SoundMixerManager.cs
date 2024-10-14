using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider soundFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;


    private void Start()
    {   
        //Load MasterVolume Player Prefs (Default = 1.0f)
        float masterVolume = PlayerPrefs.GetFloat("masterVolume", 1.0f);
        SetMasterVolume(masterVolume);
        masterVolumeSlider.value = masterVolume;

        //Load SoundFXVolume Player Prefs (Default = 0.3f)
        float soundFXVolume = PlayerPrefs.GetFloat("soundFXVolume", 0.3f);
        SetSoundFXVolume(soundFXVolume);
        soundFXVolumeSlider.value = soundFXVolume;

        //Load MusicVolume Player Prefs (Default = 0.3f)
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 0.3f);
        SetMusicVolume(musicVolume);
        musicVolumeSlider.value = musicVolume;
    }


    public void SetMasterVolume(float level)
    {
        //audioMixer.SetFloat("masterVolume", level);
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("masterVolume", level); // Saves the value
        PlayerPrefs.Save(); // Saves the Changes made to value
    }


    public void SetSoundFXVolume(float level)
    {
        //audioMixer.SetFloat("soundFXVolume", level);
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("soundFXVolume", level); // Saves the value
        PlayerPrefs.Save(); // Saves the Changes made to value
    }


    public void SetMusicVolume(float level)
    {
        //audioMixer.SetFloat("musicVolume", level);
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat("musicVolume", level); // Saves the value
        PlayerPrefs.Save(); // Saves the Changes made to value
    }
}