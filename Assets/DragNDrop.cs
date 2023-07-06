 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour{

    public float minDist;
    public GameObject target;
    public GameObject SpotParent;
    public int itemIndex;
    void Start() {
        ResizeCollider();
        itemIndex = transform.GetSiblingIndex();
        minDist = FindObjectOfType<SortingGameManager>().minimumDistance;
    }

	public void ItemDrag() {
        transform.position = Input.mousePosition;
    }

    public void ItemEndDrag() {
        target = FindClosestTarget();
		if (!target.GetComponent<TargetScript>().hasImage) {
            float dist = Vector3.Distance(transform.position, target.transform.position);
            Debug.Log("Distance is " + dist);
            transform.position = Input.mousePosition;

            if (dist < minDist) {
                int targetIndex = target.transform.GetSiblingIndex();
                transform.parent.GetChild(targetIndex).GetComponent<DragNDrop>().itemIndex = itemIndex;
                transform.parent.GetChild(targetIndex).SetSiblingIndex(itemIndex);
                transform.SetSiblingIndex(targetIndex);
                itemIndex = transform.GetSiblingIndex();
                target.GetComponent<TargetScript>().hasImage = true;
                target.GetComponent<TargetScript>().heldImage = this.gameObject;
            }
			else {
                transform.position = SpotParent.transform.GetChild(itemIndex).transform.position;
			}
        }
    }

    public GameObject FindClosestTarget() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Target");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
			if (go.transform.GetSiblingIndex() == itemIndex) {
                continue;
			}
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    void ResizeCollider() {
        BoxCollider2D boxcol = GetComponent<BoxCollider2D>();
        RectTransform rect = GetComponent<RectTransform>();
        boxcol.size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
    }
}
