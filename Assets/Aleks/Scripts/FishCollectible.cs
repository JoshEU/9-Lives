using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FishCollectible : MonoBehaviour {
    //variables
    private int Ammo = 0;
    [SerializeField] private TMP_Text currentAmmo;
    


    //check what is being collided
    private void OnTriggerEnter2D(Collider2D other) {
      
        //ammo power up
        if (other.tag == "ammo") {
            Ammo = Ammo += 1;
            currentAmmo.text = ("Ammo: " + Ammo);
            Destroy(other.gameObject);
        }
        
    }

    void Update() {
        currentAmmo.text = ("Ammo: " + Ammo);
    }


}