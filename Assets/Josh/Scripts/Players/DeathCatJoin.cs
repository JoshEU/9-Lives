using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathCatJoin : MonoBehaviour {
    [SerializeField]
    private PlayerInput playerInput;

    void Start() {
        PlayerManager.p1Device = playerInput.GetDevice<InputDevice>().device;
        PlayerManager.p1Device.MakeCurrent();
    }
    void Update() {
        
    }
}