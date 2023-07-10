using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TargetScript : MonoBehaviour, IDropHandler{

    public bool hasImage;
	//public GameObject heldImage;
	private void Start() {
        hasImage = false;
	}
	public void OnDrop(PointerEventData eventData) {
        Debug.Log("on drop");
        if (eventData.pointerDrag != null) {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            hasImage = true;
        }
    }

}
