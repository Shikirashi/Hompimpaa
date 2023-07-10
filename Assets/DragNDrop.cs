 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler{

    public SortingGameManager sorting;
    public float minDist;
    public GameObject target;
    public GameObject SpotParent;
    public int itemIndex;

    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

	private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start() {
        //ResizeCollider();
        sorting = FindObjectOfType<SortingGameManager>();
    }

    void ResizeCollider() {
        BoxCollider2D boxcol = GetComponent<BoxCollider2D>();
        RectTransform rect = GetComponent<RectTransform>();
        boxcol.size = new Vector2(rect.sizeDelta.x, rect.sizeDelta.y);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("begin drag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData) {
        Debug.Log("on drag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData) {
        Debug.Log("end drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

	public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("on click");
    }

	public void OnInitializePotentialDrag(PointerEventData eventData) {
        eventData.useDragThreshold = false;
	}

    /*
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
            Debug.Log("checking order");
            sorting.CheckOrder();
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
*/
}
