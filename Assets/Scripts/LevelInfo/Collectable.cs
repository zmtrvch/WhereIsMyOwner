using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

	protected virtual void OnCatHit(Cat cat) {

	}

	void OnTriggerEnter2D(Collider2D collider) {

		Cat cat = collider.GetComponent<Cat>();
		if(cat != null) {
			this.OnCatHit (cat);
		}
	}
	public void CollectedHide() {
		Destroy(this.gameObject);
	}}