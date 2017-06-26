using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinPanel : MonoBehaviour {
	
	public MyButton closeButton;
	public MyButton blackBackground;
	public MyButton nextButton;
	public MyButton replayButton;



	void Start () {
		
		closeButton.signalOnClick.AddListener (this.close);
		blackBackground.signalOnClick.AddListener (this.close);
		nextButton.signalOnClick.AddListener (this.next);
		replayButton.signalOnClick.AddListener (this.replay);
	}


	void close () {
		SceneManager.LoadScene ("Levels");
		Destroy (this.gameObject);
		Time.timeScale = 1;
	}

	void next(){
		SceneManager.LoadScene ("Levels");
		Time.timeScale = 1;

	}
	void replay(){
		Time.timeScale = 1;
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);

	}



}