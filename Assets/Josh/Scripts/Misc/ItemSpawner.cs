using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This Class will allow the game to spawn a 'GameObject' [chosen inside the inspector] in a pre-defined array of spawn-points [you can choose amount of spawn points inside the inspector as well as its position]
public class ItemSpawner : MonoBehaviour {
    // Script References
    [SerializeField]
    private BeginWave beginWaveScript;
    // Spawn Items Array
    [SerializeField]
    public GameObject[] itemsToSpawn;
    // Life Spawn Locations:
    public Vector3[] lifeHealthSpawnLocations;
    public Vector3[] lifeShieldSpawnLocations;
    public Vector3[] lifeSpeedBoostSpawnLocations;
    public Vector3[] lifeDoubleDmgSpawnLocations;
    public Vector3[] lifeWeaponSpawnLocations;
    public Vector3[] lifeEnemySpawnLocations;
    [Space(15)]
    // Death Spawn Locations:
    public Vector3[] deathHealthSpawnLocations;
    public Vector3[] deathShieldSpawnLocations;
    public Vector3[] deathSpeedBoostSpawnLocations;
    public Vector3[] deathDoubleDmgSpawnLocations;
    public Vector3[] deathWeaponSpawnLocations;
    public Vector3[] deathEnemySpawnLocations;
    [Space(15)]
    // Spawn Delays (Both Life/Death):
    public float healthSpawnDelay = 0.0f;
    public float shieldSpawnDelay = 0.0f;
    public float speedBoostSpawnDelay = 0.0f;
    public float doubleDmgSpawnDelay = 0.0f;
    public float weaponSpawnDelay = 0.0f;
    public float lifeEnemySpawnDelay = 0.0f;
    public float deathEnemySpawnDelay = 0.0f;
    [Space(15)]
    // Life Spawn Check Bools:
    // Life Powerups:
    public bool canSpawnLifeHealth = true;
    public bool canSpawnLifeShield = true;
    public bool canSpawnLifeSpeedBoost = true;
    public bool canSpawnLifeDoubleDmg = true;
    // Life Enemies:
    public bool canSpawnLifeEnemy = true;
    // Life Weapons:
    public bool canSpawnLifeWeapon = true;
    [Space(15)]
    // Death Spawn Check Bools:
    // DeathPowerups:
    public bool canSpawnDeathHealth = true;
    public bool canSpawnDeathShield = true;
    public bool canSpawnDeathSpeedBoost = true;
    public bool canSpawnDeathDoubleDmg = true;
    // Death Enemies:
    public bool canSpawnDeathEnemy = true;
    // Death Weapons:
    public bool canSpawnDeathWeapon = true;
    [Space(15)]
    // Powerup Locations that are taken Lists:
    public List<Vector3> lifeHealthCurrentList = new List<Vector3>();
    public List<Vector3> deathHealthCurrentList = new List<Vector3>();
    public List<Vector3> lifeShieldCurrentList = new List<Vector3>();
    public List<Vector3> deathShieldCurrentList = new List<Vector3>();
    public List<Vector3> lifeSpeedBoostCurrentList = new List<Vector3>();
    public List<Vector3> deathSpeedBoostCurrentList = new List<Vector3>();
    public List<Vector3> lifeDoubleDmgCurrentList = new List<Vector3>();
    public List<Vector3> deathDoubleDmgCurrentList = new List<Vector3>();
    public List<Vector3> lifeWeaponCurrentList = new List<Vector3>();
    public List<Vector3> deathWeaponCurrentList = new List<Vector3>();
    [Space(15)]
    // Misc:
    [SerializeField]
    public GameObject lifeEnemyParentObj;
    [SerializeField]
    public GameObject deathEnemyParentObj;
    [SerializeField]
    public GameObject lifePowerUpParentObj;
    [SerializeField]
    public GameObject deathPowerUpParentObj;
    [Space(15)]
    public int numOfStartingHealth = 0;
    public int numOfStartingShields = 0;
    public int numOfStartingSpeedBoosts = 0;
    public int numOfStartingDoubleDamages = 0;
    public int numOfStartingWeapons = 0;
    [Space(15)]
    public int numOfStartingEnemies = 0;
    [Space(15)]
    public int totalLifeEnemiesSpawnedIn;
    public int totalDeathEnemiesSpawnedIn;
    [SerializeField]
    private GameObject endOfWaveObj;

