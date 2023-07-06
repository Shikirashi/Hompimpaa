using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBlanker : MonoBehaviour {
    void Start() {
        Sprite[] sprites;
        sprites = Resources.LoadAll<Sprite>("Sprites/hompimpaa_sprites");
        foreach(Transform child in transform) {
            Image image = child.GetComponent<Image>();
            if (image.sprite == null) {
                image.color = new Color32(0, 247, 255, 255);
            }
            else if (image.sprite != (Sprite)sprites[1] && image.sprite != null) {
                Debug.Log(child.transform.name);
                int rounds = Random.Range(1, 5);
				for (int i = 0; i < rounds; i++) {
                    child.GetComponent<RectTransform>().Rotate(new Vector3(0, 0, 90));
                }
			}
		}
    }
}
