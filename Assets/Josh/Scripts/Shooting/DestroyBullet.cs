using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
    // Destroys Bullets when they hit the other sides ground
    private void OnCollisionEnter2D(Collision2D collision) {
        if (gameObject.CompareTag("LifeBullet") && collision.gameObject.CompareTag("LifeGround")) {
            Destroy(gameObject);
        } 
        else if (gameObject.CompareTag("DeathBullet") && collision.gameObject.CompareTag("LifeGround")) {
            Destroy(gameObject);
        }
        else {
            // Will destroy bullet after 0.25 seconds if it has hit a collider in the game but hasn't despawned yet.
            Destroy(gameObject);
		}
    }
    // Destroys Bullets when out of camera view (When it collides with outer bounds triggers)
    private void OnTriggerEnter2D(Collider2D collision) {
		 if (collision.gameObject.CompareTag("OutOfBounds")) {
            Destroy(gameObject);
        }
    }
}