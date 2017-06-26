using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteLug : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider)
	{
		Cat cat = collider.GetComponent<Cat>();
		if (cat != null)
		{

			if (SceneManager.GetActiveScene().name=="Level1") {
				LevelController.isLevel1Completed = true;
				//Debug.Log (SceneManager.GetActiveScene().name=="Level1");
				PlayerPrefs.SetInt ("isLevel1Completed", 1);
				PlayerPrefs.Save ();
			}else  if (SceneManager.GetActiveScene().name=="Level2"){

				LevelController.isLevel2Completed = true;
				PlayerPrefs.SetInt ("isLevel2Completed", 1);
				PlayerPrefs.Save ();

			}


			LevelController.current.createWinPanel ();
		}


	}
}