using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : MonoBehaviour {
    [SerializeField]
    private Animator wave1Transition;

    void Start() {
        if (GameplayUIManager.pressedPlayAgain == true) {
            wave1Transition.enabled = false;
        }
    }
}