using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Luggage1 : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		SceneManager.LoadScene ("Level1");
		Debug.Log ("Level1");
	}
}
