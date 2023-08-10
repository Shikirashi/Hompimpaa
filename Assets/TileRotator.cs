using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using CarterGames.Assets.AudioManager;

public class TileRotator : MonoBehaviour{
    [SerializeField] string filename;
    [SerializeField]
    float speed = 10f, volume;
	private new Camera camera;
    private Ray ray;
    private RaycastHit hit;
    private bool turning = false;
    public static AudioManager audioManager;
    private List<UserData> userdata = new List<UserData>();

    void Start(){
        filename = "saveData.json";
        userdata = SaveData.ReadFromJSON<UserData>(filename);
        foreach (UserData u in userdata) {
            volume = u.sfxVolume;
        }
        camera = Camera.main;
        turning = false;
        if(audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
		}
    }

    void Update(){
        if(Application.platform == RuntimePlatform.Android) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    Touch touch = Input.GetTouch(0);
                    ray = new Ray(camera.ScreenToWorldPoint(touch.position), camera.transform.forward);
                    if (Physics.Raycast(ray, out hit, 100f, 1)) {
                        if (hit.collider.gameObject.tag == "MirrorBody" && !turning) {
                            turning = true;
                            hit.collider.transform.parent.transform.DORotate(new Vector3(90f, 0f, 0f), 1 / speed, RotateMode.LocalAxisAdd).OnComplete(() => turning = false);
                            audioManager.Play("short-whistle", volume: volume, loop: false);
                        }
                    }
                }
            }
        }
        else if(Application.platform == RuntimePlatform.WindowsEditor) {
			if (Input.GetMouseButtonDown(0)) {
                ray = new Ray(camera.ScreenToWorldPoint(Input.mousePosition), camera.transform.forward);
                if (Physics.Raycast(ray, out hit, 100f, 1)) {
                    if (hit.collider.gameObject.tag == "MirrorBody" && !turning) {
                        turning = true;
                        hit.collider.transform.parent.transform.DORotate(new Vector3(90f, 0f, 0f), 1 / speed, RotateMode.LocalAxisAdd).OnComplete(() => turning = false);
                        audioManager.Play("short-whistle", volume: volume, loop: false);
                    }
                }
            }
		}
    }
    public float GetVolume() {
        return this.volume;
	}
}
