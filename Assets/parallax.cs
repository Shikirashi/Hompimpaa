using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour{
    //public GameObject parallaxParent;
    //public GameObject clouds, trees, chicken, deer;
    //public float cloudRate, treesRate, chickenRate, deerRate;
    [SerializeField] float moveSpeed;
    [SerializeField] bool scrollLeft;

    float singleTextureWidth;

	private void Start() {
        //parallaxParent = gameObject;
        SetupTexture();
		if (scrollLeft) {
            moveSpeed = -moveSpeed;
		}
    }
    void SetupTexture() {
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        singleTextureWidth = sprite.texture.width / sprite.pixelsPerUnit;
    }
    void Scroll() {
        float delta = moveSpeed * Time.deltaTime;
        transform.position += new Vector3(delta, 0f, 0f);
	}
    void CheckReset() {
        if((Mathf.Abs(transform.position.x) - singleTextureWidth) > 0) {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
		}
	}

    void Update(){
        Scroll();
        CheckReset();
        /*
        clouds.transform.Translate(Vector3.left * Time.deltaTime * cloudRate, Space.World);
        trees.transform.Translate(Vector3.left * Time.deltaTime * treesRate, Space.World);
        chicken.transform.Translate(Vector3.right * Time.deltaTime * chickenRate, Space.World);
        deer.transform.Translate(Vector3.left * Time.deltaTime * deerRate, Space.World);
        */
        //cloudParallax[] clouds = FindObjectsOfType<cloudParallax>();
        //if(clouds.Length > 3) {
         //   Destroy(clouds[3].gameObject);
		//}
    }


}
