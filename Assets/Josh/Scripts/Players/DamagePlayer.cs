using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DamagePlayer : MonoBehaviour {
	[SerializeField]
	private AudioManager audioManagerScript;
	[SerializeField]
    private GUIManager guiManagerScript;
	private ItemSpawner itemSpawnerScript;
	[SerializeField]
	private PowerUpLogic powerUpLogicScript;
	[SerializeField]
	private Texture fadedHeart;
	private SpriteRenderer playerSpriteRenderer;
	private Color spriteColor = Color.white;

	public GameObject enemyObj;
	public float knockbackStrength = 10.0f;
	public bool canLoseHealth = true;
	public float immunityTime = 1.0f;

	public static bool lifeCatDied = false;
	public static bool deathCatDied = false;

	private void Awake() {
		// Finds the Gameobject with the ItemSpawner.cs script attached immediately after the Player has spawned in
		itemSpawnerScript = (ItemSpawner)GameObject.FindObjectOfType(typeof(ItemSpawner));
	}
	void Start() {
		playerSpriteRenderer = GetComponent<SpriteRenderer>();
		// Reset the player state so they can take damage again after restarting the game from dying
		if (gameObject.name == "LifeCat") {
			Physics2D.IgnoreLayerCollision(3, 10, false);
		} else if (gameObject.name == "DeathCat") {
			Physics2D.IgnoreLayerCollision(3, 9, false);
		}
	}
    void Update() {
		// Set player color to be the spriteColor every frame -> this will allow the player to change transparency when damaged
		playerSpriteRenderer.color = spriteColor;
	}
	// Gets the Players Sprite Renderer Color
	// Fades it Out 
	// Waits a short time interval
	// Fades it back in
	// Creates effect of being immune to any damage
	private IEnumerator FadingDelay() {
		float fadeOut = 0.5f;
		float fadeIn = 1.0f;
		// Fade the player that took damage in and out as long as canLoseHealth is set to false
		while (canLoseHealth == false) {
			// Ensure that the Player cannot collide with the enemies whilst they are immune
			if (gameObject.name == "LifeCat") {
				Physics2D.IgnoreLayerCollision(3, 10, true);
			}
			else if(gameObject.name == "DeathCat") {
				Physics2D.IgnoreLayerCollision(3, 9, true);
			}
			// Fade in & out	
			spriteColor.a = fadeOut;
			yield return new WaitForSeconds(0.25f);
			spriteColor.a = fadeIn;
			yield return new WaitForSeconds(0.25f);
		}
		// Ensure that the Player can collide with the enemies again as they are no longer immune - only does this if the player has not picked up a shield whilst they were immune
		if (gameObject.name == "LifeCat" && powerUpLogicScript.lifeCatisShielded == false) {
			Physics2D.IgnoreLayerCollision(3, 10, false);
		} 
		else if (gameObject.name == "DeathCat" && powerUpLogicScript.deathCatisShielded == false) {
			Physics2D.IgnoreLayerCollision(3, 9, false);
		}
	}

	// Makes player immune to damage for a set amount of time
	// Called once the player takes damage from an enemy
	private IEnumerator ImmuneToDamageDelay() {
		canLoseHealth = false;
		StartCoroutine(FadingDelay());
		yield return new WaitForSeconds(immunityTime);
		canLoseHealth = true;
	}

	private void KnockBackPlayer() {
		audioManagerScript.PlayerHurtAudio();
		Vector2 direction = (gameObject.transform.position - enemyObj.gameObject.transform.position ).normalized;
		gameObject.GetComponent<Rigidbody2D>().AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
	}
	private IEnumerator GameOverSceneDelay() {
		audioManagerScript.StopGameplayMusicAudio();
		audioManagerScript.StopTutorialMusicAudio();
		audioManagerScript.GameOverGaspAudio();
		yield return new WaitForSeconds(2.0f);
		WaveManager.LoadGameOverScene();
	}
	private void GameOverState() {
		// Destroy all remaining LifeEnemies
		Destroy(itemSpawnerScript.lifeEnemyParentObj);
		// Destroy all remaining LifePowerups
		Destroy(itemSpawnerScript.lifePowerUpParentObj);
		// Destroy all remaining DeathEnemies
		Destroy(itemSpawnerScript.deathEnemyParentObj);
		// Destroy all remaining DeathPowerups
		Destroy(itemSpawnerScript.deathPowerUpParentObj);
		StartCoroutine(GameOverSceneDelay());
	}

	private void OnCollisionStay2D(Collision2D collision) {
		if (canLoseHealth == true) {
			// Mouse Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("Mouse") && gameObject.CompareTag("LifeCat")) {
				// Remove 1 heart from the Life Cat (Right UI) & Knock them back & Make them immune [depending on the enemy that damaged them]

				// Set the enemyObj to be the enemy gameObject that the Player collided with
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				gameObject.GetComponent<PlayerMovement>().health -= 1;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();

				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("Mouse") && gameObject.CompareTag("DeathCat")) {
				// Remove 1 heart from the Death Cat (Right UI) & Knock them back & Make them immune [depending on the enemy that damaged them]

				// Set the enemyObj to be the enemy gameObject that the Player collided with
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				gameObject.GetComponent<PlayerMovement>().health -= 1;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			}
			// Dog Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("Dog") && gameObject.CompareTag("LifeCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();
				
				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if(gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
				}
				
				gameObject.GetComponent<PlayerMovement>().health -= 2;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("Dog") && gameObject.CompareTag("DeathCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
				}
				gameObject.GetComponent<PlayerMovement>().health -= 2;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}

				StartCoroutine(ImmuneToDamageDelay());
			}
			// Brute Dog Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("BruteDog") && gameObject.CompareTag("LifeCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
					if (gameObject.GetComponent<PlayerMovement>().health - 3 > -1) {
						guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 3].GetComponent<RawImage>().texture = fadedHeart;
					}
				}
				gameObject.GetComponent<PlayerMovement>().health -= 3;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("BruteDog") && gameObject.CompareTag("DeathCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
					if (gameObject.GetComponent<PlayerMovement>().health - 3 > -1) {
						guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 3].GetComponent<RawImage>().texture = fadedHeart;
					}
				}
				gameObject.GetComponent<PlayerMovement>().health -= 3;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			}
		}
	}
	private void OnCollisionEnter2D(Collision2D collision) {
		// Could store health as variable and then Find the Gameobject with the name "heart_" + cat health integer + 1 -- then just set that active to regain health again
		// Also have a variable to specify how much DAMAGE an enemy does - amount of hearts removed should correspond to this - might need a list of most recent hearts removed
		if (canLoseHealth == true) {
			// Mouse Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("Mouse") && gameObject.CompareTag("LifeCat")) {
				// Remove 1 heart from the Life Cat (Right UI) & Knock them back & Make them immune [depending on the enemy that damaged them]

				// Set the enemyObj to be the enemy gameObject that the Player collided with
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				gameObject.GetComponent<PlayerMovement>().health -= 1;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("Mouse") && gameObject.CompareTag("DeathCat")) {
				// Remove 1 heart from the Death Cat (Right UI) & Knock them back & Make them immune [depending on the enemy that damaged them]

				// Set the enemyObj to be the enemy gameObject that the Player collided with
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				gameObject.GetComponent<PlayerMovement>().health -= 1;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			}
			// Dog Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("Dog") && gameObject.CompareTag("LifeCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
				}

				gameObject.GetComponent<PlayerMovement>().health -= 2;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("Dog") && gameObject.CompareTag("DeathCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
				}
				gameObject.GetComponent<PlayerMovement>().health -= 2;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}

				StartCoroutine(ImmuneToDamageDelay());
			}
			// Brute Dog Enemy Damage for Life Cat & Death Cat:
			if (collision.gameObject.CompareTag("BruteDog") && gameObject.CompareTag("LifeCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
					if (gameObject.GetComponent<PlayerMovement>().health - 3 > -1) {
						guiManagerScript.lifeCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 3].GetComponent<RawImage>().texture = fadedHeart;
					}
				}
				gameObject.GetComponent<PlayerMovement>().health -= 3;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					lifeCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			} else if (collision.gameObject.CompareTag("BruteDog") && gameObject.CompareTag("DeathCat")) {
				enemyObj = collision.gameObject;
				KnockBackPlayer();

				guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 1].GetComponent<RawImage>().texture = fadedHeart;
				if (gameObject.GetComponent<PlayerMovement>().health - 2 > -1) {
					guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 2].GetComponent<RawImage>().texture = fadedHeart;
					if (gameObject.GetComponent<PlayerMovement>().health - 3 > -1) {
						guiManagerScript.deathCatHealthImages[gameObject.GetComponent<PlayerMovement>().health - 3].GetComponent<RawImage>().texture = fadedHeart;
					}
				}
				gameObject.GetComponent<PlayerMovement>().health -= 3;

				if (gameObject.GetComponent<PlayerMovement>().health <= 0) {
					// Make players fall off screen
					Physics2D.IgnoreLayerCollision(3, 13, true);
					Physics2D.IgnoreLayerCollision(3, 14, true);
					deathCatDied = true;
					GameOverState();
				}
				StartCoroutine(ImmuneToDamageDelay());
			}
		}
	}
}