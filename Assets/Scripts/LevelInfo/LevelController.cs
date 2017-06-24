using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	
	//префабы панелек
	public GameObject winPrefab;
	public GameObject losePrefab;
	int foodNumber = 0;
	int lifesNumber = 3;
	public int maxFood = 0;
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
	public void addFood(){
		foodNumber++;

	}
	public int getFood() {
		return foodNumber;
	}
	public int getMaxFoodNumber() {

		return maxFood;
	}

	void decreaseLifeNumber() {
		if (lifesNumber <= 0) {
			//создаем LosePanel
			createLosePanel();
		} else {
			lifesNumber--;
		}
	}
   
	//создаем LosePanel
	void createLosePanel(){
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		LosePanel popup = obj.GetComponent<LosePanel>();
		Time.timeScale = 0;
	}
	//создаем WinPanel
	void createWinPanel(){
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		WinPanel popup = obj.GetComponent<WinPanel>();
		Time.timeScale = 0;
	}

}
