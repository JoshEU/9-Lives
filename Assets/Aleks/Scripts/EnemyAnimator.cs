using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour {

    //variables
    //enemy scripts to check when to play animations
    public DeathFlyEnemy deathScript;
    public LifeFlyEnemy lifeScript;

    //current enemy's animator
    [SerializeField] private Animator anim;

    //check which enemy this script is on
    [SerializeField] private bool DeathMouse = false;
    [SerializeField] private bool DeathDog = false;
    [SerializeField] private bool LifeMouse = false;
    [SerializeField] private bool LifeDog = false;
    [SerializeField] private bool LifeBrute = false;
    [SerializeField] private bool DeathBrute = false;


    // Update is called once per frame
    void Update() {
        //if enemy - do that enemy's logic
        if(DeathMouse) {
            dMouseAnim();
        }
        else if(DeathDog) {
            dDogAnim();
        }
        else if(LifeMouse) {
            lMouseAnim();
        }
        else if(LifeDog) {
            lDogAnim();
        }
        else if(DeathBrute) {
            dBruteAnim();
        }
        else if(LifeBrute) {
            lBruteAnim();
        }
    }

    void dMouseAnim() {
		//animation logic
		//Death = 3, fly = 2, idle = 1
		if (deathScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("DeathMouseTrigger", 3);
		}
		if (deathScript.attack == true) {
            anim.SetInteger("DeathMouseTrigger", 2);
        } 
        else {
            anim.SetInteger("DeathMouseTrigger", 1);
        }
    }

    void dDogAnim() {
		////it's idle by default so it can only be dead or idle, death = 1
		if (deathScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("DeathDogTrigger", 1);
		}
	}

    void dBruteAnim() {
		//similar to death dog, either angry or dead
		if (deathScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("BruteDeathTrigger", 1);
		}
	}
	void lMouseAnim() {
		//same as death mouse
		if (lifeScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("LifeMouseTrigger", 3);
		}
		if (lifeScript.attack == true) {
            anim.SetInteger("LifeMouseTrigger", 2);
        }
        else {
            anim.SetInteger("LifeMouseTrigger", 1);
        }
    }
   void lDogAnim() {
		//same as death dog
		if (lifeScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("LifeDogTrigger", 1);
		}
	}
    void lBruteAnim() {
		//same as life dog

		if (lifeScript.flyEnemyHealth <= 0) {
            anim.SetBool("isDead", true);
            //anim.SetInteger("BruteLifeTrigger", 1);
		}
	}
}