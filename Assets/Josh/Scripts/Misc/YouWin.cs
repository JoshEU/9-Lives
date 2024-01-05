using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWin : MonoBehaviour {
    public void PlayAgainBtn() {
        GameplayUIManager.pressedPlayAgain = true;
        WaveManager.LoadWave1();
    }
    public void BackToMainMenuBtn() {
        WaveManager.LoadMainMenu();
    }
}