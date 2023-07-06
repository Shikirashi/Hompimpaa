using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettingsHandleinator : MonoBehaviour{
    MainMenuControlinator MainMenuControlinator;
    void Start(){
        MainMenuControlinator = FindObjectOfType<MainMenuControlinator>();
    }

    public void CancelAudioSettings() {
        MainMenuControlinator.isPopupWindowsShown = !MainMenuControlinator.isPopupWindowsShown;
	}
    public void SaveAudioSettings() {

	}
}
