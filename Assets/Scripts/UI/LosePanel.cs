﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LosePanel : MonoBehaviour {
	public static bool isSound=true;
	public AudioClip music;
	AudioSource musicSource;

	public MyButton closeButton;
	public MyButton blackBackground;
	public MyButton menuButton;
	public MyButton replayButton;


	void Start () {
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music; 
		musicSource.loop = false; 
		if(isSound)
			musicSource.Play ();
		closeButton.signalOnClick.AddListener (this.close);
		blackBackground.signalOnClick.AddListener (this.close);
		menuButton.signalOnClick.AddListener (this.menu);
		replayButton.signalOnClick.AddListener (this.replay);
	}

	void close () {
		menu ();
		Time.timeScale = 1;
	}

	void menu(){
		SceneManager.LoadScene ("Levels");
		Time.timeScale = 1;

	}
	void replay(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}
}