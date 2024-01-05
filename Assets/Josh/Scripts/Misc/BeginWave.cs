using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginWave : MonoBehaviour {
    [SerializeField]
    private ItemSpawner itemSpawnerScript;
    [SerializeField]
    private GameObject beginWavePanelObj;
    public float waveBeginDelay;

	private void Start() {
        StartCoroutine(BeginWaveDelay());
    }
    // Displays the BeginWaveInfo such as the Wave Number and the Goal of that wave
    // After a delay, that info disappears and the starting enemies begin to spawn in
    public IEnumerator BeginWaveDelay() {
        yield return new WaitForSeconds(0.10f);
        beginWavePanelObj.SetActive(true);
        yield return new WaitForSeconds(waveBeginDelay);
        beginWavePanelObj.SetActive(false);
        // Spawn Enemies & Powerups in
        itemSpawnerScript.lifeEnemyParentObj.SetActive(true);
        itemSpawnerScript.lifePowerUpParentObj.SetActive(true);
        itemSpawnerScript.deathEnemyParentObj.SetActive(true);
        itemSpawnerScript.deathPowerUpParentObj.SetActive(true);
    }
}