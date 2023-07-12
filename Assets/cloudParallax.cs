using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudParallax : MonoBehaviour{
    parallax parallax;

	private void Start() {
		parallax = FindObjectOfType<parallax>();
	}

	void Update(){
		//transform.Translate(Vector3.left * Time.deltaTime * parallax.cloudRate);
		//if()
    }
}
