using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    // Bools for checking if a certain type of Enemy can spawn
    public static bool canSpawnTierOneEnemy = false;
    public static bool canSpawnTierTwoEnemy = false;
    public static bool canSpawnTierThreeEnemy = false;
    // Ints for checking if the Max Number of Enemies to Kill
    public static int maxNumOfLifeEnemiesToKill;
    public static int maxNumOfDeathEnemiesToKill;
    // Ints for checking if the Current number of Enemies that have been Killed
    public static int numOfLifeEnemiesKilled = 0;
    public static int numOfDeathEnemiesKilled = 0;
}