using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutManager : MonoBehaviour {
    [SerializeField] GameObject blackImage;
    [SerializeField] private Animator fadeAnim;
    [HideInInspector] public static bool fadeOutTrigger = false;
    [HideInInspector] public static bool fadeInTrigger =  false;

    void Awake() {
        //activate black image so scene is accessible
        blackImage.SetActive(true);
    }
    // Update is called once per frame
    void Update() {
        if(fadeOutTrigger == true) {
            //do fadeOut animation if bool
            fadeAnim.SetInteger("TriggerTransition", 1);
        }
    
        if(fadeInTrigger == true) {
            //do fadeIn animation if bool
            fadeAnim.SetInteger("TriggerTransition", 0);
        }
    }

    
}
