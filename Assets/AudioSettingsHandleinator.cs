using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CarterGames.Assets.AudioManager;

public class AudioSettingsHandleinator : MonoBehaviour{
    MainMenuControlinator mainMenu;
    public Slider BGMSlider, SFXSlider;
	private void Start() {
		mainMenu = FindObjectOfType<MainMenuControlinator>();
		BGMSlider.value = mainMenu.bgmVolume;
		SFXSlider.value = mainMenu.sfxVolume;
		MusicPlayer.instance.SetVolume(mainMenu.bgmVolume);
		AudioVariables audioVars = FindObjectOfType<AudioVariables>();
		audioVars.SFXVolume = SFXSlider.value;
		audioVars.BGMVolume = BGMSlider.value;
	}
    public void setBGMVolume() {
		mainMenu.bgmVolume = BGMSlider.value;
		MusicPlayer.instance.SetVolume(mainMenu.bgmVolume);
	}
    public void setSFXVolume() {
		mainMenu.sfxVolume = SFXSlider.value;
	}
}
