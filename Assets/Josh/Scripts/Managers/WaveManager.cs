using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Loads the Waves once the number of enemies killed for both players has reached that waves maxenemiestokill number
public class WaveManager : MonoBehaviour {
    [SerializeField]
    private GameStateManager gameStateManagerScript;
    private static BeginWave beginWaveScript;
    public static int waveNumber = 0;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }
    private static IEnumerator FadeOut() {
        FadeOutManager.fadeOutTrigger = true;
        yield return new WaitForSeconds(1.5f);
        FadeOutManager.fadeOutTrigger = false;
    }
    public static void LoadMainMenu() {
        PlayerMovement.isPaused = false;
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        WeaponManager.lifeCatEquippedWeapon = "Pistol";
        WeaponManager.deathCatEquippedWeapon = "Pistol";
        Time.timeScale = 1;
        GameStateManager.currentState = "MainMenu";
        SceneManager.LoadScene("MainMenu");
    }
    public static void LoadTutorial() {
        PlayerMovement.isPaused = false;
        GameStateManager.inTutorial = true;
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        WeaponManager.lifeCatEquippedWeapon = "";
        WeaponManager.deathCatEquippedWeapon = "";
        GameStateManager.currentState = "Tutorial";
        SceneManager.LoadScene("Tutorial");
    }
    public static void LoadWinScene() {
        GameStateManager.currentState = "YouWin";
        SceneManager.LoadScene("YouWin");
    }
    public static void LoadGameOverScene() {
        GameStateManager.currentState = "GameOver";
        SceneManager.LoadScene("GameOver");
    }
    public static void LoadWave1() {
        PlayerMovement.isPaused = false;
        GameStateManager.inTutorial = false;
        NextWave.showPortals = false;
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 5;
        EnemyManager.maxNumOfDeathEnemiesToKill = 5;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        // Players Begin with Pistols
        WeaponManager.lifeCatEquippedWeapon = "Pistol";
        WeaponManager.deathCatEquippedWeapon = "Pistol";
        GameStateManager.currentState = "Wave1";
        waveNumber = 1;
        SceneManager.LoadScene("Wave1");
    }
    public static void LoadWave2() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 7;
        EnemyManager.maxNumOfDeathEnemiesToKill = 7;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave2";
        waveNumber = 2;
        SceneManager.LoadScene("Wave2");
    }
    public static void LoadWave3() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 9;
        EnemyManager.maxNumOfDeathEnemiesToKill = 9;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave3";
        waveNumber = 3;
        SceneManager.LoadScene("Wave3");
    }
    public static void LoadWave4() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 10;
        EnemyManager.maxNumOfDeathEnemiesToKill = 10;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave4";
        waveNumber = 4;
        SceneManager.LoadScene("Wave4");
    }
    public static void LoadWave5() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 12;
        EnemyManager.maxNumOfDeathEnemiesToKill = 12;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave5";
        waveNumber = 5;
        SceneManager.LoadScene("Wave5");
    }
    public static void LoadWave6() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 14;
        EnemyManager.maxNumOfDeathEnemiesToKill = 14;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave6";
        waveNumber = 6;
        SceneManager.LoadScene("Wave6");
    }
    public static void LoadWave7() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 16;
        EnemyManager.maxNumOfDeathEnemiesToKill = 16;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave7";
        waveNumber = 7;
        SceneManager.LoadScene("Wave7");
    }
    public static void LoadWave8() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 18;
        EnemyManager.maxNumOfDeathEnemiesToKill = 18;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave8";
        waveNumber = 8;
        SceneManager.LoadScene("Wave8");
    }
    public static void LoadWave9() {
        Physics2D.IgnoreLayerCollision(3, 13, false);
        Physics2D.IgnoreLayerCollision(3, 14, false);
        // Max Enemies to kill each in order to progress to next wave
        EnemyManager.maxNumOfLifeEnemiesToKill = 20;
        EnemyManager.maxNumOfDeathEnemiesToKill = 20;
        // Current Enemies Killed
        EnemyManager.numOfLifeEnemiesKilled = 0;
        EnemyManager.numOfDeathEnemiesKilled = 0;
        GameStateManager.currentState = "Wave9";
        waveNumber = 9;
        SceneManager.LoadScene("Wave9");
    }
}