using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour{
    [SerializeField]
    private AudioMixerGroup soundMixerGroup;
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;
    // Audio Source Array
    private AudioSource[] allAudioSources;
    // Main Menu Music Audio
    [SerializeField]
    private AudioSource mainMenuMusicAudio;
    // Tutorial Music Audio
    [SerializeField]
    private AudioSource tutorialMusicAudio;
    // Gameplay Music Audio
    [SerializeField]
    private AudioSource gameplayMusicAudio;
    // You Win Audio
    [SerializeField]
    private AudioSource youWinAudio;
    // Game Over Audio
    [SerializeField]
    private AudioSource gameOverAudio;
    // Game Over Gasp Audio
    [SerializeField]
    private AudioSource gameOverGaspAudio;
    // Player Shoot Audio
    [SerializeField]
    private AudioSource playerShootAudio;
    // First Player Hurt Audio
    [SerializeField]
    private AudioSource playerHurtOneAudio;
    // Second Player Hurt Audio
    [SerializeField]
    private AudioSource playerHurtTwoAudio;
    // Player Heal Audio
    [SerializeField]
    private AudioSource playerHealingAudio;
    // Player Jump Audio
    [SerializeField]
    private AudioSource playerJumpAudio;
    // Wave Complete Audio
    [SerializeField]
    private AudioSource waveCompleteAudio;
    // First Mouse Enemy Death Audio
    [SerializeField]
    private AudioSource mouseDeathOneAudio;
    // Second Mouse Enemy Death Audio
    [SerializeField]
    private AudioSource mouseDeathTwoAudio;
    // Dog Enemy Death Audio
    [SerializeField]
    private AudioSource dogDeathAudio;
    // Brute Dog Enemy Death Audio
    [SerializeField]
    private AudioSource bruteDogDeathAudio;
    // Pickup Powerup Audio
    [SerializeField]
    private AudioSource pickupPowerupAudio;
    // Equip Weapon Audio
    [SerializeField]
    private AudioSource equipWeaponAudio;
    // Enter Portal Audio
    [SerializeField]
    private AudioSource enterPortalAudio;
    // Load Wave Audio
    [SerializeField]
    private AudioSource loadWaveAudio;
    // UI Button Click Audio
    [SerializeField]
    private AudioSource buttonClickUIAudio;

	private void Start() {
        StopButtonClickUIAudio();
    }
	public void PauseAudio() {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audio in allAudioSources) {
            audio.Pause();
        }
    }
    public void UnPauseAudio() {
        foreach (AudioSource audio in allAudioSources) {
            audio.UnPause();
        }
    }
    public void MainMenuMusicAudio() {
        mainMenuMusicAudio.Play();
    }
    public void TutorialMusicAudio() {
        tutorialMusicAudio.Play();
    }
    public void StopTutorialMusicAudio() {
        tutorialMusicAudio.Stop();
    }
    public void GameplayMusicAudio() {
        gameplayMusicAudio.Play();
    }
    public void StopGameplayMusicAudio() {
        gameplayMusicAudio.Stop();
    }
    public void YouWinAudio() {
        youWinAudio.Play();
    }
    public void GameOverAudio() {
        gameOverAudio.Play();
    }
    public void GameOverGaspAudio() {
        gameOverGaspAudio.Play();
    }
    public void PlayerShootAudio() {
        playerShootAudio.Play();
    }
    public void PlayerHurtAudio() {
        // Random int - either 1 or 2
        int randIndex = Random.Range(1, 3);
        // Play 1 of the two possible Player Hurt Sounds
        if (randIndex == 1) {
            playerHurtOneAudio.Play();
        } else if (randIndex == 2) {
            playerHurtTwoAudio.Play();
        }
    }
    public void PlayerHealingAudio() {
        playerHealingAudio.Play();
    }
    public void PlayerJumpAudio() {
        playerJumpAudio.Play();
    }
    public void WaveCompleteAudio() {
        waveCompleteAudio.Play();
    }
    public void MouseDeathAudio() {
        // Random int - either 1 or 2
        int randIndex = Random.Range(1, 3);
        // Play 1 of the two possible Mouse Death Sounds
        if (randIndex == 1) {
            mouseDeathOneAudio.Play();
        }
        else if(randIndex == 2) {
            mouseDeathTwoAudio.Play();
        }
    }
    public void DogDeathAudio() {
        dogDeathAudio.Play();
    }
    public void BruteDogDeathAudio() {
        bruteDogDeathAudio.Play();
    }
    public void PickupPowerupAudio() {
        pickupPowerupAudio.Play();
    }
    public void EquipWeaponAudio() {
        equipWeaponAudio.Play();
    }
    public void EnterPortalAudio() {
        enterPortalAudio.Play();
    }
    public void LoadWaveAudio() {
        loadWaveAudio.Play();
    }
    public void ButtonClickUIAudio() {
        buttonClickUIAudio.Play();
    }
    public void StopButtonClickUIAudio() {
        buttonClickUIAudio.Stop();
    }
    public void UpdateMixerVolume() {
        soundMixerGroup.audioMixer.SetFloat("SoundVolume", Mathf.Log10(OptionsManager.soundVolume) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10 (OptionsManager.musicVolume) * 20);
    }
}