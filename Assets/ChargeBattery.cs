using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarterGames.Assets.AudioManager;

public class ChargeBattery : MonoBehaviour{
    [SerializeField]
    Material noBattery, lowBattery, halfBattery, almostBattery, fullBattery;

    public bool isCharging, isFull;
    public static AudioManager audioManager;
    MeshRenderer face;
    float count;
    int charge, previousCharge;
    void Start(){
        face = transform.GetChild(0).GetComponent<MeshRenderer>();
        isCharging = false;
        isFull = false;
        charge = 0;
        previousCharge = charge;
        count = 0;
        if(audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
		}
    }

    void Update(){
		if (isCharging && count <= 5f) {
            count += Time.deltaTime;
            charge = Mathf.FloorToInt(count);
        }
        /*
        if(count <= 1f) {
            face.material = noBattery;
            charge = Mathf.FloorToInt(count);
		}
        else if (count <= 2f) {
            face.material = lowBattery;
            charge = Mathf.FloorToInt(count);
        }
        else if (count <= 3f) {
            face.material = halfBattery;
            charge = Mathf.FloorToInt(count);
        }
        else if (count <= 4f) {
            face.material = almostBattery;
            charge = Mathf.FloorToInt(count);
        }
        else if (count <= 5f) {
            face.material = fullBattery;
            count += 1f;
            StartCoroutine("BatteryFull");
            charge = Mathf.FloorToInt(count);
        }*/

		if (isFull) {
            FindObjectOfType<LaserGameManager>().ClearLevel();
        }
        if(charge != previousCharge) {
            switch (charge) {
                case 1:
                    face.material = noBattery;
                    break;
                case 2:
                    face.material = lowBattery;
                    audioManager.Play("186669__fordps3__computer-boop", pitch: 0.4f, loop: false);
                    break;
                case 3:
                    face.material = halfBattery;
                    audioManager.Play("186669__fordps3__computer-boop", pitch: 0.6f, loop: false);
                    break;
                case 4:
                    face.material = almostBattery;
                    audioManager.Play("186669__fordps3__computer-boop", pitch: 0.8f, loop: false);
                    break;
                case 5:
                    face.material = fullBattery;
                    audioManager.Play("186669__fordps3__computer-boop", pitch: 1f, loop: false);
                    count += 1f;
                    StartCoroutine("BatteryFull");
                    break;
                default: break;
            }
            previousCharge = charge;
        }
        isCharging = false;
    }


	public IEnumerator BatteryFull() {
        Debug.Log("Battery is charged");
        yield return new WaitForSeconds(1f);
        isFull = true;
        Debug.Log("Battery full is " + isFull);
    }
}
