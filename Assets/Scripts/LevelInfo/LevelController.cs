using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	//префабы панелек
	public GameObject winPrefab;
	public GameObject losePrefab;
	public GameObject settingsPrefab;
	int mousesNumber=0;
	int foodNumber = 0;
	int lifesNumber = 3;
	public int maxFood = 0;
	public static LevelController current;
	public static bool isMusicOn;
	public static bool isLevel1Completed;
	public static bool isLevel2Completed;

	public static int collectedMouses;
	public AudioClip music = null;
	AudioSource musicSource = null;
	public MyButton pause;
	int level1Copl ;
	void Awake() {
		current = this;
		//sounds
		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		//musicSource.loop = true;

		int Level1Complated = PlayerPrefs.GetInt ("isLevel1Completed", 0);
		if (Level1Complated == 1)
			isLevel1Completed = true;
		else
			isLevel1Completed = false;


		this.pause.signalOnClick.AddListener (this.showSettings);


	}

	void Start(){
		int SoundOn = PlayerPrefs.GetInt ("isMusicOn", 0);
		Debug.Log ("son" +SoundOn);
		if (SoundOn == 0){
			setMusicOff();
			Debug.Log ("son OFF");
		}
		else{
			setMusicOn();
			Debug.Log ("son ON");}
		collectedMouses = PlayerPrefs.GetInt ("collectedMouses",0);
		//Debug.Log ("collectedMouses" +collectedMouses );
		//level1Copl = PlayerPrefs.GetInt ("isLevel1Completed", 0);

		//PlayerPrefs.SetInt ("isMusicOn",2);
		//Debug.Log (PlayerPrefs.GetInt("music"));

		//if(PlayerPrefs.GetInt("music")!=0){
		//	musicSource.Play ();}

	}


	public void addMouses(int number)
	{
		mousesNumber ++;
		PlayerPrefs.SetInt ("collectedMouses",mousesNumber );
		PlayerPrefs.Save ();
	}
	public int getMouses() {
		return mousesNumber;
	}
	Vector3 startingPosition;
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}

	public void onCatDeath(Cat cat) {
		decreaseLifeNumber ();
		cat.transform.position = this.startingPosition;
	}

	//музика
	public void setMusicOff(){
		isMusicOn = false;
		musicSource.Stop ();
		PlayerPrefs.SetInt ("isMusicOn", 0);
		PlayerPrefs.Save ();

	}

	public void setMusicOn(){
		isMusicOn = true;

		musicSource.Play ();
		PlayerPrefs.SetInt ("isMusicOn",1);
		PlayerPrefs.Save ();
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
	public	void createLosePanel(){
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		LosePanel popup = obj.GetComponent<LosePanel>();
		//	Time.timeScale = 0;
	}
	//создаем WinPanel
	public void createWinPanel(){
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		WinPanel popup = obj.GetComponent<WinPanel>();
		collectedMouses += mousesNumber;
		PlayerPrefs.SetInt ("collectedMouses",collectedMouses);
		PlayerPrefs.Save ();
		//	Time.timeScale = 0;
	}

	//settings
	public	void showSettings() {
		GameObject parent = UICamera.first.transform.parent.gameObject;
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		SettingPanel popup = obj.GetComponent<SettingPanel>();
		//	Time.timeScale = 0;
	} 

}
