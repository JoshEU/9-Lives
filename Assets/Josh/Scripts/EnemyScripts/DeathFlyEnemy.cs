using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlyEnemy : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    GameObject deathFlyEnemyObj;
    [SerializeField]
    private Animator deathAnim;
    [SerializeField]
    private GameObject healthBarObj;
    public float speed = 0f;
    private GameObject player;
    public bool attack = false;
    public Transform start;
    private Rigidbody2D enemy;
    public int flyEnemyHealth;
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
            StartCoroutine(DeathMouseDeathAnim());
        } else if (gameObject.CompareTag("Dog")) {
            audioManagerScript.DogDeathAudio();
            StartCoroutine(DeathDogDeathAnim());
        } else if (gameObject.CompareTag("BruteDog")) {
            audioManagerScript.BruteDogDeathAudio();
            StartCoroutine(DeathBruteDogDeathAnim());
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("DeathBullet")) {
            if (PowerUpLogic.canDeathCatDoDoubleDamage == true) {
                // Remove double health from enemy
                flyEnemyHealth -= 2;
            } else if (PowerUpLogic.canDeathCatDoDoubleDamage == false) {
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
                if (EnemyManager.numOfDeathEnemiesKilled < EnemyManager.maxNumOfDeathEnemiesToKill) {
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
            Tutorial.isDeathMouseDead = true;
        } else if (gameObject.CompareTag("Dog")) {
            Tutorial.isDeathDogDead = true;
        } else if (gameObject.CompareTag("BruteDog")) {
            Tutorial.isDeathBruteDogDead = true;
        }
        EnemyManager.numOfDeathEnemiesKilled += 1;
    }
    private void RespawnEnemy() {
        EnemyManager.numOfDeathEnemiesKilled += 1;
        // Stop spawning Death Enemies if the Death Cat has just killed the final one in the wave
        if (EnemyManager.numOfDeathEnemiesKilled >= EnemyManager.maxNumOfDeathEnemiesToKill) {
            itemSpawnerScript.canSpawnDeathEnemy = false;
            // Destroy all remaining DeathEnemies
            Destroy(itemSpawnerScript.deathEnemyParentObj);
        }
        // Spawn another Death Enemy
        else if (EnemyManager.numOfDeathEnemiesKilled < EnemyManager.maxNumOfDeathEnemiesToKill) {
            itemSpawnerScript.canSpawnDeathEnemy = true;
            itemSpawnerScript.StartCoroutine(itemSpawnerScript.SpawnDeathEnemy());
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
        player = GameObject.FindGameObjectWithTag("LifeCat");
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
    private IEnumerator DeathMouseDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f);
        Destroy(gameObject);
    }
    private IEnumerator DeathDogDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(1.0f); 
        Destroy(gameObject);
    }
    private IEnumerator DeathBruteDogDeathAnim() {
        deathAnim.SetBool("isDead", true);
        yield return new WaitForSeconds(2.0f); 
        Destroy(gameObject);
    }
}