using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMove : MonoBehaviour {
    public float speed = 0f;
    private GameObject player;
    public bool attack = false;
    public Transform start;
    private Rigidbody2D enemy;
    public static float flyEnemyHealth;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            //health is taken away
            flyEnemyHealth -= 1;
            Player.health -= 1;
        }
    }

    private void RespawnEnemy(){
        Destroy(enemy.gameObject);

    }

    private void AttackPlayer() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    
    private void EnemyRotate() {
        if (transform.position.x > player.transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } 
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void GoToStart() {
        transform.position = Vector2.MoveTowards(transform.position,start.position, speed * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player");
        flyEnemyHealth = 1;
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        if (player == null)
            return;

        if (attack == true)
            AttackPlayer();

        else
            GoToStart();
        EnemyRotate();

        if (flyEnemyHealth == 0){
            RespawnEnemy();
        }
    }
}


