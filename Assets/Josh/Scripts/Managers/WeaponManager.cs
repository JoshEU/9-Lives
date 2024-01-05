using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private ItemSpawner itemSpawnerScript;

    public static string lifeCatEquippedWeapon = "";
    public static string deathCatEquippedWeapon = "";

    public static bool canSpawnPistol = false;
    public static bool canSpawnSmg = false;
    public static bool canSpawnAssaultRifle = false;

    public bool hasGunInHand = false;
    public float timeBetweenShots;
    public bool equipBtnPressed = false;

    [SerializeField]
    private GameObject lifeCatPistol;
    [SerializeField]
    private GameObject lifeCatSmg;
    [SerializeField]
    private GameObject lifeCatAssaultRifle;
    [SerializeField]
    private GameObject deathCatPistol;
    [SerializeField]
    private GameObject deathCatSmg;
    [SerializeField]
    private GameObject deathCatAssaultRifle;

    // Check which weapon the Life & Death Cat had equipped in the previous wave and carry it over onto the current wave
    private void EquippedWeaponCheck() {
        // Check Life Cat Equipped Weapon
        if (lifeCatEquippedWeapon == "Pistol" && gameObject.CompareTag("LifeCat")) {
            lifeCatPistol.SetActive(true);
        } else if (lifeCatEquippedWeapon == "Smg" && gameObject.CompareTag("LifeCat")) {
            lifeCatSmg.SetActive(true);
        } else if (lifeCatEquippedWeapon == "AssaultRifle" && gameObject.CompareTag("LifeCat")) {
            lifeCatAssaultRifle.SetActive(true);
        }
        // Check Death Cat Equipped Weapon
        if (deathCatEquippedWeapon == "Pistol" && gameObject.CompareTag("DeathCat")) {
            deathCatPistol.SetActive(true);
        } else if (deathCatEquippedWeapon == "Smg" && gameObject.CompareTag("DeathCat")) {
            deathCatSmg.SetActive(true);
        } else if (deathCatEquippedWeapon == "AssaultRifle" && gameObject.CompareTag("DeathCat")) {
            deathCatAssaultRifle.SetActive(true);
        }
    }
	private void Start() {
        EquippedWeaponCheck();
    }
    private IEnumerator disableEquip() {
        yield return new WaitForSeconds(0.1f);
        equipBtnPressed = false;
    }
	private void OnEquipWeapon() {
        equipBtnPressed = true;
        StartCoroutine(disableEquip());
	}
	// Picking up the weapon - have pickup binding be Y and then when it is true have a trigger checking if you are in  the trigger and ht epickup bool is true then pickup weapon - set it active and set the current one to inactive
	// Equips the weapon by pressing 'Y' on the controller and being within its trigger.
	// Unequips the other weapons and Destroys the weapon pickup weapon object
	// Adjusts the fire rate based on which weapon is equipped
	private void OnTriggerStay2D(Collider2D collision) {
        // Life Cat Weapon Pickups:
        if (collision.gameObject.CompareTag("PistolPickup") && gameObject.name == "LifeCat" && equipBtnPressed) {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Life Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());
            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.deathWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int lcWeaponPositionIndex = itemSpawnerScript.deathWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.deathWeaponCurrentList.RemoveAt(lcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            lifeCatPistol.SetActive(true);
            lifeCatSmg.SetActive(false);
            lifeCatAssaultRifle.SetActive(false);
            lifeCatEquippedWeapon = "Pistol";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.35f;
            itemSpawnerScript.canSpawnDeathWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathWeapon()); // Spawn Weapon on Death side - where the Life Cat is located at
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("SmgPickup")  && gameObject.name == "LifeCat" && equipBtnPressed) {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Life Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());

            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.deathWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int lcWeaponPositionIndex = itemSpawnerScript.deathWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.deathWeaponCurrentList.RemoveAt(lcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            lifeCatPistol.SetActive(false);
            lifeCatSmg.SetActive(true);
            lifeCatAssaultRifle.SetActive(false);
            lifeCatEquippedWeapon = "Smg";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.20f;
            itemSpawnerScript.canSpawnDeathWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathWeapon()); // Spawn Weapon on Death side - where the Life Cat is located at
            Destroy(collision.gameObject);
        } 
        else if (collision.gameObject.CompareTag("AssaultRiflePickup") && equipBtnPressed && gameObject.name == "LifeCat") {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Life Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());

            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.deathWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int lcWeaponPositionIndex = itemSpawnerScript.deathWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.deathWeaponCurrentList.RemoveAt(lcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            lifeCatPistol.SetActive(false);
            lifeCatAssaultRifle.SetActive(true);
            lifeCatSmg.SetActive(false);
            lifeCatEquippedWeapon = "AssaultRifle";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.25f;
            itemSpawnerScript.canSpawnDeathWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathWeapon()); // Spawn Weapon on Death side - where the Life Cat is located at
            Destroy(collision.gameObject);
		} 
        // Death Cat Weapon Pickups:
        if (collision.gameObject.CompareTag("PistolPickup") && gameObject.name == "DeathCat" && equipBtnPressed) {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Death Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());

            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.lifeWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int dcWeaponPositionIndex = itemSpawnerScript.lifeWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.lifeWeaponCurrentList.RemoveAt(dcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            deathCatPistol.SetActive(true);
            deathCatSmg.SetActive(false);
            deathCatAssaultRifle.SetActive(false);
            deathCatEquippedWeapon = "Pistol";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.35f;
            itemSpawnerScript.canSpawnLifeWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeWeapon()); // Spawn Weapon on Life side - where the Death Cat is located at
            Destroy(collision.gameObject);
        } 
        else if (collision.gameObject.CompareTag("SmgPickup") && equipBtnPressed && gameObject.name == "DeathCat") {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Death Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());

            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.lifeWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int dcWeaponPositionIndex = itemSpawnerScript.lifeWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.lifeWeaponCurrentList.RemoveAt(dcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            deathCatPistol.SetActive(false);
            deathCatSmg.SetActive(true);
            deathCatAssaultRifle.SetActive(false);
            deathCatEquippedWeapon = "Smg";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.20f;
            itemSpawnerScript.canSpawnLifeWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeWeapon()); // Spawn Weapon on Life side - where the Death Cat is located at
            Destroy(collision.gameObject);
        } 
        else if (collision.gameObject.CompareTag("AssaultRiflePickup") && equipBtnPressed && gameObject.name == "DeathCat") {
            audioManagerScript.EquipWeaponAudio();
            // Destroy Weapon Collider2D to prevent the Death Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<CircleCollider2D>());

            // Check which index the Weapon was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.lifeWeaponCurrentList.Contains(collision.gameObject.transform.position)) {
                int dcWeaponPositionIndex = itemSpawnerScript.lifeWeaponCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.lifeWeaponCurrentList.RemoveAt(dcWeaponPositionIndex);
            }
            // Enable the Weapon being picked up and disable the others
            deathCatPistol.SetActive(false);
            deathCatAssaultRifle.SetActive(true);
            deathCatSmg.SetActive(false);
            deathCatEquippedWeapon = "AssaultRifle";
            hasGunInHand = true;
            // Alter fire rate
            timeBetweenShots = 0.25f;
            itemSpawnerScript.canSpawnLifeWeapon = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeWeapon()); // Spawn Weapon on Life side - where the Death Cat is located at
            Destroy(collision.gameObject);
        } 
    }
}