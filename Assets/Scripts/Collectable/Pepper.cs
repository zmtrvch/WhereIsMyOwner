using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pepper : Collectable {

	protected override void OnCatHit (Cat cat)
	{
		
				cat.bombCat ();


		this.CollectedHide ();

	}
}