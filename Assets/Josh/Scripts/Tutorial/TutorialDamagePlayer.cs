using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDamagePlayer : MonoBehaviour {
    [SerializeField]
    private GameObject lifeMouseEnemy;
    [SerializeField]
    private GameObject deathMouseEnemy;
    [SerializeField]
    private GameObject invisibleLifeWall;
    [SerializeField]
    private GameObject invisibleDeathWall;
    [SerializeField]
    private GameObject lifeHealthObj;
    [SerializeField]
    private GameObject deathHealthObj;
    [SerializeField]
    private GameObject healPrompt;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("DeathCat")) {
            Destroy(lifeMouseEnemy);
            Destroy(invisibleLifeWall);
            deathHealthObj.SetActive(true);
            healPrompt.SetActive(true);
        } else if (collision.gameObject.CompareTag("LifeCat")) {
            Destroy(deathMouseEnemy);
            Destroy(invisibleDeathWall);
            lifeHealthObj.SetActive(true);
            healPrompt.SetActive(true);
        }

    }
    // This will make the Heal prompt text disappear once the player heals themselves
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("LifeCat") && gameObject.name == "Health") {
            healPrompt.SetActive(false);
        } else if (collision.gameObject.CompareTag("DeathCat") && gameObject.name == "Health") {
            healPrompt.SetActive(false);
        }
    }
}