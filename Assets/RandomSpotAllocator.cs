using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpotAllocator : MonoBehaviour{
    [SerializeField]
    List<GameObject> SpotParents = new List<GameObject>();
    [SerializeField]
    List<GameObject> SpotImages = new List<GameObject>();
    [SerializeField]
    List<GameObject> RefImages;
    void Start() {
        RefImages = new List<GameObject>(SpotImages);
        //while(CheckMatch(SpotImages, RefImages)) {
            //RandomizeSpots();
        //}
    }

    void RandomizeSpots() {
        for (int i = 0; i < SpotImages.Count; i++) {
            GameObject temp = SpotImages[i];
            int randomIndex = Random.Range(i, SpotImages.Count);
            SpotImages[i] = SpotImages[randomIndex];
            SpotImages[randomIndex] = temp;
            SpotImages[i].transform.SetParent(SpotParents[i].transform);
        }
    }
    bool CheckMatch(List<GameObject> l1, List<GameObject> l2) {
        if (l1.Count != l2.Count)
            return false;
        for (int i = 0; i < l1.Count; i++) {
            if (l1[i] != l2[i])
                return false;
        }
        return true;
    }
}
