using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XInput;
using UnityEngine.InputSystem.DualShock;

public class PlayerManager : MonoBehaviour {
    [SerializeField] 
    private PlayerInputManager inputManager;
    [SerializeField]
    private GameObject playerOne;
    [SerializeField]
    private GameObject playerTwo;
    [SerializeField]
    private Camera startCamera;
    [SerializeField]
    private GameObject p1StartScreen;
    [SerializeField]
    private GameObject p2StartScreen;
    public static InputDevice p1Device;
    public static InputDevice p2Device;
    private bool hasP1Joined = false;

    void Start() {
        Time.timeScale = 1;
    }
    void Update() {
		if (hasP1Joined) {
            playerOne.SetActive(true);
            playerTwo.SetActive(true);
        }
    }
    // Switches the player prefab depending on which player is present in the game and is receiving input
    private void OnPlayerJoined(PlayerInput playerInput) {
        if (playerInput.playerIndex == 0) {
            playerOne.SetActive(true);
            playerOne.name = "PlayerOne";
            // Remove P1 start screen
            p1StartScreen.SetActive(false);
            // Get Player 1 Device
            p1Device = playerInput.GetDevice<InputDevice>().device;
            // Make player 2 prefab spawn next if there is 1 player already in the game
            inputManager.playerPrefab = playerTwo;
            startCamera.enabled = false;
        } else if (playerInput.playerIndex == 1) {
            playerTwo.SetActive(true);
            playerTwo.name = "PlayerTwo";
            // Remove P2 start screen
            p2StartScreen.SetActive(false);
            // Get Player 2 Device id
            p2Device = playerInput.GetDevice<InputDevice>().device;
        }
    }
}