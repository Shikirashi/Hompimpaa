using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour{
    public GameObject clouds, trees, chicken, deer;
    void Start(){
        
    }

    void Update(){
        clouds.transform.Translate(Vector3.left * Time.deltaTime * 10f, Space.World);
    }
}
