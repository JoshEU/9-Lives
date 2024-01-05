using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWave : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private ItemSpawner itemSpawnerScript;
    [SerializeField]
    private GameObject lCWaitingPanel;
    [SerializeField]
    private GameObject dCWaitingPanel;
    [SerializeField]
    private GameObject lifePortal;
    [SerializeField]
    private GameObject deathPortal;
    [SerializeField]
    private GameObject findPortalText;
    [SerializeField]
    private Animator enterPortalAnim;
    [SerializeField]
    private Rigidbody2D rb2D;
    private bool playedNextWaveSFX = false;

    public static bool lcEnteredPortal = false;
    public static bool dcEnteredPortal = false;
    public static bool showPortals = false;

    private void Awake() {
        lcEnteredPortal = false;
        dcEnteredPortal = false;
        findPortalText.SetActive(false);
    }
    void Update() {
	    if (showPortals) {
            lifePortal.SetActive(true);
            deathPortal.SetActive(true);
            findPortalText.SetActive(true);
            // Destroy all remaining LifePowerups
            Destroy(itemSpawnerScript.lifePowerUpParentObj);
            // Destroy all remaining DeathPowerups
            Destroy(itemSpawnerScript.deathPowerUpParentObj);
            // Play Wave Complete Audio & Stop Gameplay Music
            audioManagerScript.StopGameplayMusicAudio();
            if (playedNextWaveSFX == false) {
                audioManagerScript.WaveCompleteAudio();
                playedNextWaveSFX = true;
            }
            // Only execute this if statement once
            showPortals = false;
        }
    }
    private IEnumerator delayNextWave() {
        yield return new WaitForSeconds(1.5f);
        // Check which Wave to Load next
        switch (GameStateManager.currentState) {
            case "Wave1":
                // Loads the next waves
                WaveManager.LoadWave2();
                break;
            case "Wave2":
                WaveManager.LoadWave3();
                break;
            case "Wave3":
                WaveManager.LoadWave4();
                break;
            case "Wave4":
                WaveManager.LoadWave5();
                break;
            case "Wave5":
                WaveManager.LoadWave6();
                break;
            case "Wave6":
                WaveManager.LoadWave7();
                break;
            case "Wave7":
                WaveManager.LoadWave8();
                break;
            case "Wave8":
                WaveManager.LoadWave9();
                break;
            case "Wave9":
                WaveManager.LoadWinScene();
                break;
        }
    }
    private void CheckBothEnteredPortal() {
        if (lcEnteredPortal == true && dcEnteredPortal == true) {
            lCWaitingPanel.SetActive(false);
            dCWaitingPanel.SetActive(false);
            StartCoroutine(delayNextWave());
        } else if (lcEnteredPortal == true) {
            lCWaitingPanel.SetActive(true);
        } else if (dcEnteredPortal == true) {
            dCWaitingPanel.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "Life_NextWavePortal") {
            // Play Enter Portal SFX
            audioManagerScript.EnterPortalAudio();
            rb2D.constraints = RigidbodyConstraints2D.None;
            enterPortalAnim.SetBool("EnteredPortal", true);
            dcEnteredPortal = true;
            CheckBothEnteredPortal();
        } else if (collision.gameObject.name  == "Death_NextWavePortal") {
            // Play Enter Portal SFX
            audioManagerScript.EnterPortalAudio();
            rb2D.constraints = RigidbodyConstraints2D.None;
            enterPortalAnim.SetBool("EnteredPortal", true);
            lcEnteredPortal = true;
            CheckBothEnteredPortal();
        }
    }
}