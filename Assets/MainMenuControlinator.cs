using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using CarterGames.Assets.AudioManager;
using UnityEngine.SceneManagement;

public class MainMenuControlinator : MonoBehaviour{
    [SerializeField] string filename;
    public AudioSettingsHandleinator audioSettings;

    public GameObject popupWindows, AudioSettingsPopup, ExitPopup, LevelSelect, tutorials, laserTutorial;
    public TextMeshProUGUI titleTxt, tutorialTxt;

    public bool isPopupWindowsShown;
    public bool isSelectUserShown;
    public bool isAudioSettingsShown;
    public bool isExitPopupShown;
    public bool isLevelSelectShown;
    public bool game1;
    public bool game2;
    public bool game3;
    public bool game4;
    public bool game5;
    public bool game6;
    public bool game7;
    public bool game8;
    public bool game9;
    public bool game10;
    public float bgmVolume;
    public float sfxVolume;
    public string username;

    private List<UserData> userdata = new List<UserData>();
    private int gameToRun;

	private void Awake() {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves/")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
        }

        userdata = SaveData.ReadFromJSON<UserData>(filename);
        foreach (UserData u in userdata) {
            username = u.playerName;
            game1 = u.hasGame1;
            game2 = u.hasGame2;
            game3 = u.hasGame3;
            game4 = u.hasGame4;
            game5 = u.hasGame5;
            game6 = u.hasGame6;
            game7 = u.hasGame7;
            game8 = u.hasGame8;
            game9 = u.hasGame9;
            game10 = u.hasGame10;
            bgmVolume = u.bgmVolume;
            sfxVolume = u.sfxVolume;
        }

