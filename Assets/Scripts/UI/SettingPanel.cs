using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingPanel : MonoBehaviour {
	public Sprite musicOff;
	public Sprite musicOn;
	public MyButton closeButton;
	public MyButton menuButton;
	public MyButton replayButton;
	public MyButton backgroundButton;

	public static bool sound = true;

	void Start () {
		
		closeButton.signalOnClick.AddListener (this.close);
		menuButton.signalOnClick.AddListener (this.menu);

	    replayButton.signalOnClick.AddListener (this.replay);
		backgroundButton.signalOnClick.AddListener (this.close);
	
	}
	void replay(){

		if (replayButton.GetComponent<UIButton> ().normalSprite2D.name.Equals ("music-on")) {
			replayButton.GetComponent<UIButton> ().normalSprite2D = musicOff;
			Debug.Log ("off");
			LevelController.current.setMusicOff ();
		}
		else{
			replayButton.GetComponent<UIButton> ().normalSprite2D=musicOn;
			Debug.Log ("on");
			LevelController.current.setMusicOn ();
		}

	}
	void close(){
		Destroy (this.gameObject);
		Destroy (this.gameObject);
		Time.timeScale = 1;

	}
	void menu(){
		SceneManager.LoadScene ("Levels");
		Destroy (this.gameObject);
		Time.timeScale = 1;
	}
}
