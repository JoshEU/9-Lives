using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

//this script + GhostControl was made using a tutorial - cant find it tho
//gamestates for cat
public enum CharacterState
{
    IDLE,
    RUNNING,
    JUMPING,
    DEAD
}
//animSetter
//Idle = 1
//Walking = 2
//Jumping = 3
//shooting = 4
//Dead =5 

public class CatControl : MonoBehaviour
{
    //beginning gamestate
    public CharacterState playerState = CharacterState.IDLE;

    //modifiable variables for cat
    [Header("Settings for Movement")]
    public float speed = 5.0f;
    public float jumpStrength = 10.0f;
    [SerializeField] Animator anim;


    void Update()
    {
        //check for changes to state
        //if idle check for movement
        if (playerState == CharacterState.IDLE)
        {
            anim.SetInteger("animSetter",1 );
            if (Keyboard.current.rightArrowKey.isPressed || (Keyboard.current.leftArrowKey.isPressed))
            {
                playerState = CharacterState.RUNNING;
            }
            else if (Keyboard.current.upArrowKey.isPressed)
            {
                //player jumps from idle
                gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * jumpStrength;
                playerState = CharacterState.JUMPING;
                anim.SetInteger("animSetter",3 );
                //coroutine to see if player can jump again
                StartCoroutine("CheckGrounded");
            }
        }
        else if (playerState == CharacterState.RUNNING)
        {

            if (Keyboard.current.upArrowKey.isPressed)
            {
                //jump from running
                anim.SetInteger("animSetter",3 );  
                gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * jumpStrength;
                playerState = CharacterState.JUMPING;
                StartCoroutine("CheckGrounded");
            }
            else if (!Keyboard.current.rightArrowKey.isPressed && !Keyboard.current.leftArrowKey.isPressed)
            {
                //set back to idle if no input
                playerState = CharacterState.IDLE;
                anim.SetInteger("animSetter",1 );
                
            }
        }

        if (playerState == CharacterState.JUMPING || playerState == CharacterState.RUNNING)
        {
            anim.SetInteger("animSetter",2 );

            //if jumping or moving move in direction dependent on movement 
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                transform.Translate(transform.right * Time.deltaTime * speed);
            }
            else if (Keyboard.current.leftArrowKey.isPressed)
            {
                transform.Translate(-transform.right * Time.deltaTime * speed);

            }
        }
        int animInt = anim.GetInteger("animSetter");
    }

    IEnumerator CheckGrounded()
    {
        //only allow jump every 0.5 seconds
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.up * 1f, -Vector2.up, 0.05f);
            if (hit.collider != null)
            {
                //if colliding with the ground
                if (hit.transform.tag == "Terrain")
                {
                    //change states
                    if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                    {
                        playerState = CharacterState.RUNNING;
                        
                    }
                    else
                    {
                        playerState = CharacterState.IDLE;
                        anim.SetInteger("animSetter",1 );

                    }
                    break;
                }
            }

            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }
}