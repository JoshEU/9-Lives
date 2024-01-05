using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//gamestates for ghost
public enum GhostState
{
    IDLE,
    RUNNING,
    DEAD
}

public class GhostControl : MonoBehaviour
{
    //starting game state
    public GhostState playerState = GhostState.IDLE;

    //modifiable speed variable (no jump as ghost can't jump
    [Header("Settings for Movement")]
    public float speed = 5.0f;

    void Update()
    {
        //check for changes to state
        //if idle check for movement
        if (playerState == GhostState.IDLE)
        {
            if (Keyboard.current.dKey.isPressed || Keyboard.current.aKey.isPressed)
            {

                playerState = GhostState.RUNNING;
            }
        }
        //if running check if movement stops
        else if (playerState == GhostState.RUNNING)
        {

            if (!Keyboard.current.dKey.isPressed && (!Keyboard.current.aKey.isPressed))
            {
                playerState = GhostState.IDLE;
            }
        }
        
        //if running move ghost 
        if (playerState == GhostState.RUNNING)
        {
            if (Keyboard.current.dKey.isPressed)
            {
                transform.Translate(transform.right * Time.deltaTime * speed);
            }
            else if (Keyboard.current.aKey.isPressed)
            {
                transform.Translate(-transform.right * Time.deltaTime * speed);
            }
        }
    }

   
}
