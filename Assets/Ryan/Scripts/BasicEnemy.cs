using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {
    //Movement left and right
    private Rigidbody2D enemy;
    [SerializeField] private Transform start;
    [SerializeField] private float speed = 3;
    public static float basicEnemyHealth;

    //Updating the enemies movement every frame (only moving on the x axis)
    private void EnemyMovement() {
        enemy.velocity = new Vector2 (speed , 0);
    }

    //Collision detection
    private void OnCollisionEnter2D(Collision2D collision) {
        //if enemy hits right wall
        if (collision.gameObject.CompareTag("RWALL")) {
            //speed is timesed by -1 so it goes the other direction
            speed *= -1;
        }
        //if enemy hits left wall
        if (collision.gameObject.CompareTag("LWALL")) {
            //speed is timesed by -1 so it goes the other direction
            speed *= -1;
        }

        if (collision.gameObject.CompareTag("Player")) {
            //health is taken away
            basicEnemyHealth -= 1f;
            Player.health -= 0.1f;
        }

    }

    private void RespawnEnemy(){
        Instantiate(enemy, start);
        Destroy(enemy.gameObject);
        
    }

    // Start is called before the first frame update
    void Start(){
        //finding the enemy in the scene
        enemy = GetComponent<Rigidbody2D>();
        basicEnemyHealth = 1;
    }

    // Update is called once per frame
    void Update(){
        //update enemy movement
        EnemyMovement();
        if (basicEnemyHealth == 0) {
            basicEnemyHealth = 1;
            RespawnEnemy();

        }
    }
}