    // These Co-routines Wait the specified amount of time before spawning the chosen GameObject (set inside of the inspector) in a random location from the pre-defined array of possible spawn locations (also set inside of the inspector):
    // Life Co-routines for the Life Side (which the Death Cat is on):
    public IEnumerator SpawnLifeHealth() {
        if (canSpawnLifeHealth && totalLifeEnemiesSpawnedIn < EnemyManager.maxNumOfLifeEnemiesToKill) {
            yield return new WaitForSeconds(healthSpawnDelay);
            int randIndex = Random.Range(0, lifeHealthSpawnLocations.Length);
            // Check if another Health Powerup is already in this random location chosen
            while (lifeHealthCurrentList.Contains(lifeHealthSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, lifeHealthSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[0], lifeHealthSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(lifePowerUpParentObj.transform);
            // Add position to the current locations list
            lifeHealthCurrentList.Add(lifeHealthSpawnLocations[randIndex]);
            newItem.name = "Health";
            canSpawnLifeHealth = false;
		} else if(canSpawnLifeHealth && totalLifeEnemiesSpawnedIn >= EnemyManager.maxNumOfLifeEnemiesToKill) {
            canSpawnLifeHealth = false;
        }
        yield return null;
    }
    public IEnumerator SpawnLifeShield() {
        if (canSpawnLifeShield && totalLifeEnemiesSpawnedIn < EnemyManager.maxNumOfLifeEnemiesToKill) {
            yield return new WaitForSeconds(shieldSpawnDelay);
            int randIndex = Random.Range(0, lifeShieldSpawnLocations.Length);
            // Check if another Shield Powerup is already in this random location chosen
            while (lifeShieldCurrentList.Contains(lifeShieldSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, lifeShieldSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[1], lifeShieldSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(lifePowerUpParentObj.transform);
            // Add position to the current locations list
            lifeShieldCurrentList.Add(lifeShieldSpawnLocations[randIndex]);
            newItem.name = "Shield";
            canSpawnLifeShield = false;
        } else if (canSpawnLifeShield && totalLifeEnemiesSpawnedIn >= EnemyManager.maxNumOfLifeEnemiesToKill) {
            canSpawnLifeShield = false;
        }
        yield return null;
    }
    public IEnumerator SpawnLifeSpeedBoost() {
        if (canSpawnLifeSpeedBoost && totalLifeEnemiesSpawnedIn < EnemyManager.maxNumOfLifeEnemiesToKill) {
            yield return new WaitForSeconds(speedBoostSpawnDelay);
            int randIndex = Random.Range(0, lifeSpeedBoostSpawnLocations.Length);
            // Check if another Shield Powerup is already in this random location chosen
            while (lifeSpeedBoostCurrentList.Contains(lifeSpeedBoostSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, lifeSpeedBoostSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[2], lifeSpeedBoostSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(lifePowerUpParentObj.transform);
            // Add position to the current locations list
            lifeSpeedBoostCurrentList.Add(lifeSpeedBoostSpawnLocations[randIndex]);
            newItem.name = "SpeedBoost";
            canSpawnLifeSpeedBoost = false;
        } else if (canSpawnLifeSpeedBoost && totalLifeEnemiesSpawnedIn >= EnemyManager.maxNumOfLifeEnemiesToKill) {
            canSpawnLifeSpeedBoost = false;
        }
        yield return null;
    }
    public IEnumerator SpawnLifeDoubleDmg() {
        if (canSpawnLifeDoubleDmg && totalLifeEnemiesSpawnedIn < EnemyManager.maxNumOfLifeEnemiesToKill) {
            yield return new WaitForSeconds(doubleDmgSpawnDelay);
            int randIndex = Random.Range(0, lifeDoubleDmgSpawnLocations.Length);
            // Check if another Double Dmg Powerup is already in this random location chosen
            while (lifeDoubleDmgCurrentList.Contains(lifeDoubleDmgSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, lifeDoubleDmgSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[3], lifeDoubleDmgSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(lifePowerUpParentObj.transform);
            // Add position to the current locations list
            lifeDoubleDmgCurrentList.Add(lifeDoubleDmgSpawnLocations[randIndex]);
            newItem.name = "DoubleDamage";
            canSpawnLifeDoubleDmg = false;
        } else if (canSpawnLifeDoubleDmg && totalLifeEnemiesSpawnedIn >= EnemyManager.maxNumOfLifeEnemiesToKill) {
            canSpawnLifeDoubleDmg = false;
        }
        yield return null;
    }
    public IEnumerator SpawnLifeWeapon() {
        if (canSpawnLifeWeapon) {
            yield return new WaitForSeconds(weaponSpawnDelay);
            int randIndex = Random.Range(0, lifeWeaponSpawnLocations.Length);
            // Check if another Weapon is already in this random location chosen
            while (lifeWeaponCurrentList.Contains(lifeWeaponSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, lifeWeaponSpawnLocations.Length);
            }
            // Check which weapons can be spawned - this is according to the current wave that the players are on as well as their weapon in hand
            if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == false && WeaponManager.canSpawnAssaultRifle == false) {
                // Spawn only a Pistol
                GameObject newItem = Instantiate(itemsToSpawn[10], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                // Add position to the current locations list
                lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
            } 
            else if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == true && WeaponManager.canSpawnAssaultRifle == false) {
                // Check the Death cats currently equipped weapon
                if (WeaponManager.deathCatEquippedWeapon == "Pistol") {
                    // Spawn Smg
                    GameObject newItem = Instantiate(itemsToSpawn[11], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                } else if (WeaponManager.deathCatEquippedWeapon == "Smg") {
                    // Spawn Pistol
                    GameObject newItem = Instantiate(itemsToSpawn[10], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                }
            } 
            else if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == true && WeaponManager.canSpawnAssaultRifle == true) {
                // Spawn either a Pistol or Smg or AssaultRifle [The weapon that the player has equipped currently will not spawn] 
                // Check the Death cats currently equipped weapon
                if (WeaponManager.deathCatEquippedWeapon == "Pistol") {
                    // Spawn Smg or Ar at random
                    GameObject newItem = Instantiate(itemsToSpawn[Random.Range(11, 13)], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                } 
                else if (WeaponManager.deathCatEquippedWeapon == "Smg") {
                    // Spawn Pistol or Ar at random
                    int randWeaponIndex = Random.Range(1, 3);
                    // Spawn Pistol if 1
                    if (randWeaponIndex == 1) {
                        GameObject newItem = Instantiate(itemsToSpawn[10], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                        // Add position to the current locations list
                        lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                    }
                    // Spawn Ar if 2
                    else if (randWeaponIndex == 2) {
                        GameObject newItem = Instantiate(itemsToSpawn[12], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                        // Add position to the current locations list
                        lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                    }
                } 
                else if (WeaponManager.deathCatEquippedWeapon == "AssaultRifle") {
                    // Spawn Pistol or Smg at random
                    GameObject newItem = Instantiate(itemsToSpawn[Random.Range(10, 12)], lifeWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    lifeWeaponCurrentList.Add(lifeWeaponSpawnLocations[randIndex]);
                }
            }
            canSpawnLifeWeapon = false;
        }
        yield return null;
    }
	public IEnumerator SpawnLifeEnemy() {
        // Only Spawn a Life Enemy if the total spawned in during that wave is less than the maxNumberOfLifeEnemiesToKill
        if (canSpawnLifeEnemy && totalLifeEnemiesSpawnedIn < EnemyManager.maxNumOfLifeEnemiesToKill) {
			yield return new WaitForSeconds(lifeEnemySpawnDelay);
			int randIndex = Random.Range(0, lifeEnemySpawnLocations.Length);
			// Check which tier enemies can be spawned - this is according to the current wave that the players are on
			if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy == false && EnemyManager.canSpawnTierThreeEnemy == false) {
				// Spawn only a Tier 1 Enemy
				GameObject newItem = Instantiate(itemsToSpawn[4], lifeEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(lifeEnemyParentObj.transform);
            } else if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy && EnemyManager.canSpawnTierThreeEnemy == false) {
				// Spawn either a Tier 1 or Tier 2 Enemy
				GameObject newItem = Instantiate(itemsToSpawn[Random.Range(4, 6)], lifeEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(lifeEnemyParentObj.transform);
            } else if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy && EnemyManager.canSpawnTierThreeEnemy) {
				// Spawn either a Tier 1, Tier 2 or Tier 3 Enemy
				GameObject newItem = Instantiate(itemsToSpawn[Random.Range(4, 7)], lifeEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(lifeEnemyParentObj.transform);
            }
            totalLifeEnemiesSpawnedIn += 1;
            canSpawnLifeEnemy = false;
		}
		yield return null;
    }
    // Death Co-routines for the Death Side (which the Life Cat is on):
    public IEnumerator SpawnDeathHealth() {
        if (canSpawnDeathHealth && totalDeathEnemiesSpawnedIn < EnemyManager.maxNumOfDeathEnemiesToKill) {
            yield return new WaitForSeconds(healthSpawnDelay);
            int randIndex = Random.Range(0, deathHealthSpawnLocations.Length);
            // Check if another Health Powerup is already in this random location chosen
            while (deathHealthCurrentList.Contains(deathHealthSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, deathHealthSpawnLocations.Length); // check this as unity crashes
            }
            GameObject newItem = Instantiate(itemsToSpawn[0], deathHealthSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(deathPowerUpParentObj.transform);
            // Add position to the current locations list
            deathHealthCurrentList.Add(deathHealthSpawnLocations[randIndex]);
            newItem.name = "Health";
            canSpawnDeathHealth = false;
        } else if (canSpawnDeathHealth && totalDeathEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            canSpawnDeathHealth = false;
        }
        yield return null;
    }
    public IEnumerator SpawnDeathShield() {
        if (canSpawnDeathShield && totalDeathEnemiesSpawnedIn < EnemyManager.maxNumOfDeathEnemiesToKill) {
            yield return new WaitForSeconds(shieldSpawnDelay);
            int randIndex = Random.Range(0, deathShieldSpawnLocations.Length);
            // Check if another Shield Powerup is already in this random location chosen
            while (deathShieldCurrentList.Contains(deathShieldSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, deathShieldSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[1], deathShieldSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(deathPowerUpParentObj.transform);
            // Add position to the current locations list
            deathShieldCurrentList.Add(deathShieldSpawnLocations[randIndex]);
            newItem.name = "Shield";
            canSpawnDeathShield = false;
        } else if (canSpawnDeathShield && totalDeathEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            canSpawnDeathShield = false;
        }
        yield return null;
    }
    public IEnumerator SpawnDeathSpeedBoost() {
        if (canSpawnDeathSpeedBoost && totalDeathEnemiesSpawnedIn < EnemyManager.maxNumOfDeathEnemiesToKill) {
            yield return new WaitForSeconds(speedBoostSpawnDelay);
            int randIndex = Random.Range(0, deathSpeedBoostSpawnLocations.Length);
            // Check if another Speed Boost Powerup is already in this random location chosen
            while (deathSpeedBoostCurrentList.Contains(deathSpeedBoostSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, deathSpeedBoostSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[2], deathSpeedBoostSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(deathPowerUpParentObj.transform);
            // Add position to the current locations list
            deathSpeedBoostCurrentList.Add(deathSpeedBoostSpawnLocations[randIndex]);
            newItem.name = "SpeedBoost";
            canSpawnDeathSpeedBoost = false;
        } else if (canSpawnDeathSpeedBoost && totalDeathEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            canSpawnDeathSpeedBoost = false;
        }
        yield return null;
    }
    public IEnumerator SpawnDeathDoubleDmg() {
        if (canSpawnDeathDoubleDmg && totalDeathEnemiesSpawnedIn < EnemyManager.maxNumOfDeathEnemiesToKill) {
            yield return new WaitForSeconds(doubleDmgSpawnDelay);
            int randIndex = Random.Range(0, deathDoubleDmgSpawnLocations.Length);
            // Check if another Double Dmg Powerup is already in this random location chosen
            while (deathDoubleDmgCurrentList.Contains(deathDoubleDmgSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, deathDoubleDmgSpawnLocations.Length);
            }
            GameObject newItem = Instantiate(itemsToSpawn[3], deathDoubleDmgSpawnLocations[randIndex], Quaternion.identity);
            newItem.transform.SetParent(deathPowerUpParentObj.transform);
            // Add position to the current locations list
            deathDoubleDmgCurrentList.Add(deathDoubleDmgSpawnLocations[randIndex]);
            newItem.name = "DoubleDamage";
            canSpawnDeathDoubleDmg = false;
        } else if (canSpawnDeathDoubleDmg && totalDeathEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            canSpawnDeathDoubleDmg = false;
        }
        yield return null;
    }
    public IEnumerator SpawnDeathWeapon() {
        if (canSpawnDeathWeapon) {
            yield return new WaitForSeconds(weaponSpawnDelay);
            int randIndex = Random.Range(0, deathWeaponSpawnLocations.Length);
            // Check if another Weapon is already in this random location chosen
            while (deathWeaponCurrentList.Contains(deathWeaponSpawnLocations[randIndex])) {
                randIndex = Random.Range(0, deathWeaponSpawnLocations.Length);
            }
            // Check which weapons can be spawned - this is according to the current wave that the players are on as well as their weapon in hand
            if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == false && WeaponManager.canSpawnAssaultRifle == false) {
                // Spawn only a Pistol
                GameObject newItem = Instantiate(itemsToSpawn[10], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                // Add position to the current locations list
                deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
            } 
            else if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == true && WeaponManager.canSpawnAssaultRifle == false) {
                // Check the Life cats currently equipped weapon
                if (WeaponManager.lifeCatEquippedWeapon == "Pistol") {
                    // Spawn Smg
                    GameObject newItem = Instantiate(itemsToSpawn[11], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                } else if (WeaponManager.lifeCatEquippedWeapon == "Smg") {
                    // Spawn Pistol
                    GameObject newItem = Instantiate(itemsToSpawn[10], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                }
            } 
            else if (WeaponManager.canSpawnPistol == true && WeaponManager.canSpawnSmg == true && WeaponManager.canSpawnAssaultRifle == true) {
                // Spawn either a Pistol or Smg or AssaultRifle [The weapon that the player has equipped currently will not spawn] 
                // Check the Life cats currently equipped weapon
                if (WeaponManager.lifeCatEquippedWeapon == "Pistol") {
                    // Spawn Smg or Ar at random
                    GameObject newItem = Instantiate(itemsToSpawn[Random.Range(11,13)], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                } 
                else if (WeaponManager.lifeCatEquippedWeapon == "Smg") {
                    // Spawn Pistol or Ar at random
                    int randWeaponIndex = Random.Range(1, 3);
                    // Spawn Pistol if 1
                    if(randWeaponIndex == 1) {
                        GameObject newItem = Instantiate(itemsToSpawn[10], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                        // Add position to the current locations list
                        deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                    } 
                    // Spawn Ar if 2
                    else if(randWeaponIndex == 2) {
                        GameObject newItem = Instantiate(itemsToSpawn[12], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                        // Add position to the current locations list
                        deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                    }
                } 
                else if (WeaponManager.lifeCatEquippedWeapon == "AssaultRifle") {
                    // Spawn Pistol or Smg at random
                    GameObject newItem = Instantiate(itemsToSpawn[Random.Range(10, 12)], deathWeaponSpawnLocations[randIndex], Quaternion.identity);
                    // Add position to the current locations list
                    deathWeaponCurrentList.Add(deathWeaponSpawnLocations[randIndex]);
                }
            }
            canSpawnDeathWeapon = false;
        }
        yield return null;
    }
    public IEnumerator SpawnDeathEnemy() {
        // Only Spawn a Death Enemy if the total spawned in during that wave is less than the maxNumberOfDeathEnemiesToKill
        if (canSpawnDeathEnemy && totalDeathEnemiesSpawnedIn < EnemyManager.maxNumOfDeathEnemiesToKill) {
        yield return new WaitForSeconds(deathEnemySpawnDelay);
            int randIndex = Random.Range(0, deathEnemySpawnLocations.Length);
            // Check which tier enemies can be spawned - this is according to the current wave that the players are on
            if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy == false && EnemyManager.canSpawnTierThreeEnemy == false) {
                // Spawn only a Tier 1 Enemy
                GameObject newItem = Instantiate(itemsToSpawn[7], deathEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(deathEnemyParentObj.transform);
            } else if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy && EnemyManager.canSpawnTierThreeEnemy == false) {
                // Spawn either a Tier 1 or Tier 2 Enemy
                GameObject newItem = Instantiate(itemsToSpawn[Random.Range(7, 9)], deathEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(deathEnemyParentObj.transform);
            } else if (EnemyManager.canSpawnTierOneEnemy && EnemyManager.canSpawnTierTwoEnemy && EnemyManager.canSpawnTierThreeEnemy) {
                // Spawn either a Tier 1, Tier 2 or Tier 3 Enemy
                GameObject newItem = Instantiate(itemsToSpawn[Random.Range(7, 10)], deathEnemySpawnLocations[randIndex], Quaternion.identity);
                newItem.transform.SetParent(deathEnemyParentObj.transform);
            }
            totalDeathEnemiesSpawnedIn += 1;
            canSpawnDeathEnemy = false;
        }
        yield return null;
    } 
    private IEnumerator beginSpawningDelay() {
        // Wait the specified delay before calling all spawning Co-routines.
        yield return new WaitForSeconds(beginWaveScript.waveBeginDelay);
        // Spawn in the num of starting Powerup (specified in the inspector at the beginning of a wave - there will still be the specified delay to the powerup spawning in)
        // Initial amount of Health to spawn in
        for (int i = 0; i <= numOfStartingHealth; i++) {
            StartCoroutine(SpawnLifeHealth());
            StartCoroutine(SpawnDeathHealth());
        }
        // Initial amount of Shields to spawn in
        for (int i = 0; i <= numOfStartingShields; i++) {
            StartCoroutine(SpawnLifeShield());
            StartCoroutine(SpawnDeathShield());
        }
        // Initial amount of Speed Boosts to spawn in
        for (int i = 0; i <= numOfStartingSpeedBoosts; i++) {
            StartCoroutine(SpawnLifeSpeedBoost());
            StartCoroutine(SpawnDeathSpeedBoost());
        }
        // Initial amount of Double Dmgs to spawn in
        for (int i = 0; i <= numOfStartingDoubleDamages; i++) {
            StartCoroutine(SpawnLifeDoubleDmg());
            StartCoroutine(SpawnDeathDoubleDmg());
        }
        // Initial amount of Weapons to spawn in
        for (int i = 0; i <= numOfStartingWeapons; i++) {
            StartCoroutine(SpawnLifeWeapon());
            StartCoroutine(SpawnDeathWeapon());
        }
        // Increment the total number of Enemies Spawned in by the starting number of enemies
        totalLifeEnemiesSpawnedIn += numOfStartingEnemies;
        totalDeathEnemiesSpawnedIn += numOfStartingEnemies;
    }
    void Start() {
        StartCoroutine(beginSpawningDelay());
    }
	private void Update() {
        if(endOfWaveObj.activeInHierarchy && totalLifeEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill && totalDeathEnemiesSpawnedIn >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            GameObject[] powerupArray =  GameObject.FindGameObjectsWithTag("Powerup");
            foreach(GameObject powerup in powerupArray) {
                Destroy(powerup);
			}
		}
    }
} 