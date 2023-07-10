using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragController : MonoBehaviour{
    private bool isDragActive = false;
    private Vector2 screenPos;
    private Vector3 worldPos;
    private DragNDrop lastDragged;

	private void Awake() {
        DragController[] controllers = FindObjectsOfType<DragController>();
        if(controllers.Length > 1) {
            Destroy(gameObject);
		}
	}

	private void Update() {
		if(isDragActive && (Input.GetMouseButton(0) || (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Ended))) {
			Drop();
			return;
		}
		if (Input.GetMouseButton(0)) {
			Vector3 mousePos = Input.mousePosition;
			screenPos = new Vector2(mousePos.x, mousePos.y);
		}
		else if (Input.touchCount > 0) {
			screenPos = Input.GetTouch(0).position;
		}

		worldPos = Camera.main.ScreenToWorldPoint(screenPos);

		if (isDragActive) {
			Drag();
		}
		else {
			RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
			if(hit.collider != null) {
				DragNDrop dragDrop = hit.transform.gameObject.GetComponent<DragNDrop>();
				if(dragDrop != null) {
					lastDragged = dragDrop;
					InitDrag();
				}
			}
		}
	}

	void InitDrag() {
		isDragActive = true;
	}

	void Drag() {
		lastDragged.transform.position = new Vector2(worldPos.x, worldPos.y);
	}

	void Drop() {
		isDragActive = false;
	}
}
