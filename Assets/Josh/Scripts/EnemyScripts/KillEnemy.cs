using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {
	private void OnCollisionEnter2D(Collision2D collision) {
        // Life Enemy Bullet Hit checks:
        if (gameObject.CompareTag("LifeBullet") && collision.gameObject.CompareTag("LifeEnemy") && collision.gameObject.CompareTag("Mouse")) {
            // Destroy Enemy and bullet upon collision & Play Mouse Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("LifeMouseTrigger", 3);
            Destroy(collision.gameObject);
            Destroy(gameObject);            
        }
        else if(gameObject.CompareTag("LifeBullet") && collision.gameObject.CompareTag("LifeEnemy") && collision.gameObject.CompareTag("Dog")) {
            // Destroy Enemy and bullet upon collision & Play Dog Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("LifeDogTrigger", 1);
            Destroy(collision.gameObject);
            Destroy(gameObject);        
        } 
        else if(gameObject.CompareTag("LifeBullet") && collision.gameObject.CompareTag("LifeEnemy") && collision.gameObject.CompareTag("BruteDog")) {
            // Destroy Enemy and bullet upon collision & Play Brute Dog Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("BruteLifeTrigger", 1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        // Death Enemy Bullet Hit checks:
        if (gameObject.CompareTag("DeathBullet") && collision.gameObject.CompareTag("DeathEnemy") && collision.gameObject.CompareTag("Mouse")) {
            // Destroy Enemy and bullet upon collision & Play Mouse Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("DeathMouseTrigger", 3);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } 
        else if (gameObject.CompareTag("DeathBullet") && collision.gameObject.CompareTag("DeathEnemy") && collision.gameObject.CompareTag("Dog")) {
            // Destroy Enemy and bullet upon collision & Play Dog Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("DeathDogTrigger", 1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } 
        else if (gameObject.CompareTag("DeathBullet") && collision.gameObject.CompareTag("DeathEnemy") && collision.gameObject.CompareTag("BruteDog")) {
            // Destroy Enemy and bullet upon collision & Play Brute Dog Animation
            collision.gameObject.GetComponent<Animator>().SetInteger("BruteDeathTrigger", 1);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}