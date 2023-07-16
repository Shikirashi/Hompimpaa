using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVariables : MonoBehaviour{
	public float SFXVolume, BGMVolume;
	private void Start() {
		AudioVariables[] audioVars = FindObjectsOfType<AudioVariables>();
		if(audioVars.Length > 1) {
			Destroy(this);
		}
		else {
			DontDestroyOnLoad(this);
		}
	}
}
