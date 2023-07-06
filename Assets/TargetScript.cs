using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour{

    public bool hasImage;
    public GameObject heldImage;

    void Start(){
        //hasImage = false;
        heldImage = null;
        GetComponent<Image>().enabled = true;
        ResizeCollider();
    }

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == "Image") {
            hasImage = false;
            heldImage = null;
            GetComponent<Image>().enabled = true;
        }
    }
    void ResizeCollider() {
        BoxCollider2D boxcol = GetComponent<BoxCollider2D>();
        RectTransform rect = GetComponent<RectTransform>();
        boxcol.size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
    }
}
