using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SortingGameManager : MonoBehaviour{
    [SerializeField]
    GameObject pauseButton, pausePanel, winPanel;
    public float minimumDistance;
    public GameObject ImageParent;
    void Start() {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        Shuffle();
    }

    void Shuffle() {
        List<int> indexes = new List<int>();
        List<Transform> items = new List<Transform>();
        for (int i = 0; i < ImageParent.transform.childCount; ++i) {
            indexes.Add(i);
            items.Add(ImageParent.transform.GetChild(i));
        }
        foreach (var item in items) {
            item.SetSiblingIndex(indexes[Random.Range(0, indexes.Count)]);
        }
        foreach (var item in items) {
            item.GetComponent<DragNDrop>().itemIndex = item.GetSiblingIndex();
        }
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

    public void ClearLevel() {
        Time.timeScale = 1f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(true);
    }
}
