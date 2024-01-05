using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealthBar : MonoBehaviour {
    [SerializeField] private Slider bar;
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject fill;
    [SerializeField] private GameObject Enemy; 
    [SerializeField] private Vector3 Offset;
    [SerializeField] bool lifeEnemy;
    [SerializeField] bool deathEnemy;
    private Camera cam;
    private RectTransform rectTransform;


    public LifeFlyEnemy lifeScript;
    public DeathFlyEnemy deathScript;

    void Start() {
        //activate healthbar
        fill.SetActive(true);

        rectTransform = GetComponent<RectTransform>();

        //if its a life enemy
        if(lifeEnemy) {
            //if its a life enemy the slider needs to be tracked to the death cats camera
            cam = GameObject.FindGameObjectWithTag("DeathCatCamera").GetComponent<Camera>() as Camera;
            
            //bar set to enemy's starting health
            bar.maxValue = lifeScript.flyEnemyHealth;
        }
        //if its a death enemy
        else if(deathEnemy) {
            //if its a death enemy the slider needs to be tracked to the life cats camera
            cam = GameObject.FindGameObjectWithTag("LifeCatCamera").GetComponent<Camera>() as Camera;
            //bar set to enemy's starting health
            bar.maxValue = deathScript.flyEnemyHealth;

        }
    }

    // Update is called once per frame
    void Update() {
		// Convert the in-game object's position to screen coordinates
        Vector3 screenPosition = cam.WorldToScreenPoint(Enemy.transform.position)+ Offset;

        // Set the UI object's position to the screen coordinates
        rectTransform.position = screenPosition;
		
        if(lifeEnemy) {
            //if health below a certain point change its color
            if (lifeScript.flyEnemyHealth < 2.0f && lifeScript.gameObject.tag != "Mouse") {
                barImage.color = Color.red;
            }
            //if enemy dies disable healthbar
            else  if(lifeScript.flyEnemyHealth == 0.0f) {
                fill.SetActive(false);
            }
            //set slider to health
            bar.value = lifeScript.flyEnemyHealth;

        }
        else if(deathEnemy) {
            if (deathScript.flyEnemyHealth < 2.0f && deathScript.gameObject.tag != "Mouse") {
                barImage.color = Color.red;
            }
            else  if(deathScript.flyEnemyHealth == 0.0f) {
                fill.SetActive(false);
            }
            bar.value = deathScript.flyEnemyHealth;

        }

        

    }

}
