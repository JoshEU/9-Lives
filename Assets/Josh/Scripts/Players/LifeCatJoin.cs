using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LifeCatJoin : MonoBehaviour {
    [SerializeField]
    private PlayerInput playerInput;

    void Start() {
        PlayerManager.p2Device = playerInput.GetDevice<InputDevice>().device;
        PlayerManager.p2Device.MakeCurrent();
    }
    void Update() {
        
    }
}