using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOver : MonoBehaviour {
    [SerializeField]
    private GameObject lifeCatDiedPanelObj;
    [SerializeField]
    private GameObject deathCatDiedPanelObj;
    [SerializeField]
    private GameObject lifeCatPlayAgainBtn;
    [SerializeField]
    private GameObject deathCatPlayAgainBtn;

    // Check which cat died and Load the neccessary GameOver Background
    private void Awake() {
        // Show GameOver Screen referencing the Life Cat
        if (DamagePlayer.lifeCatDied == true && DamagePlayer.deathCatDied == false) {
            lifeCatDiedPanelObj.SetActive(true);
            deathCatDiedPanelObj.SetActive(false);
            // Focus on Btn
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(lifeCatPlayAgainBtn);
            // Set Static Bools back to false
            DamagePlayer.lifeCatDied = false;
            DamagePlayer.deathCatDied = false;
        }
        // Show GameOver Screen referencing the Death Cat
        else if(DamagePlayer.lifeCatDied == false && DamagePlayer.deathCatDied == true) {
            lifeCatDiedPanelObj.SetActive(false);
            deathCatDiedPanelObj.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(deathCatPlayAgainBtn);
            // Set Static Bools back to false
            DamagePlayer.lifeCatDied = false;
            DamagePlayer.deathCatDied = false;
        } else {
            lifeCatDiedPanelObj.SetActive(true);
        }
    }
    public void PlayAgainBtn() {
        if (GameStateManager.inTutorial == true) {
            GameplayUIManager.pressedPlayAgain = true;
            WaveManager.LoadTutorial();
        } else if(GameStateManager.inTutorial == false) {
            GameplayUIManager.pressedPlayAgain = true;
            WeaponManager.lifeCatEquippedWeapon = "Pistol";
            WeaponManager.deathCatEquippedWeapon = "Pistol";
            WaveManager.LoadWave1();
        }
    }
    public void BackToMainMenuBtn() {
        WaveManager.LoadMainMenu();
    }
}