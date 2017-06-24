using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Collectable {

	void Start(){
		
	}


	protected override void OnCatHit (Cat cat)
	{
		
		PlayerPrefs.SetInt (this.name.ToString (), 1);

		LevelController.current.addFood ();
		this.CollectedHide ();
	}
}

