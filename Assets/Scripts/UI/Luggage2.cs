﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Luggage2 : MonoBehaviour {
	public SpriteRenderer lug;
	public Sprite openlug;
	public Sprite closelug;

	// Use this for initialization
	void Start(){
		//if (LevelController.isLevel1Complated == true)
		//lug.sprite= openlug;
		//else
		lug.sprite = closelug;
	}
	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level2");
		openlug = closelug;

	}
}
