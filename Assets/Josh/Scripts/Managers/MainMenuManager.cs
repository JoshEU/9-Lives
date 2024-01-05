using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {
    [SerializeField]
    private GameStateManager gameStateManagerScript;
    [SerializeField]
    private GameObject mainMenuObj;
    [SerializeField]
    private GameObject optionsMenuObj;
    [SerializeField]
    private GameObject howToPlayMenuObj;
    [SerializeField]
    private GameObject startBtnObj;
    [SerializeField]
    private GameObject optionsBtnObj;
    [SerializeField]
    private GameObject howToPlayBtnObj;
    [SerializeField]
    private GameObject volumeSliderObj;
    public static bool hasPlayedTutorial = false;
    private GameObject recentBtnHover;
    private int playedBeforeState; 

    private void Start() {
        if (!PlayerPrefs.HasKey("NewPlayer")) {
            PlayerPrefs.SetInt("NewPlayer", 0);
        } else {
            PlayerPrefs.GetInt("NewPlayer");
        }
        playedBeforeState = PlayerPrefs.GetInt("NewPlayer");
        if (playedBeforeState == 1) {
            hasPlayedTutorial = true;
        } else if (playedBeforeState == 0) {
            hasPlayedTutorial = false;
        }
    }
    void Update() {
        // Returns back to the Main menu screen if you are in the Options or HowToPlayMenu
        if (Gamepad.all[0].buttonEast.isPressed && GameStateManager.currentState == "MainMenu" && mainMenuObj.activeSelf == false || Gamepad.all[1].buttonEast.isPressed && GameStateManager.currentState == "MainMenu" && mainMenuObj.activeSelf == false) {
            optionsMenuObj.SetActive(false);
            howToPlayMenuObj.SetActive(false);
            mainMenuObj.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(recentBtnHover);
        }
    }
    // Start of: Main Menu Button Functions:

    // This Function loads the 'MainGame' scene if the player has completed the tutorial before
    // Otherwise it will load the 'TutorialScene' scene to teach the player the basics of the game
    // There is a PlayerPref key that checks this
    public void StartGame() {
        // Checks if the player has played before
        if (hasPlayedTutorial == true) {
            GameplayUIManager.pressedPlayAgain = false;
            WaveManager.LoadWave1();
        } else if (hasPlayedTutorial == false) {
            GameplayUIManager.pressedPlayAgain = false;
            WaveManager.LoadTutorial();
        }
    }
    // Loads the OptionsMenu
    public void OptionsMenu() {
        recentBtnHover = optionsBtnObj;
        mainMenuObj.SetActive(false);
        optionsMenuObj.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(volumeSliderObj);
    }
    // Loads the HowToPlayMenu
    public void HowToPlayMenu() {
        recentBtnHover = howToPlayBtnObj;
        mainMenuObj.SetActive(false);
        howToPlayMenuObj.SetActive(true);
    }
    public void QuitGame() {
        // Quit Application inside Build.exe of game
        Application.Quit();
    }
    // End of: Main Menu Button Functions
}