using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tutorial : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D playerRB2D;
    [SerializeField]
    private Rigidbody2D otherPlayerRB2D;
    [SerializeField]
    private GameObject playerPistolObj;
    [SerializeField]
    private GameObject playerPickUpWeaponPrompt;
    [SerializeField]
    private GameObject playerShootPrompt;
    [SerializeField]
    private GameObject lifeMouseEnemy;
    [SerializeField]
    private GameObject deathMouseEnemy;
    [SerializeField]
    private GameObject lifeSpawnInMouseEnemy;
    [SerializeField]
    private GameObject deathSpawnInMouseEnemy;
    [SerializeField]
    private GameObject lifeSpawnInDogEnemy;
    [SerializeField]
    private GameObject deathSpawnInDogEnemy;
    [SerializeField]
    private GameObject lifeSpawnInBruteDogEnemy;
    [SerializeField]
    private GameObject deathSpawnInBruteDogEnemy;
    [SerializeField]
    public GameObject lCWaitingPanel;
    [SerializeField]
    public GameObject dCWaitingPanel;
    [SerializeField]
    private GameObject firstEnemyInfoDisplayPanel;
    [SerializeField]
    private GameObject secondEnemyInfoDisplayPanel;
    [SerializeField]
    private GameObject lifeEndPortal;
    [SerializeField]
    private GameObject deathEndPortal;
    [SerializeField]
    private GameObject lifeEnemyPortal;
    [SerializeField]
    private GameObject deathEnemyPortal;
    [SerializeField]
    private LifeFlyEnemy lifeFlyEnemyScript;
    [SerializeField]
    private DeathFlyEnemy deathFlyEnemyScript;

    public static bool lCPickedUpWeapon = false;
    public static bool dCPickedUpWeapon = false;
    public static bool isLifeMouseDead = false;
    public static bool isDeathMouseDead = false;
    public static bool isLifeDogDead = false;
    public static bool isDeathDogDead = false;
    public static bool isLifeBruteDogDead = false;
    public static bool isDeathBruteDogDead = false;

    public bool equipBtnPressed = false;

    void Start() {
        lCPickedUpWeapon = false;
        dCPickedUpWeapon = false;
        isLifeMouseDead = false;
        isDeathMouseDead = false;
        isLifeDogDead = false;
        isDeathDogDead = false;
        isLifeBruteDogDead = false;
        isDeathBruteDogDead = false;
        lifeEndPortal.SetActive(false);
        deathEndPortal.SetActive(false);
    }
    IEnumerator UnPause() {
        yield return new WaitForSeconds(0.1f);
        PlayerMovement.isPaused = false;
    }
    void Update() {
        // Close First Enemy Info Panel & Open the second one
		if (Gamepad.all[0].buttonSouth.isPressed && firstEnemyInfoDisplayPanel.activeSelf == true || Gamepad.all[1].buttonSouth.isPressed && firstEnemyInfoDisplayPanel.activeSelf == true) {
            StartCoroutine(UnPause());
            firstEnemyInfoDisplayPanel.SetActive(false);
            secondEnemyInfoDisplayPanel.SetActive(true);
        }
        // Close Second Enemy Info Panel & Spawn First Set of Enemies to Kill
        if (Gamepad.all[0].buttonEast.isPressed && secondEnemyInfoDisplayPanel.activeSelf == true || Gamepad.all[1].buttonEast.isPressed && secondEnemyInfoDisplayPanel.activeSelf == true) {
            secondEnemyInfoDisplayPanel.SetActive(false);
            SpawnMouseEnemy();
            Time.timeScale = 1;
        }
        // Check if First Set of Enemies are Dead (Mouse)
        if (isLifeMouseDead && isDeathMouseDead) {
            SpawnDogEnemy();
        }
        // Check if Second Set of Enemies are Dead (Dog)
        if (isLifeDogDead && isDeathDogDead) {
            SpawnBruteDogEnemy();
        }
        // Check if Final Set of Enemies are Dead (BruteDog)
        if (isLifeBruteDogDead && isDeathBruteDogDead) {
            // Disable Enemy Portals
            lifeEnemyPortal.SetActive(false);
            deathEnemyPortal.SetActive(false);
            // Enable Player Portals
            lifeEndPortal.SetActive(true);
            deathEndPortal.SetActive(true);
            isLifeBruteDogDead = false;
            isDeathBruteDogDead = false;
        }
    }
    // Spawns in a Mouse enemy on both sides once the firstEnemyGraphicDisplay Panel has been closed
    private void SpawnMouseEnemy() {
        // Re-enable Pausing 
        GameplayUIManager.canPauseGame = true;
        lifeSpawnInMouseEnemy.SetActive(true);
        deathSpawnInMouseEnemy.SetActive(true);
    }
    // Spawns in a Dog enemy on both sides once the Mice Enemies have both been eliminated
    private void SpawnDogEnemy() {
        lifeSpawnInDogEnemy.SetActive(true);
        deathSpawnInDogEnemy.SetActive(true);
        isLifeMouseDead = false;
        isDeathMouseDead = false;
    }
    // Spawns in a Brute Dog enemy on both sides once the Mice Enemies have both been eliminated
    private void SpawnBruteDogEnemy() {
        lifeSpawnInBruteDogEnemy.SetActive(true);
        deathSpawnInBruteDogEnemy.SetActive(true);
        isLifeDogDead = false;
        isDeathDogDead = false;
    }
    // This will get called once a player has picked up the pistol
    // Once both players have picked up the pistol, this will display a Graphic on-screen showcasing how to kill and enemy
    // "You can only damage enemies on the other side of the portal" | "Enemies on your side of the portal are immune to your bullets"
    private void DisplayFirstEnemyInfoGraphic() {
        if (lCPickedUpWeapon == true && dCPickedUpWeapon == true) {
            PlayerMovement.isPaused = true;
            lCWaitingPanel.SetActive(false);
            dCWaitingPanel.SetActive(false);
            otherPlayerRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            // Display the Graphic that explains how to damage/kill enemies
            firstEnemyInfoDisplayPanel.SetActive(true);
            // Disable Pausing 
            GameplayUIManager.canPauseGame = false;
            // Stop Time - so players cannot move
            Time.timeScale = 0;
        } else if (lCPickedUpWeapon == true) {
            lCWaitingPanel.SetActive(true);
            playerRB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; ;
        } else if (dCPickedUpWeapon == true) {
            dCWaitingPanel.SetActive(true);
            playerRB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation; ;
        }
    }
    private IEnumerator disableEquip() {
        yield return new WaitForSeconds(0.1f);
        equipBtnPressed = false;
    }
    private void OnEquipWeapon() {
        equipBtnPressed = true;
        StartCoroutine(disableEquip());
    }
    // Picking up Pistol - Tutorial only
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("PistolPickup") && gameObject.name == "LifeCat" && equipBtnPressed) {
            collision.gameObject.SetActive(false);
            playerPistolObj.SetActive(true);
            gameObject.GetComponent<WeaponManager>().hasGunInHand = true;
            playerPickUpWeaponPrompt.SetActive(false);
            playerShootPrompt.SetActive(true);
            gameObject.GetComponent<WeaponManager>().timeBetweenShots = 0.25f;
            lCPickedUpWeapon = true;
            DisplayFirstEnemyInfoGraphic();
        }
        else if (collision.gameObject.CompareTag("PistolPickup") && gameObject.name == "DeathCat" && equipBtnPressed) {
            collision.gameObject.SetActive(false);
            playerPistolObj.SetActive(true);
            gameObject.GetComponent<WeaponManager>().hasGunInHand = true;
            playerPickUpWeaponPrompt.SetActive(false);
            playerShootPrompt.SetActive(true);
            gameObject.GetComponent<WeaponManager>().timeBetweenShots = 0.25f;
            dCPickedUpWeapon = true;
            DisplayFirstEnemyInfoGraphic();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("LifeCat") && gameObject.name == "AttackTrigger") {
            // Mouse Attack and make LifeCat lose health
            deathFlyEnemyScript.attack = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        } else if (collision.CompareTag("DeathCat") && gameObject.name == "AttackTrigger") {
            // Mouse Attack and make DeathCat lose health
            lifeFlyEnemyScript.attack = true;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("LifeBullet") && gameObject.name == "LifeDog") {
            isLifeDogDead = true;
        }
        else if(collision.gameObject.CompareTag("DeathBullet") && gameObject.name == "DeathDog") {
            isDeathDogDead = true;
        }
    }
}