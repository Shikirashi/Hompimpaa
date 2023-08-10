using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

public class ChargeBattery : MonoBehaviour{
    [SerializeField]
    Material noBattery, lowBattery, halfBattery, almostBattery, fullBattery;

    public bool isCharging, isFull, isPlay;
    public static AudioManager audioManager;
    MeshRenderer face;
    TileRotator rotator;
    float count, volume;
    int charge, previousCharge;
    void Start(){
        rotator = FindObjectOfType<TileRotator>();
        volume = rotator.GetVolume();
        face = transform.GetChild(0).GetComponent<MeshRenderer>();
        isCharging = false;
        isFull = false;
        isPlay = true;
        charge = 1;
        previousCharge = charge;
        count = 1;
        if(audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
		}
    }

    void Update(){
		if (isPlay) {
            if (isCharging && count <= 5f) {
                count += Time.deltaTime;
                charge = Mathf.FloorToInt(count);
            }
            if (isFull) {
                isPlay = false;
                FindObjectOfType<LaserGameManager>().ClearLevel();
            }
            if (charge != previousCharge) {
                switch (charge) {
                    case 1:
                        face.material = noBattery;
                        audioManager.Play("boop", pitch: 0.4f, volume: volume, loop: false); ;
                        break;
                    case 2:
                        face.material = lowBattery;
                        audioManager.Play("boop", pitch: 0.4f, volume: volume, loop: false);
                        break;
                    case 3:
                        face.material = halfBattery;
                        audioManager.Play("boop", pitch: 0.6f, volume: volume, loop: false);
                        break;
                    case 4:
                        face.material = almostBattery;
                        audioManager.Play("boop", pitch: 0.8f, volume: volume, loop: false);
                        break;
                    case 5:
                        face.material = fullBattery;
                        audioManager.Play("boop", pitch: 1f, volume: volume, loop: false);
                        count += 1f;
                        StartCoroutine("BatteryFull");
                        break;
                    default: break;
                }
                previousCharge = charge;
            }
            isCharging = false;
        }
    }


	public IEnumerator BatteryFull() {
        Debug.Log("Battery is charged");
        yield return new WaitForSeconds(1f);
        isFull = true;
        Debug.Log("Battery full is " + isFull);
    }
}
