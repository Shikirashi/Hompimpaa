using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PingPongManager : MonoBehaviour{
    [SerializeField]
    GameObject pauseButton, pausePanel, winPanel;
    [SerializeField]
    TextMeshProUGUI pemenangText;

    void Start() {
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
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

    public void ClearLevel(string pemenang) {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pausePanel.SetActive(false);
        pemenangText.text = pemenang;
        winPanel.SetActive(true);
    }
}
