using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameplayUIManager : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private GameStateManager gameStateManagerScript;
    [SerializeField]
    private WaveManager waveManagerScript;
    [SerializeField]
    public Slider lifeCatProgressSlider;
    [SerializeField]
    public Slider deathCatProgressSlider;
    [SerializeField]
    public GameObject playAgainBtnObj;
    [SerializeField]
    private GameObject backToMainMenuBtnObj;
    [SerializeField]
    private GameObject pauseMenuObj;
    [SerializeField]
    private GameObject pauseMenuResumeBtnObj;
    [SerializeField]
    private GameObject pauseMenuOptionsBtnObj;
    [SerializeField]
    private GameObject pauseMenuHowToPlayBtnObj;
    [SerializeField]
    private GameObject pauseOptionsManagerObj;
    [SerializeField]
    private GameObject pauseHowToPlayManagerObj;
    [SerializeField]
    private GameObject masterVolumeSliderObj;
    [SerializeField]
    private WeaponManager lifeCatWeaponManagerScript;
    [SerializeField]
    private WeaponManager deathCatWeaponManagerScript;
    public static bool canPauseGame = true;
    public static bool pressedPlayAgain = false;
    public static string previousState = "";
    private GameObject recentBtnHover;

    private void Awake() {
        gameStateManagerScript = (GameStateManager)GameObject.FindObjectOfType(typeof(GameStateManager));
        waveManagerScript = (WaveManager)GameObject.FindObjectOfType(typeof(WaveManager));
        canPauseGame = true;
    }
	// Start is called before the first frame update
	void Start() {
        // Set initial slider values to 0
        lifeCatProgressSlider.value = 0;
        deathCatProgressSlider.value = 0;
    }

    // Update is called once per frame
    void Update() {
         // Set max value for sliders to the max number of enemies that can spawn in the current wave
        lifeCatProgressSlider.maxValue = EnemyManager.maxNumOfLifeEnemiesToKill;
        deathCatProgressSlider.maxValue = EnemyManager.maxNumOfDeathEnemiesToKill;
        // Set the current value of the slider to be the number of enemies that either the Life cat or Death cat has killed - shows progress in the wave
        lifeCatProgressSlider.value = EnemyManager.numOfLifeEnemiesKilled;
        deathCatProgressSlider.value = EnemyManager.numOfDeathEnemiesKilled;
        // Checks if the game is paused
        // Checks if the player pressed 'B' button in either Options or How to Play Menu. If so it returns them to the Pause Menu
        if (GameStateManager.currentState == "Paused" && Gamepad.all[0].buttonEast.isPressed || GameStateManager.currentState == "Paused" && Gamepad.all[1].buttonEast.isPressed) {
            if(pauseOptionsManagerObj.activeSelf == true) {
                pauseOptionsManagerObj.SetActive(false);
                pauseMenuObj.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(recentBtnHover);
            }
            else if(pauseHowToPlayManagerObj.activeSelf == true) {
                pauseHowToPlayManagerObj.SetActive(false);
                pauseMenuObj.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(recentBtnHover);
            }
        }
    }
    // You Win & GameOver Buttons
    public void PlayAgainBtn() {
        if(GameStateManager.currentState != "Tutorial") {
            pressedPlayAgain = true;
            WeaponManager.lifeCatEquippedWeapon = "Pistol";
            WeaponManager.deathCatEquippedWeapon = "Pistol";
            WaveManager.LoadWave1();
		} else {
            pressedPlayAgain = true;
            WaveManager.LoadTutorial();
        }
    }
    // Used for Win Screen + GameOver Screen + Pause Menu
    public void BackToMainMenuBtn() {
        Destroy(gameStateManagerScript.gameObject);
        Destroy(waveManagerScript.gameObject);
        WaveManager.LoadMainMenu();
    }
    IEnumerator UnPause() {
        yield return new WaitForSeconds(0.1f);
        PlayerMovement.isPaused = false;
    }
    // Pause Menu Buttons:
    public void ResumeBtn() {
        // Allow Players to shoot again
        lifeCatWeaponManagerScript.hasGunInHand = true;
        deathCatWeaponManagerScript.hasGunInHand = true;
        pauseMenuObj.SetActive(false);
        GameStateManager.currentState = previousState;
        EventSystem.current.SetSelectedGameObject(null);
        // Un-Pauses any Audio that was previously playing in the game prior to pausing
        audioManagerScript.UnPauseAudio();
        Time.timeScale = 1;
        StartCoroutine(UnPause());
    }
    public void OptionsBtn() {
        recentBtnHover = pauseMenuOptionsBtnObj;
        // Set to Volume slider
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(masterVolumeSliderObj);
        pauseMenuObj.SetActive(false);
        pauseOptionsManagerObj.SetActive(true);
    }
    public void HowToPlayBtn() {
        recentBtnHover = pauseMenuHowToPlayBtnObj;
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenuObj.SetActive(false);
        pauseHowToPlayManagerObj.SetActive(true);
    }
    public void QuitBtn() {
		// Quit Application inside Build.exe of game
		Application.Quit();
	}

    // Called when 'Start' button is pressed on controller
    // Displays Pause Menu and selects the Resume Button
    private void OnPause() {
        if (canPauseGame) {
            // Pauses any Audio that is currently playing in the game
            audioManagerScript.PauseAudio();
            PlayerMovement.isPaused = true;
            // Make sure Players cannot shoot when paused
            lifeCatWeaponManagerScript.hasGunInHand = false;
            deathCatWeaponManagerScript.hasGunInHand = false;
            // Store previous state - Wave 'x'
            previousState = GameStateManager.currentState;
            pauseMenuObj.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(pauseMenuResumeBtnObj);
            lifeCatProgressSlider.enabled = false;
            deathCatProgressSlider.enabled = false;
            Time.timeScale = 0.0f;
            GameStateManager.currentState = "Paused";
        }
    }
}