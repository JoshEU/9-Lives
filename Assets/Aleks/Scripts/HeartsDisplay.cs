using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//references
//https://www.youtube.com/watch?v=uqGkNTFzYXM
//https://www.aleksandrhovhannisyan.com/blog/invulnerability-frames-in-unity/

public class HeartsDisplay : MonoBehaviour {
    //at the start health and max health should be the same
    private int health = 3;
    private int maxHealth = 3;
    private bool isImmune = false;
    private float invinDuration = 1.5f;

    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Image[] hearts;
    [SerializeField] private SpriteRenderer player;
    

    void Update() {
        //shows hearts on screen - also takes away hearts upon damage
        DisplayHearts();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //anything tagged with damage will take away a heart
        if (other.tag == "damage") {
            Damage(1);
            StartCoroutine(Flash());
        }
        //healing sources are currently tagged with fish
        //both tags can be changed 
        else if (other.tag == "fish") {
            //heal player + destroy collectible
            health = health + 1;
            Destroy(other.gameObject);
        }
    }

    //makes player flash red after taking damage
    IEnumerator Flash() {
        //flash player red 
        player.color = Color.red;
        yield return new WaitForSeconds(invinDuration);
        player.color = Color.blue;
    }

    //grants invincibility for set amount of seconds
    private IEnumerator TempInvincibility() {
        isImmune = true;

        yield return new WaitForSeconds(invinDuration);

        isImmune = false;

    }


    public void Damage(int amount) {
        //if immune no damage taken
        if (isImmune) return;

        //amount of damage can be changed when method is called
        health -= amount;

        if(health <= 0) {
            health = 0;
            //game over stuff goes here!!
            return;
        }
        //invincibility coroutine called after damage 
        StartCoroutine(TempInvincibility());
    }

    private void DisplayHearts() {
        //for every heart in scene
        for (int i = 0; i < hearts.Length; i++) {
            //hearts in scene below amount of health become full hearts
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                //if health is lower than amount of hearts in scene make extra empty as damage has been taken
                hearts[i].sprite = emptyHeart;
            }

            //only enable amount of hearts as to max health
            if (i < maxHealth) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
    }
}