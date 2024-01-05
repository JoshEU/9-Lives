using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFlyEnemy : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private GameObject lifeFlyEnemyObj;
    [SerializeField]
    private Animator deathAnim;
    [SerializeField]
    private GameObject healthBarObj; 
    public float speed = 0f;
    private GameObject player;
    public bool attack = false;
    public Transform start;
    private Rigidbody2D enemy;
    public int flyEnemyHealth = 0;
    public static bool respawnFlyEnemy;
	private ItemSpawner itemSpawnerScript;

    private void Awake() {
        // Finds the Gameobject with the ItemSpawner.cs script attached immediately after the Enemy has spawned in
        itemSpawnerScript = (ItemSpawner)GameObject.FindObjectOfType(typeof(ItemSpawner));
        // Finds the Gameobject with the AudioManager.cs script attached immediately after the Enemy has spawned in
        audioManagerScript = (AudioManager)GameObject.FindObjectOfType(typeof(AudioManager));
    }
    // Used to Play particular Audio Cues for certain enemies as well as their death animations
    private void CheckEnemyType() {
        if (gameObject.CompareTag("Mouse")) {
            audioManagerScript.MouseDeathAudio();
            StartCoroutine(LifeMouseDeathAnim());
        } else if (gameObject.CompareTag("Dog")) {
            audioManagerScript.DogDeathAudio();
            StartCoroutine(LifeDogDeathAnim());
        } else if (gameObject.CompareTag("BruteDog")) {
            audioManagerScript.BruteDogDeathAudio();
            StartCoroutine(LifeBruteDogDeathAnim());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        // Check for collisions with LifeBullets
        if (collision.gameObject.CompareTag("LifeBullet")) {
			if (PowerUpLogic.canLifeCatDoDoubleDamage == true) {
                // Remove double health from enemy
                flyEnemyHealth -= 2;
			}
            else if (PowerUpLogic.canLifeCatDoDoubleDamage == false) {
                // Remove 1 health from Enemy
                flyEnemyHealth -= 1;
            }
            if (flyEnemyHealth <= 0) {
                // Disable Collision with Player
                gameObject.GetComponent<Collider2D>().enabled = false;
                // Destroy HealthBar
                Destroy(healthBarObj);
                // Play Enemy Death SFX 
                CheckEnemyType();
                if (EnemyManager.numOfLifeEnemiesKilled < EnemyManager.maxNumOfLifeEnemiesToKill) {
                    if (GameStateManager.currentState != "Tutorial") { 
                        respawnFlyEnemy = true;
                        RespawnEnemy();
                    } else if (GameStateManager.currentState == "Tutorial") {
                        TutorialCheckEnemyKilled();
                    }
                }
            }
        }
    }
    private void TutorialCheckEnemyKilled() {
        if (gameObject.CompareTag("Mouse")) {
            Tutorial.isLifeMouseDead = true;
        } else if (gameObject.CompareTag("Dog")) {
            Tutorial.isLifeDogDead = true;
        } else if (gameObject.CompareTag("BruteDog")) {
            Tutorial.isLifeBruteDogDead = true;
        }
        EnemyManager.numOfLifeEnemiesKilled += 1;
    }
    private void RespawnEnemy() {
        EnemyManager.numOfLifeEnemiesKilled += 1;
        // Stop spawning Life Enemies if the Life Cat has just killed the final one in the wave
        if (EnemyManager.numOfLifeEnemiesKilled >= EnemyManager.maxNumOfLifeEnemiesToKill) {
            itemSpawnerScript.canSpawnLifeEnemy = false;
            // Destroy all remaining LifeEnemies
            Destroy(itemSpawnerScript.lifeEnemyParentObj);
        }
        // Spawn another Life Enemy
        else if(EnemyManager.numOfLifeEnemiesKilled < EnemyManager.maxNumOfLifeEnemiesToKill) {
            itemSpawnerScript.canSpawnLifeEnemy = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnLifeEnemy());
        }
    }

    private void AttackPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void EnemyRotate() {
        if (transform.position.x > player.transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        } else {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void Start() {
        player = GameObject.FindGameObjectWithTag("DeathCat");
        enemy = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (player == null) { 
            return;
        }
        if (attack == true) { 
            AttackPlayer();
            EnemyRotate();
        } 
    }
    private IEnumerator LifeMouseDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    private IEnumerator LifeDogDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    private IEnumerator LifeBruteDogDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}