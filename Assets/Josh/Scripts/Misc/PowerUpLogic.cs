using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpLogic : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private GUIManager guiManagerScript;
    [SerializeField]
    private ItemSpawner itemSpawnerScript;
    [SerializeField]
    private DamagePlayer damagePlayerScript;
    [SerializeField]
    private Texture normalHeart;
    [SerializeField]
    private GameObject shieldObj;
    public static bool canLifeCatDoDoubleDamage = false;
    public static bool canDeathCatDoDoubleDamage = false;
    public bool lifeCatisShielded = false;
    public bool deathCatisShielded = false;

	private void Start() {
        canLifeCatDoDoubleDamage = false;
        canDeathCatDoDoubleDamage = false;
    }

	private void HealLifeCat() {
        // Play Healing SFX
        audioManagerScript.PlayerHealingAudio();
        guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health].GetComponent<RawImage>().texture = normalHeart;
        gameObject.GetComponent<PlayerMovement>().health += 1;
    }
    private void HealDeathCat() {
        // Play Healing SFX
        audioManagerScript.PlayerHealingAudio();
        guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health].GetComponent<RawImage>().texture = normalHeart;
        gameObject.GetComponent<PlayerMovement>().health += 1;
    }
    private IEnumerator ShieldPlayer() {  
        yield return new WaitForSeconds(6.0f);
        damagePlayerScript.canLoseHealth = true;
        // Ensure that the Player can collide with the enemies again as they are no longer immune
        if (gameObject.name == "LifeCat") {
            lifeCatisShielded = false;
            Physics2D.IgnoreLayerCollision(3, 10, false);
            shieldObj.SetActive(false);
        } else if (gameObject.name == "DeathCat") {
            deathCatisShielded = false;
            Physics2D.IgnoreLayerCollision(3, 9, false);
            shieldObj.SetActive(false);
        }
    }
    private void ShieldLifeCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        lifeCatisShielded = true;
        shieldObj.SetActive(true);
        damagePlayerScript.canLoseHealth = false;
        Physics2D.IgnoreLayerCollision(3, 10, true);
        StartCoroutine(ShieldPlayer());
    }
    private void ShieldDeathCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        deathCatisShielded = true;
        shieldObj.SetActive(true);
        damagePlayerScript.canLoseHealth = false;
        Physics2D.IgnoreLayerCollision(3, 9, true);
        StartCoroutine(ShieldPlayer());
    }
    private IEnumerator SpeedBoostPlayer() {
        yield return new WaitForSeconds(5.0f);
        gameObject.GetComponent<PlayerMovement>().movementSpeed -= 5;
    }
    private void SpeedBoostLifeCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        gameObject.GetComponent<PlayerMovement>().movementSpeed += 5;
        StartCoroutine(SpeedBoostPlayer());
    }
    private void SpeedBoostDeathCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        gameObject.GetComponent<PlayerMovement>().movementSpeed += 5;
        StartCoroutine(SpeedBoostPlayer());
    }
    private IEnumerator DoubleDamageOnEnemy() {
        yield return new WaitForSeconds(6.5f);
        // Ensure that the Player can no longer do double damage
        if (gameObject.name == "LifeCat") {
            canLifeCatDoDoubleDamage = false;
        } else if (gameObject.name == "DeathCat") {
            canDeathCatDoDoubleDamage = false;
        }
	}
    private void DoubleDmgLifeCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        canLifeCatDoDoubleDamage = true;
        StartCoroutine(DoubleDamageOnEnemy());
    }
    private void DoubleDmgDeathCat() {
        // Play PickupPowerup SFX
        audioManagerScript.PickupPowerupAudio();
        canDeathCatDoDoubleDamage = true;
        StartCoroutine(DoubleDamageOnEnemy());
    }

    // Checks which Power up trigger(s) both the Life Cat & Death Cat collide with.
    // Performs the Powerup Logic as well
    private void OnTriggerEnter2D(Collider2D collision) {
        // Health Powerup for Life Cat & Death Cat:
        if (collision.gameObject.name == "Health" && gameObject.name == "LifeCat" && gameObject.GetComponent<PlayerMovement>().health < 9) {
            // Check which index the Health was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.deathHealthCurrentList.Contains(collision.gameObject.transform.position)) {
                int lcHealthPositionIndex = itemSpawnerScript.deathHealthCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.deathHealthCurrentList.RemoveAt(lcHealthPositionIndex);
            }
            // Destroy Health Object
            Destroy(collision.gameObject);
            itemSpawnerScript.canSpawnDeathHealth = true; // Spawn Health on Death side - where the Life Cat is located at
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathHealth());
            HealLifeCat();
        } 
        else if (collision.gameObject.name == "Health" && gameObject.name == "DeathCat" && gameObject.GetComponent<PlayerMovement>().health < 9) {
            // Check which index the Health was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.lifeHealthCurrentList.Contains(collision.gameObject.transform.position)) {
                int dcHealthPositionIndex = itemSpawnerScript.lifeHealthCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.lifeHealthCurrentList.RemoveAt(dcHealthPositionIndex);
            }
            // Destroy Health Object
            Destroy(collision.gameObject);
            itemSpawnerScript.canSpawnLifeHealth = true; // Spawn Health on Life side - where the Life Cat is located at
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeHealth());
            HealDeathCat();
        }
        // Shield Powerup for Life Cat & Death Cat:
        if (collision.gameObject.name == "Shield" && gameObject.name == "LifeCat") {
            // Only pickup shield if the player isn't already shielded
            if (lifeCatisShielded == false) {
                // Check which index the Shield was in the current positions list and remove it so another can spawn in that position
                if (itemSpawnerScript.deathShieldCurrentList.Contains(collision.gameObject.transform.position)) {
                    int lcShieldPositionIndex = itemSpawnerScript.deathShieldCurrentList.IndexOf(collision.gameObject.transform.position);
                    itemSpawnerScript.deathShieldCurrentList.RemoveAt(lcShieldPositionIndex);
                }
                // Destroy Shield Object
                Destroy(collision.gameObject);
                itemSpawnerScript.canSpawnDeathShield = true; // Spawn Shield on Death side - where the Life Cat is located at
                itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathShield());
                ShieldLifeCat();
            }
        } 
        else if (collision.gameObject.name == "Shield" && gameObject.name == "DeathCat") {
            // Only pickup shield if the player isn't already shielded
            if (deathCatisShielded == false) {
                // Check which index the Shield was in the current positions list and remove it so another can spawn in that position
                if (itemSpawnerScript.lifeShieldCurrentList.Contains(collision.gameObject.transform.position)) {
                    int dcShieldPositionIndex = itemSpawnerScript.lifeShieldCurrentList.IndexOf(collision.gameObject.transform.position);
                    itemSpawnerScript.lifeShieldCurrentList.RemoveAt(dcShieldPositionIndex);
                }
                // Destroy Shield Object
                Destroy(collision.gameObject);
                itemSpawnerScript.canSpawnLifeShield = true; // Spawn Shield on Life side - where the Death Cat is located at
                itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeShield());
                ShieldDeathCat();
            }
          
        }
        // Speed Boost Powerup for Life Cat & Death Cat:
        if (collision.gameObject.name == "SpeedBoost" && gameObject.name == "LifeCat") {
            // Destroy Speed Boosts Collider2D to prevent the Life Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<Collider2D>());
            // Check which index the Speed Boost was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.deathSpeedBoostCurrentList.Contains(collision.gameObject.transform.position)) {
                int lcSpeedBoostPositionIndex = itemSpawnerScript.deathSpeedBoostCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.deathSpeedBoostCurrentList.RemoveAt(lcSpeedBoostPositionIndex);
            }
            // Destroy Speed Boost Object
            Destroy(collision.gameObject);
            itemSpawnerScript.canSpawnDeathSpeedBoost = true; // Spawn Speed Boost on Death side - where the Life Cat is located at
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathSpeedBoost());
            SpeedBoostLifeCat();
        } 
        else if (collision.gameObject.name == "SpeedBoost" && gameObject.name == "DeathCat") {
            // Destroy Speed Boosts Collider2D to prevent the Death Cat from entering the trigger twice on rare occasions
            Destroy(collision.GetComponent<Collider2D>());
            // Check which index the Speed Boost was in the current positions list and remove it so another can spawn in that position
            if (itemSpawnerScript.lifeSpeedBoostCurrentList.Contains(collision.gameObject.transform.position)) {
                int dcSpeedBoostPositionIndex = itemSpawnerScript.lifeSpeedBoostCurrentList.IndexOf(collision.gameObject.transform.position);
                itemSpawnerScript.lifeSpeedBoostCurrentList.RemoveAt(dcSpeedBoostPositionIndex);
            }
            // Destroy Speed Boost Object
            Destroy(collision.gameObject);
            itemSpawnerScript.canSpawnLifeSpeedBoost = true; // Spawn Speed Boost on Life side - where the Death Cat is located at
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeSpeedBoost());
            SpeedBoostDeathCat();
        }
		// Double Damage Powerup for Life Cat & Death Cat:
		if (collision.gameObject.name == "DoubleDamage" && gameObject.name == "LifeCat") {
			// Destroy Double Dmg Collider2D to prevent the Life Cat from entering the trigger twice on rare occasions
			Destroy(collision.GetComponent<PolygonCollider2D>());
			// Check which index the Double Dmg was in the current positions list and remove it so another can spawn in that position
			if (itemSpawnerScript.deathDoubleDmgCurrentList.Contains(collision.gameObject.transform.position)) {
				int lcDoubleDmgPositionIndex = itemSpawnerScript.deathDoubleDmgCurrentList.IndexOf(collision.gameObject.transform.position);
				itemSpawnerScript.deathDoubleDmgCurrentList.RemoveAt(lcDoubleDmgPositionIndex);
			}
			// Destroy Double Dmg Object
			Destroy(collision.gameObject);
			itemSpawnerScript.canSpawnDeathDoubleDmg = true; // Spawn Double Dmg on Death side - where the Life Cat is located at
			itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathDoubleDmg());
			DoubleDmgLifeCat();
		} 
        else if (collision.gameObject.name == "DoubleDamage" && gameObject.name == "DeathCat") {
			// Destroy Double Dmg Collider2D to prevent the Death Cat from entering the trigger twice on rare occasions
			Destroy(collision.GetComponent<PolygonCollider2D>());
			// Check which index the Double Dmg was in the current positions list and remove it so another can spawn in that position
			if (itemSpawnerScript.lifeDoubleDmgCurrentList.Contains(collision.gameObject.transform.position)) {
				int dcDoubleDmgPositionIndex = itemSpawnerScript.lifeDoubleDmgCurrentList.IndexOf(collision.gameObject.transform.position);
				itemSpawnerScript.lifeDoubleDmgCurrentList.RemoveAt(dcDoubleDmgPositionIndex);
			}
			// Destroy Double Dmg Object
			Destroy(collision.gameObject);
			itemSpawnerScript.canSpawnLifeDoubleDmg = true; // Spawn Double Dmg on Life side - where the Death Cat is located at
			itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeDoubleDmg());
			DoubleDmgDeathCat();
		}
	}
}