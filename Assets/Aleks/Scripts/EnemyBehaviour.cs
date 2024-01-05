using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enemy logic for health to test health display

public class EnemyBehaviour : MonoBehaviour {
    [SerializeField] public static float EnemyHP;
    public static float EnemyMaxHP = 1.0f;


    private void Start() {
        //set health to max
        EnemyHP = EnemyMaxHP;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //if it collides with player damage
        if(other.tag == "cat") {
            EnemyHP = EnemyHP - 0.1f;
        }
    }
}
