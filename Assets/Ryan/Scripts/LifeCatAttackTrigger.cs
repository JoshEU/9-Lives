using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCatAttackTrigger : MonoBehaviour {
    public DeathFlyEnemy[] enemyArray;


    //Collision detection
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("LifeCat")) {
            foreach (DeathFlyEnemy enemy in enemyArray) {
                enemy.attack = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("LifeCat")) {
            foreach (DeathFlyEnemy enemy in enemyArray) {
                enemy.attack = false;
            }
        }
    }
}