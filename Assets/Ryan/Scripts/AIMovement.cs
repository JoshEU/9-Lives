using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
    //Movement left and right
    private Rigidbody2D enemy;
    public float speed = 3;

    private void EnemyMovement() {
        enemy.velocity = new Vector2 (speed , 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("RWALL")) {
            speed *= -1;
        }
        if (collision.gameObject.CompareTag("LWALL")) {
            speed *= -1;
        }   
    }

	// Start is called before the first frame update
	void Start(){
        enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        EnemyMovement();
    }
}
