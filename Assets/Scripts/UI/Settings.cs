using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings: MonoBehaviour {
	public MyButton closeButton;
	public MyButton blackBackground;
	public MyButton musicButton;
	public MyButton menuButton;
	public Sprite buttonMusicOff;
	public Sprite buttonMusicOn;


	void Start () {
		if (!SoundController.current.music) {
			musicButton.GetComponent<UIButton> ().normalSprite2D = buttonMusicOff;
		}
		closeButton.signalOnClick.AddListener (this.close);
		blackBackground.signalOnClick.AddListener (this.close);
		menuButton.signalOnClick.AddListener (this.menu);
		musicButton.signalOnClick.AddListener (this.changeMusic);
	}

	// Update is called once per frame
	public void close () {
		Debug.Log ("Close");
		Destroy (this.gameObject);
		Time.timeScale = 1;
	}

	public void changeMusic(){			
		if (musicButton.GetComponent<UIButton> ().normalSprite2D.name.Equals("music-on"))
			musicButton.GetComponent<UIButton> ().normalSprite2D=buttonMusicOff;
		else
			musicButton.GetComponent<UIButton> ().normalSprite2D=buttonMusicOn;
		SoundController.current.changeMusic ();
	}

	public void menu(){
		SceneManager.LoadScene ("Levels");
		Time.timeScale = 1;
	}

}
