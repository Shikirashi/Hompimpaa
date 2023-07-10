using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SortingGameManager : MonoBehaviour{
    [SerializeField]
    GameObject pauseButton, pausePanel, winPanel;
    public float minimumDistance;
    public GameObject ImageParent;
    [SerializeField] int itemAmount;
    [SerializeField] DragNDrop[] spawnItems;
    [SerializeField] ImageSpawner[] spawnSpots;
    List<Transform> items = new List<Transform>();
    List<Transform> spots = new List<Transform>();
    void Start() {
        spawnItems = FindObjectsOfType<DragNDrop>();
        spawnSpots = FindObjectsOfType<ImageSpawner>();
        SortImages();
        SortSpots();
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        Shuffle();
    }

    void SortImages() {
        int i = 1;
        int j;
        DragNDrop x;
        while (i < spawnItems.Length) {
            x = spawnItems[i];
            j = i - 1;
            while ((j >= 0) && (spawnItems[j].transform.position.x > x.transform.position.x)) {
                spawnItems[j + 1] = spawnItems[j];
                j = j - 1;
            }
            spawnItems[j + 1] = x;
            i = i + 1;
        }
    }

    void SortSpots() {
        int i = 1;
        int j;
        ImageSpawner x;
        while(i < spawnSpots.Length) {
            x = spawnSpots[i];
            j = i - 1;
            while((j >= 0) && (spawnSpots[j].transform.position.x > x.transform.position.x)) {
                spawnSpots[j + 1] = spawnSpots[j];
                j = j - 1;
            }
            spawnSpots[j + 1] = x;
            i = i + 1;
		}
	}

    void Shuffle() {
        Debug.Log("shuffling");
        int[] indexes = new int[5] { 0, 1, 2, 3, 4 };
        int x;
        for (int i = 0; i < indexes.Length; i++) {
            int rand = Random.Range(i, indexes.Length);
            x = indexes[rand];
            indexes[rand] = indexes[i];
            indexes[i] = x;
		}
		for (int i = 0; i < spawnItems.Length; i++) {
            Debug.Log("order: " + indexes[i]);
            spawnItems[i].transform.position = spawnSpots[indexes[i]].transform.position;
		}
    }

    void SwapInt(int a, int b) {
        int x = a;
        a = b;
        b = x;
	}

    public void PauseGame() {
        Debug.Log("Pausing game");
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void RetryLevel() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnHome() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void CheckOrder() {
        //checks order of images
        SortImages();
		for (int i = 0; i < spawnItems.Length; i++) {
            if (spawnItems[i].itemIndex != i + 1) {
                Debug.Log("order wrong");
                return;
			}
        }
        Debug.Log("order correct");
        ClearLevel();
    }

    public void ClearLevel() {
        Time.timeScale = 1f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(true);
    }
}
