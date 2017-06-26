using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteLug : MonoBehaviour {
	void OnTriggerEnter2D(Collider2D collider)
	{
		Cat cat = collider.GetComponent<Cat>();
		if (cat != null)
		{
			
			LevelController.current.createWinPanel ();
			}


	}
}