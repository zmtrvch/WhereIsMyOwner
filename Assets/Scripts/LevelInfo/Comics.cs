using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Comics : MonoBehaviour {


	void Start () {
		StartCoroutine (comics(2.7f));
		
	}
	
	public IEnumerator comics(float time){
		yield return new WaitForSeconds (time);
		SceneManager.LoadScene ("Levels");
	}
}
