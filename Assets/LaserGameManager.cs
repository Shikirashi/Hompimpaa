using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CarterGames.Assets.AudioManager;

public class LaserGameManager : MonoBehaviour{
    [SerializeField]
    GameObject pauseButton, pausePanel, winPanel;
    AudioVariables audioVars;

    void Start(){
        audioVars = FindObjectOfType<AudioVariables>();
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

    public void ClearLevel() {
        Time.timeScale = 1f;
        AudioManager.instance.Play("level-win", volume: audioVars.SFXVolume, loop: false);
        pauseButton.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(true);
    }
}
