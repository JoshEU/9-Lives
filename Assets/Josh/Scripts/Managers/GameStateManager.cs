using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Script will keep track of which states the players are in during the game
// For waves, it will help set the maximum enemies that can spawn in as well as which tier of enemy
public class GameStateManager : MonoBehaviour {
    public static string currentState = "MainMenu";
    public static bool inTutorial = false;

    // Start is called before the first frame update
    void Start() {
        currentState = "MainMenu";
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {
        switch (currentState) {
            case "YouWin":
                GameplayUIManager.canPauseGame = false;
                break;
            case "GameOver":
                GameplayUIManager.canPauseGame = false;
                break;
            case "Tutorial":
                // Max Enemies to Spawn for each player
                EnemyManager.maxNumOfLifeEnemiesToKill = 3;
                EnemyManager.maxNumOfDeathEnemiesToKill = 3;
                // If True: Display Portals to Wave 1
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave1":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = false;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = false;
                WeaponManager.canSpawnSmg = false;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 2
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave2":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = false;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = false;
                WeaponManager.canSpawnSmg = false;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 3
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave3":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = false;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = false;
                WeaponManager.canSpawnSmg = false;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 4
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave4":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 5
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave5":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 6
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave6":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = false;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = false;
                // If True: Display Portals to Wave 7
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave7":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = true;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = true;
                // If True: Display Portals to Wave 8
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave8":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = true;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = true;
                // If True: Display Portals to Wave 9
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            case "Wave9":
                // Which types of Enemies can spawn in this wave
                EnemyManager.canSpawnTierOneEnemy = true;
                EnemyManager.canSpawnTierTwoEnemy = true;
                EnemyManager.canSpawnTierThreeEnemy = true;
                // Which weapons can spawn in this wave: None
                WeaponManager.canSpawnPistol = true;
                WeaponManager.canSpawnSmg = true;
                WeaponManager.canSpawnAssaultRifle = true;
                // If True: Display Portals to the YOU WIN! Scene
                if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill && EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
                    NextWave.showPortals = true;
                }
                break;
            default:
                break;
        }
    }
}