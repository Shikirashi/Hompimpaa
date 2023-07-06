using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour{
    public SpriteRenderer bg;
    void Start(){
        float orthoSize = Mathf.Clamp(bg.bounds.size.x * Screen.height / Screen.width * 0.5f, 4.5f, float.MaxValue);
        Camera.main.orthographicSize = orthoSize;
    }
}
