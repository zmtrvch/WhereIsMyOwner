using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : Collectable {

	protected override void OnCatHit (Cat cat)
	{

		LevelController.current.addMouses(1);
		this.CollectedHide ();
	}
}