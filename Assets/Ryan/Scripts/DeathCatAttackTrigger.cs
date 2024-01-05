using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCatAttackTrigger : MonoBehaviour {
    public LifeFlyEnemy[] enemyArray;


    //Collision detection
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("DeathCat")) {
            foreach (LifeFlyEnemy enemy in enemyArray) {
                enemy.attack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("DeathCat")) {
            foreach (LifeFlyEnemy enemy in enemyArray) {
                enemy.attack = false;
            }
        }
    }
}
