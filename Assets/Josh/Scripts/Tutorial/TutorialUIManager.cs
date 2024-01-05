using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TutorialUIManager : MonoBehaviour {
    public static bool playMenuTransition = false;

    public void PlayAgainBtn() {
       GameplayUIManager.pressedPlayAgain = true;
        WaveManager.LoadTutorial();
    }

    // Called when the Player select the 'No' button in the EndTutorialPanel
    public IEnumerator delayMainGame() {
        yield return new WaitForSeconds(2.5f);
        GameplayUIManager.canPauseGame = true;
        WaveManager.LoadWave1();
    }
    public void EndPanelYesBtnClicked() {
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        WaveManager.LoadWave1();
    }
    public void EndPanelNoBtnClicked() {
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        StartCoroutine(delayMainGame());
    }
    public void FinishedTutorial() {
        // Called after finishing the tutorial
        PlayerPrefs.SetInt("NewPlayer", 1);
        PlayerPrefs.Save();
        GameplayUIManager.pressedPlayAgain = true;
        playMenuTransition = false;
    }
}