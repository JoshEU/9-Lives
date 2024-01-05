using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private PlayerInput playerInput;
    // Player Properties:
    [SerializeField]
    private Rigidbody2D rb2D;
    public float movementSpeed = 1.0f;
    public bool jumpBtnPressed = false;
    public Vector2 moveDirection;
    public LayerMask whatIsGround;
    public bool isFacingRight = true;
    public int health = 9;
    private Vector3 prevPos;
    // Variable Jump Properties:
    public float jumpForce = 14.5f;
    public float maxButtonTime = 0.35f;
    public float currentJumpTime = 0.0f;
    // Animation Variables:
    [SerializeField] 
    private Animator anim;
    public bool isIdle = false;
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isDead = false;

    public static bool isPaused = false; 

    // Referencing Input Action Asset [Movement]
    private void OnMove(InputValue value) {
        moveDirection = value.Get<Vector2>();
        isWalking = true;
    }
    // Performing Movement Functionality
    private void MovePlayer() {
        rb2D.velocity = new Vector2(moveDirection.x * movementSpeed, rb2D.velocity.y);
    }

    // Check if the player is currently Moving - Called inside Update()
    private void MoveCheck() {
        // Check if the Player has moved
        if (gameObject.transform.position != prevPos && isJumping == false) {
            // Player has moved
            isIdle = false;
            isWalking = true;
        } else if (isJumping == false) {
            // Player has not moved
            isWalking = false;
            isIdle = true;
        }
        prevPos = gameObject.transform.position;
    }

    //Performing Turn Functionality
    private void TurnPlayer() {
        // Turn Life Cat 
	    if(gameObject.name == "LifeCat" && moveDirection.x > 0) {
            // Facing right
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            isFacingRight = true;
        } else if(gameObject.name == "LifeCat" && moveDirection.x < 0) {
            // Facing left
            gameObject.transform.rotation = new Quaternion(0, 180.0f, 0, 0);
            isFacingRight = false;
        }
        // Turn Death Cat 
        if (gameObject.name == "DeathCat" && moveDirection.x > 0) {
            // Facing right
            gameObject.transform.rotation = new Quaternion(0, 180.0f, 0, 0);
            isFacingRight = true;
        } else if (gameObject.name == "DeathCat" && moveDirection.x < 0) {
            // Facing left
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            isFacingRight = false;
        }
    }

	// Referencing Input Action Asset [Jump]
	private void OnJump() {
		if (gameObject.GetComponent<CompositeCollider2D>().IsTouchingLayers(whatIsGround) && GameStateManager.currentState != "Paused" && isPaused == false) {
            audioManagerScript.PlayerJumpAudio();
            currentJumpTime = 0;
            jumpBtnPressed = true;
        }
	}
	// Check if the player is currently Jumping - Called inside Update()
	private void JumpCheck() {
        if (jumpBtnPressed) {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            currentJumpTime += Time.deltaTime;
            isIdle = false;
            isWalking = false;
            isJumping = true;
        }
		// Make the first device to join be the DeathCat and set it to current controller
		if (gameObject.CompareTag("DeathCat")) {
			PlayerManager.p1Device = playerInput.GetDevice<InputDevice>().device;
			PlayerManager.p1Device.MakeCurrent();
		}
		// Make the second device to join be the LifeCat and set it to current controller
		if (gameObject.CompareTag("LifeCat")) {
			PlayerManager.p2Device = playerInput.GetDevice<InputDevice>().device;
			PlayerManager.p2Device.MakeCurrent();
		}
		// Stop Jumping when the 'Jump Button' has been released
		if (Gamepad.current.buttonSouth.isPressed == false && EventSystem.current.currentSelectedGameObject == null || currentJumpTime > maxButtonTime && EventSystem.current.currentSelectedGameObject == null) {
            jumpBtnPressed = false;
            currentJumpTime = 0;
            isJumping = false;
        }
    }

    private void CheckAnimations() {
        // Play appropriate Animations
        if (isDead != true) {
            if (isIdle == true) {
                anim.SetInteger("animSetter", 1);
            } else if (isWalking == true) {
                anim.SetInteger("animSetter", 2);
            } else if (currentJumpTime != 0 && currentJumpTime < maxButtonTime) {
                anim.SetInteger("animSetter", 3);
            }
        } else if (isDead == true) {
            anim.SetInteger("animSetter", 4);
        }
    }
	private void Awake() {
        rb2D.velocity = new Vector2(0, 0);
        isIdle = true;
        isWalking = false;
        isJumping = false;
        isDead = false;
    }
	void Update() {
        TurnPlayer();
        JumpCheck();
        MoveCheck();
        CheckAnimations();
	}
	private void FixedUpdate() {
        MovePlayer();
    }
}