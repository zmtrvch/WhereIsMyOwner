using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
	public static SoundController current;
	public Cat cat;
	public bool music=true;

	void Awake() {
		current = this;
	}

	void Start () {
		int music = PlayerPrefs.GetInt ("music",-1);
		if (music == 1||music==-1) {
			this.music = true;
		}else if(music==0){
			this.music = false;
		}


		changeMusic ();
		changeMusic ();

	}

	public void changeMusic(){
		if (music) {
			music = false;
			PlayerPrefs.SetInt ("music",0);
			PlayerPrefs.Save ();
			LevelController.current.setMusicOff ();
		} else {
			music = true;
			PlayerPrefs.SetInt ("music",1);
			PlayerPrefs.Save ();
			LevelController.current.setMusicOn ();
		}

	}



}