        if (!MusicPlayer.instance.IsTrackPlaying) {
            MusicPlayer.instance.PlayTrack();
        }
    }
	void Start(){
        isPopupWindowsShown = false;
        isSelectUserShown = false;
        isAudioSettingsShown = false;
        isExitPopupShown = false;
        isLevelSelectShown = false;
        gameToRun = 0;

        popupWindows.SetActive(isPopupWindowsShown);
        ExitPopup.SetActive(isExitPopupShown);
        tutorials.SetActive(false);
        laserTutorial.SetActive(false);

        audioSettings = AudioSettingsPopup.GetComponent<AudioSettingsHandleinator>();
        AudioSettingsPopup.SetActive(isAudioSettingsShown);
        MusicPlayer.instance.SetVolume(bgmVolume);
        AudioVariables audioVars = FindObjectOfType<AudioVariables>();
        audioVars.SFXVolume = sfxVolume;
        audioVars.BGMVolume = bgmVolume;
    }

    void Update(){
		if (Input.GetKeyDown(KeyCode.S)) {
            SaveToFile();
		}
    }

    public void RunGame1() {
        Debug.Log("Running ping pong");
        ShowTutorial("PING PONG", "Game ini cocok dimainkan oleh dua orang. Masing-masing pemain harus menggeser balok untuk memantulkan bola sehingga masuk ke gawang pemain lainnya. Pemain pertama yang mencapai 5 poin adalah pemenangnya!", 1);
    }
    public void RunGame2() {
        Debug.Log("Selecting laser level");
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        SceneManager.LoadScene("Laser1");
    }
    public void RunGame3() {
        Debug.Log("Running game 3");
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        SceneManager.LoadScene("Laser2");
    }
    public void RunGame4() {
        Debug.Log("Running game 4");
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        SceneManager.LoadScene("Laser3");
    }
    public void RunGame5() {
        Debug.Log("Running game 5");
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        SceneManager.LoadScene("Laser4");
    }
    public void RunGame6() {
        Debug.Log("Running game 6");
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        SceneManager.LoadScene("Laser5");
    }
    public void RunGame7() {
        Debug.Log("Running sequence");
        ShowTutorial("MENGHAFAL POLA", "Dalam game ini kamu akan melihat beberapa pola warna, hafalkan dan tunggu giliranmu untuk bermain. Pencet panah berwarna sesuai dengan pola yang kamu ingat dan capai skor paling tinggi!", 7);
    }
    public void RunGame8() {
        Debug.Log("Running sorting");
        ShowTutorial("RANTAI MAKANAN", "Di sawah, terdapat berbagai macam makhluk hidup. Makhluk hidup membutuhkan makanan untuk bertahan hidup. Mari kita urutkan rantai makanan dari bawah ke atas!", 8);
    }
    public void RunGame9() {
        Debug.Log("Running game 9");
    }
    public void RunGame10() {
        Debug.Log("Running game 10");
    }

    public void PopupWindowsToggle() {
        isPopupWindowsShown = !isPopupWindowsShown;
        popupWindows.SetActive(isPopupWindowsShown);
        AudioManager.instance.Play("vs-pop-4", volume: sfxVolume, loop: false);
    }
    public void AudioSettingsToggle() {
        Debug.Log("Toggling audio settings");
        PopupWindowsToggle();
        isAudioSettingsShown = !isAudioSettingsShown;
        AudioSettingsPopup.SetActive(isAudioSettingsShown);
        AudioManager.instance.Play("vs-pop-4", volume: sfxVolume, loop: false);
    }
    public void LevelSelectToggle() {
        PopupWindowsToggle();
        isLevelSelectShown = !isLevelSelectShown;
        LevelSelect.SetActive(isLevelSelectShown);
        AudioManager.instance.Play("vs-pop-4", volume: sfxVolume, loop: false);
    }
    public void SaveAudioSettings() {
        SaveToFile();
        AudioSettingsToggle();
        AudioManager.instance.Play("confirm-button", volume: sfxVolume, loop: false);
    }
    public void DevButton() {
        Debug.Log("Going to developer's website");
        AudioManager.instance.Play("open-button-1", volume: sfxVolume, loop: false);
    }
	public void ExitDialogToggle() {
        Debug.Log("Toggling exit dialog");
        PopupWindowsToggle();
        isExitPopupShown = !isExitPopupShown;
        ExitPopup.SetActive(isExitPopupShown);
        AudioManager.instance.Play("vs-pop-4", volume: sfxVolume, loop: false);
    }
    public void ShowTutorial(string title, string tutorial, int GTR) {
        popupWindows.SetActive(true);
        tutorials.SetActive(true);
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        titleTxt.text = title;
        tutorialTxt.text = tutorial;
        gameToRun = GTR;
	}
    public void ShowLaserTutorial() {
        popupWindows.SetActive(true);
        laserTutorial.SetActive(true);
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);

    }
    public void HideTutorial() {
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
        popupWindows.SetActive(false);
        tutorials.SetActive(false);
        gameToRun = 0;
    }
    public void HideLaserTutorial() {
        popupWindows.SetActive(false);
        laserTutorial.SetActive(false);
        AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
    }
    public void TutorialRunGame() {
        popupWindows.SetActive(false);
        tutorials.SetActive(false);
        switch (gameToRun) {
            case 1:
                AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
                SceneManager.LoadScene("PingPong"); break;
            case 7:
                AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
                SceneManager.LoadScene("Sequence"); break;
            case 8:
                AudioManager.instance.Play("button-click-1", volume: sfxVolume, loop: false);
                SceneManager.LoadScene("Sorting"); break;
            default: Debug.LogWarning("No game to run!");
                break;
        }
	}
    public void QuitApp() {
        Application.Quit();
	}
	private void OnApplicationQuit() {
        Debug.Log("Quitting application");
	}
	public void SaveToFile() {
        userdata.Clear();
        userdata.Add(new UserData(username, game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, bgmVolume, sfxVolume));
        SaveData.SaveToJSON(userdata, filename);
        Debug.Log("data saved");
    }

}
