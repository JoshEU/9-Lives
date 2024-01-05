using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private WeaponManager weaponManagerScript;
    [SerializeField]
    private GameObject bulletObj;
    [SerializeField]
    private GameObject pistolBulletSpawnObj;
    [SerializeField]
    private GameObject smgBulletSpawnObj;
    [SerializeField]
    private GameObject assaultRifleBulletSpawnObj;
    [SerializeField]
    private Sprite doubleDmgSprite;

    private GameObject newBulletObj;
    public float bulletSpeed = 15.0f;
    private Vector2 shootDirection;
    private float shootTrigger;
    private float nextFireTime;

    // Returns the time delay between shots determining the guns fire rate
    private bool canFire {
      get { return Time.time > nextFireTime; }
	}
    // Referencing Input Action Asset [Shoot]
    private void OnFire(InputValue value) {
        if (weaponManagerScript.hasGunInHand && canFire) {
            // Play Shoot Audio
            audioManagerScript.PlayerShootAudio();
            // Sets the delay for the fire rate of the gun equipped
            nextFireTime = Time.time + weaponManagerScript.timeBetweenShots;
            shootTrigger = value.Get<float>();
            // LIFE CAT: Spawns a bullet on Right Gun OR Left Gun muzzle (which ever gun is visible)
            if (gameObject.CompareTag("LifeCat")) {
                // Adjust the location of where the bullets will spawn based on the current weapon equipped
                if (WeaponManager.lifeCatEquippedWeapon == "Pistol") {
                    newBulletObj = Instantiate(bulletObj, pistolBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the Life Cats Bullets into fish
                    if(PowerUpLogic.canLifeCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite= doubleDmgSprite;
					}
                }
                if (WeaponManager.lifeCatEquippedWeapon == "Smg") {
                    newBulletObj = Instantiate(bulletObj, smgBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the Life Cats Bullets into fish
                    if (PowerUpLogic.canLifeCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite = doubleDmgSprite;
                    }
                }
                if (WeaponManager.lifeCatEquippedWeapon == "AssaultRifle") {
                    newBulletObj = Instantiate(bulletObj, assaultRifleBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the Life Cats Bullets into fish
                    if (PowerUpLogic.canLifeCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite = doubleDmgSprite;
                    }
                }
            }
            // DEATH CAT: Spawns a bullet on Right Gun OR Left Gun muzzle (which ever gun is visible)
            if (gameObject.CompareTag("DeathCat")) {
                // Adjust the location of where the bullets will spawn based on the current weapon equipped
                if (WeaponManager.deathCatEquippedWeapon == "Pistol") {
                    newBulletObj = Instantiate(bulletObj, pistolBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the DeathCats Bullets into fish
                    if (PowerUpLogic.canDeathCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite = doubleDmgSprite;
                    }
                }
                if (WeaponManager.deathCatEquippedWeapon == "Smg") {
                    newBulletObj = Instantiate(bulletObj, smgBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the DeathCats Bullets into fish
                    if (PowerUpLogic.canDeathCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite = doubleDmgSprite;
                    }
                }
                if (WeaponManager.deathCatEquippedWeapon == "AssaultRifle") {
                    newBulletObj = Instantiate(bulletObj, assaultRifleBulletSpawnObj.transform.position, Quaternion.identity);
                    // If Double Damage is Active, turn the DeathCats Bullets into fish
                    if (PowerUpLogic.canDeathCatDoDoubleDamage == true) {
                        newBulletObj.GetComponent<SpriteRenderer>().sprite = doubleDmgSprite;
                    }
                }
            }
            // Set Bullets direction to be the same as the players facing direction
            newBulletObj.transform.SetPositionAndRotation(newBulletObj.transform.position, Quaternion.identity);
            newBulletObj.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletSpeed;
        }
    }
    void Update() {
        // Life Cat Shooting Direction
        if (gameObject.CompareTag("LifeCat") && gameObject.GetComponent<PlayerMovement>().isFacingRight == true) {
            shootDirection = Vector2.right;
            if(newBulletObj != null) {
                newBulletObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        } else if (gameObject.CompareTag("LifeCat") && gameObject.GetComponent<PlayerMovement>().isFacingRight == false) {
            shootDirection = Vector2.left;
        }
        // Death Cat Shooting Direction
        if (gameObject.CompareTag("DeathCat") && gameObject.GetComponent<PlayerMovement>().isFacingRight == true) {
            shootDirection = Vector2.right;
            if (newBulletObj != null) {
                newBulletObj.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        } else if (gameObject.CompareTag("DeathCat") && gameObject.GetComponent<PlayerMovement>().isFacingRight == false) {
            shootDirection = Vector2.left;
        }
    }
}