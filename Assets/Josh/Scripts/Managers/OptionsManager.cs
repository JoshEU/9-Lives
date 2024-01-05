using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    public static float soundVolume;
    public static float musicVolume;
    // An array of all audio sources that need to be added by hand
    [SerializeField]
    private AudioSource[] allAudioClipArray;
    // An array of all Sound audio sources that need to be added by hand
    [SerializeField]
    private AudioSource[] soundAudioClipArray;
    // An array of all Music audio sources that need to be added by hand
    [SerializeField]
    private AudioSource[] musicAudioClipArray;
    [SerializeField]
    private Slider masterVolumeSlider;
    [SerializeField]
    private Slider soundVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private TextMeshProUGUI masterVolumeValueText;
    [SerializeField]
    private TextMeshProUGUI soundVolumeValueText;
    [SerializeField]
    private TextMeshProUGUI musicVolumeValueText;

    void Start() {
        // Checks to see if MasterVolume has not been previously saved in Player Preferences
        // If true, then a default value is set to 0.50
        // If false, then it will retrieve the most recent value in Player preferences
        if (!PlayerPrefs.HasKey("MasterVolume")) {
            // Sets the deafault MasterVolume to 50% if there is no previously saved volume data.
            PlayerPrefs.SetFloat("MasterVolume", 0.50f);
            masterVolumeSlider.value = 0.50f;
            SetMasterVolume();
        } else {
            GetMasterVolume();
        }
        // Checks to see if SoundVolume has not been previously saved in Player Preferences
        // If true, then a default value is set to 0.30
        // If false, then it will retrieve the most recent value in Player preferences
        if (!PlayerPrefs.HasKey("SoundVolume")) {
            // Sets the deafault SoundVolume to 30% if there is no previously saved volume data.
            PlayerPrefs.SetFloat("SoundVolume", 0.30f);
            soundVolumeSlider.value = 0.30f;
            SetSoundVolume();
        } else {
            GetSoundVolume();
        }
        // Checks to see if MusicVolume has not been previously saved in Player Preferences
        // If true, then a default value is set to 0.30
        // If false, then it will retrieve the most recent value in Player preferences
        if (!PlayerPrefs.HasKey("MusicVolume")) {
            // Sets the deafault MusicVolume to 30% if there is no previously saved volume data.
            PlayerPrefs.SetFloat("MusicVolume", 0.30f);
            musicVolumeSlider.value = 0.30f;
            SetMusicVolume();
        } else {
            GetMusicVolume();
        }
    }
    // Master Volume Functions:
    // This function will load the MasterVolume from Players preferences.
    public void GetMasterVolume() {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SetMasterVolume();
    }
    // This function will save the MasterVolume in Players preferences.
    public void SetMasterVolume() {
       PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        masterVolumeValueText.text = ((int)(masterVolumeSlider.value * 10)).ToString();
        UpdateMasterVolume(masterVolumeSlider.value);
    }
    // This function will save the MasterVolume slider's value to the Player preferences once it has been changed in run-time.
    public void ChangeMasterVolume() {
        // The Master Volume of the game will be equal to the slider value.
        AudioListener.volume = masterVolumeSlider.value;
        SetMasterVolume();
        UpdateMasterVolume(masterVolumeSlider.value);
    }
    public void UpdateMasterVolume(float volume) {
        foreach (AudioSource audioClip in allAudioClipArray) {
            audioClip.volume = volume;
        }
    }
    // Sound Volume Functions:
    // This function will load the SoundVolume from Players preferences.
    public void GetSoundVolume() {
        soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume");
        SetSoundVolume();
    }
    // This function will save the SoundVolume in Players preferences.
    public void SetSoundVolume() {
        PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlider.value);
        soundVolumeValueText.text = ((int)(soundVolumeSlider.value * 10)).ToString();
        audioManagerScript.UpdateMixerVolume();
        soundVolume = soundVolumeSlider.value;
        UpdateSoundVolume(soundVolumeSlider.value);
    }
    // This function will save the SoundVolume slider's value to the Player preferences once it has been changed in run-time.
    public void ChangeSoundVolume() {
        // The SoundVolume of the game will be equal to the slider value.
        SetSoundVolume();
        UpdateSoundVolume(soundVolumeSlider.value);
    }
    public void UpdateSoundVolume(float volume) {
        foreach (AudioSource audioClip in soundAudioClipArray) {
            audioClip.volume = volume;
        }
    }
    // Music Volume Functions:
    // This function will load the MusicVolume from Players preferences.
    public void GetMusicVolume() {
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SetMusicVolume();
    }
    // This function will save the MusicVolume in Players preferences.
    public void SetMusicVolume() {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        musicVolumeValueText.text = ((int)(musicVolumeSlider.value * 10)).ToString();
        audioManagerScript.UpdateMixerVolume();
        musicVolume = musicVolumeSlider.value;
        UpdateMusicVolume(musicVolumeSlider.value);
    }
    // This function will save the MusicVolume slider's value to the Player preferences once it has been changed in run-time.
    public void ChangeMusicVolume() {
        // The MusicVolume of the game will be equal to the slider value.
        SetMusicVolume();
        UpdateMusicVolume(musicVolumeSlider.value);
    }
    public void UpdateMusicVolume(float volume) {
        foreach (AudioSource audioClip in musicAudioClipArray) {
            audioClip.volume = volume;
        }
    }
}