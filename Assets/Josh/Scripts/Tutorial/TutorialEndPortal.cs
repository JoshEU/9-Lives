using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialEndPortal : MonoBehaviour {
    [SerializeField]
    private AudioManager audioManagerScript;
    [SerializeField]
    private Tutorial tutorialScript;
    [SerializeField]
    private GameObject endTutorialPanelObj;
    [SerializeField]
    private GameObject yesBtnObj;
    [SerializeField]
    private GameObject findPortalText;
    [SerializeField]
    private Animator enterPortalAnim;
    [SerializeField]
    private Rigidbody2D rb2D;

    public static bool lcEnteredPortalTutorial = false;
    public static bool dcEnteredPortalTutorial = false;

    void Start() {
        lcEnteredPortalTutorial = false;
        dcEnteredPortalTutorial = false;
        findPortalText.SetActive(false);
    }
    private void CheckBothEnteredPortal() {
        if (lcEnteredPortalTutorial == true && dcEnteredPortalTutorial == true) {
            GameplayUIManager.canPauseGame = false;
            tutorialScript.lCWaitingPanel.SetActive(false);
            tutorialScript.dCWaitingPanel.SetActive(false);
            findPortalText.SetActive(false);
            endTutorialPanelObj.SetActive(true);
            MainMenuManager.hasPlayedTutorial = true;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(yesBtnObj);
        } else if(lcEnteredPortalTutorial == true) {
            tutorialScript.lCWaitingPanel.SetActive(true);
        } else if(dcEnteredPortalTutorial == true) {
            tutorialScript.dCWaitingPanel.SetActive(true);
        }
    }
	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.CompareTag("LifeCat")) {
            // Play Enter Portal SFX
            audioManagerScript.EnterPortalAudio();
            rb2D.constraints = RigidbodyConstraints2D.None;
            enterPortalAnim.SetBool("EnteredPortal", true);
            lcEnteredPortalTutorial = true;
            CheckBothEnteredPortal();
        }
        else if (collision.gameObject.CompareTag("DeathCat")) {
            // Play Enter Portal SFX
            audioManagerScript.EnterPortalAudio();
            rb2D.constraints = RigidbodyConstraints2D.None;
            enterPortalAnim.SetBool("EnteredPortal", true);
            dcEnteredPortalTutorial = true;
            CheckBothEnteredPortal();
        }
	}
}