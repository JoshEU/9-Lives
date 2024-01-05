using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBulletMirror : MonoBehaviour {
    // Camera GameObjects
    private GameObject dcCamObject;
    // Camera
    private Camera dcCamera;
    // Bullet Position
    private Vector3 deathBulletPos;
    // Bullet Object
    [SerializeField]
    private GameObject deathBulletObj;
    // The difference in height between the Death Camera and the Death Bullet
    public float deathCamHeightDiff;

    void Start() {
        // Retrieve both Camera Objects
        dcCamObject = GameObject.FindGameObjectWithTag("DeathCatCamera");
        dcCamera = dcCamObject.GetComponent<Camera>();
    }
    void Update() {
        // Stores Death Bullets Current Position
        deathBulletPos = deathBulletObj.transform.localPosition;

        // Get the difference between the center of the Death Camera and the Death Bullets position every frame
        deathCamHeightDiff = dcCamera.transform.position.y - deathBulletObj.transform.position.y;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("DeathCatCameraConfiner")) {
            deathBulletPos = dcCamera.ViewportToWorldPoint(deathBulletPos);
            // Move Death Bullet to the same height on the Death Camera as it was on the Life Camera (the side it was shot from)
            deathBulletObj.transform.position = new Vector3(0.680f, GameObject.FindGameObjectWithTag("LifeCatCamera").transform.position.y - deathCamHeightDiff, 2);
        }
    }
}
