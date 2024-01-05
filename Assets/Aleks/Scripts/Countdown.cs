using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{

    [SerializeField]private float timer;
    private string countingDown;
    [SerializeField] private TMP_Text cDisplayCounter;
    private bool timerIsRunning = false;
    [SerializeField] private TMP_Text gDisplayCounter;

    void Start() {
        //enables timer
        timerIsRunning = true;
    }
    // Update is called once per frame
    void Update() {
        //if timer enabled
        if (timerIsRunning) {
            if (timer > 0) {
                //with every second timer decreases
                timer -= Time.deltaTime;
                //convert to int then to string
                countingDown  = ("" + (int)timer);
                //display timer
                gDisplayCounter.text = (countingDown);
                cDisplayCounter.text = (countingDown);
            }
            else {
                //at the end of timer set text to null
                gDisplayCounter.text = ("");
                cDisplayCounter.text = ("");
            }
        }
    }
}
