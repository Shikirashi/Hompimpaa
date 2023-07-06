using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using CarterGames.Assets.AudioManager;
using UnityEngine.SceneManagement;

public class MainMenuControlinator : MonoBehaviour{
    [SerializeField] string filename;

    public GameObject PopupWindows;
    public GameObject SelectUserPopup;
    public GameObject AudioSettingsPopup;
    public GameObject ExitPopup;
    public GameObject LevelSelect;
    public static AudioManager audioManager;

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
    public float masterVolume;
    public float bgmVolume;
    public float sfxVolume;
    public string username;

    private List<UserData> userdata = new List<UserData>();
    void Start(){
        Debug.Log(this.gameObject.name);
		if (!Directory.Exists(Application.persistentDataPath + "/Saves/")) {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves/");
		}

        userdata = SaveData.ReadFromJSON<UserData>(filename);
        foreach(UserData u in userdata) {
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
            masterVolume = u.masterVolume;
            bgmVolume = u.bgmVolume;
            sfxVolume = u.sfxVolume;
        }
        isPopupWindowsShown = false;
        isSelectUserShown = false;
        isAudioSettingsShown = false;
        isExitPopupShown = false;
        isLevelSelectShown = false;

        PopupWindows.SetActive(isPopupWindowsShown);
        SelectUserPopup.SetActive(isSelectUserShown);
        AudioSettingsPopup.SetActive(isAudioSettingsShown);
        ExitPopup.SetActive(isExitPopupShown);

        if (audioManager == null) {
            audioManager = FindObjectOfType<AudioManager>();
            DontDestroyOnLoad(audioManager.gameObject);
            audioManager.Play("LCB_Milkyway_Tea_Shop_noAmb_Loop", loop: true);
        }
    }

    void Update(){
		if (Input.GetKeyDown(KeyCode.S)) {
            SaveToFile();
		}
    }

    public void RunGame1() {
        Debug.Log("Running ping pong");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("PingPong");
    }
    public void RunGame2() {
        Debug.Log("Selecting laser level");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Laser1");
    }
    public void RunGame3() {
        Debug.Log("Running game 3");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Laser2");
    }
    public void RunGame4() {
        Debug.Log("Running game 4");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Laser3");
    }
    public void RunGame5() {
        Debug.Log("Running game 5");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Laser4");
    }
    public void RunGame6() {
        Debug.Log("Running game 6");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Laser5");
    }
    public void RunGame7() {
        Debug.Log("Running sequence");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Sequence");
    }
    public void RunGame8() {
        Debug.Log("Running sorting");
        audioManager.Play("button-click-1", loop: false);
        SceneManager.LoadScene("Sorting");
    }
    public void RunGame9() {
        Debug.Log("Running game 9");
    }
    public void RunGame10() {
        Debug.Log("Running game 10");
    }

    public void PopupWindowsToggle() {
        isPopupWindowsShown = !isPopupWindowsShown;
        PopupWindows.SetActive(isPopupWindowsShown);
        audioManager.Play("vs-pop-4", loop: false);
    }
    public void AudioSettingsToggle() {
        Debug.Log("Toggling audio settings");
        PopupWindowsToggle();
        isAudioSettingsShown = !isAudioSettingsShown;
        AudioSettingsPopup.SetActive(isAudioSettingsShown);
        audioManager.Play("vs-pop-4", loop: false);
    }
    public void LevelSelectToggle() {
        PopupWindowsToggle();
        isLevelSelectShown = !isLevelSelectShown;
        LevelSelect.SetActive(isLevelSelectShown);
        audioManager.Play("vs-pop-4", loop: false);
    }
    public void SaveAudioSettings() {
        SaveToFile();
        AudioSettingsToggle();
        audioManager.Play("confirm-button", loop: false);
    }
    public void DevButton() {
        Debug.Log("Going to developer's website");
        audioManager.Play("open-button-1", loop: false);
    }
    public void SelectUserToggle() {
        Debug.Log("Toggling user selection");
        PopupWindowsToggle();
        isSelectUserShown = !isSelectUserShown;
        SelectUserPopup.SetActive(isSelectUserShown);
        audioManager.Play("vs-pop-4", loop: false);
        //if shown, load user data
    }
	public void ExitDialogToggle() {
        Debug.Log("Toggling exit dialog");
        PopupWindowsToggle();
        isExitPopupShown = !isExitPopupShown;
        ExitPopup.SetActive(isExitPopupShown);
        audioManager.Play("vs-pop-4", loop: false);
    }
    public void QuitApp() {
        Application.Quit();
	}
	private void OnApplicationQuit() {
        Debug.Log("Quitting application");
	}
	public void SaveToFile() {
        userdata.Clear();
        userdata.Add(new UserData(username, game1, game2, game3, game4, game5, game6, game7, game8, game9, game10, masterVolume, bgmVolume, sfxVolume));
        SaveData.SaveToJSON(userdata, filename);
        Debug.Log("data saved");
    }

}
