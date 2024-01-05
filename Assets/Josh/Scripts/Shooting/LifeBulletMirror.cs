using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBulletMirror : MonoBehaviour {
    // Camera GameObjects
    private GameObject lcCamObject;
    // Camera
    private Camera lcCamera;
    // Bullet Position
    private Vector3 lifeBulletPos;
    // Bullet Object
    [SerializeField]
    private GameObject lifeBulletObj;
    // The difference in height between the Life Camera and the Life Bullet 
    private float lifeCamHeightDiff;

    void Start() {
        // Retrieve both Camera Objects
        lcCamObject = GameObject.FindGameObjectWithTag("LifeCatCamera");
        lcCamera = lcCamObject.GetComponent<Camera>();
    }
    void Update() {
        // Stores Life Bullets Current Position
        lifeBulletPos = lifeBulletObj.transform.localPosition;

        // Get the difference between the center of the Life Camera and the Life Bullets position every frame
        lifeCamHeightDiff = lcCamera.transform.position.y - lifeBulletObj.transform.position.y;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("LifeCatCameraConfiner")) {
            lifeBulletPos = lcCamera.ViewportToWorldPoint(lifeBulletPos);
            // Move Life Bullet to the same height on the Life Camera as it was on the Death Camera (the side it was shot from)
            lifeBulletObj.transform.position = new Vector3(0.025f, GameObject.FindGameObjectWithTag("DeathCatCamera").transform.position.y - lifeCamHeightDiff, -2);
        } 
    }
}