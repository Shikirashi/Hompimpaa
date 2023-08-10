using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    Rigidbody2D rb;
    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        MovePlayer();
    }

    private void MovePlayer() {
        if(Application.platform == RuntimePlatform.Android) {
            foreach (Touch touch in Input.touches) {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 pos = rb.position;

                if (Mathf.Abs(touchPosition.x - pos.x) <= 2) {
                    pos.y = Mathf.Lerp(pos.y, touchPosition.y, 10f);
                    pos.y = Mathf.Clamp(pos.y, -4f, 4f);
                    rb.position = pos;
                }
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor) {
            //Debug.Log("No controls yet player");
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer) {
            //Debug.Log("No controls yet player");
        }
    }
}
