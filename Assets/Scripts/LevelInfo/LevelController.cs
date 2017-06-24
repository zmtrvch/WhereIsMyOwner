using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	

	int lifesNumber = 3;

	public static LevelController current;
	void Awake() {
		current = this;
	}


	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}

	public void onCatDeath(Cat cat) {
		decreaseLifeNumber ();
		cat.transform.position = this.startingPosition;
	}


	//життя
	public int getLifes() {
		return lifesNumber;
	}

	public void addLife(){
		if (lifesNumber < 3) lifesNumber++;

	}
	void decreaseLifeNumber() {
		if (lifesNumber <= 0) {
			//создаем LosePanel
		} else {
			lifesNumber--;
		}
	}


}
