using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPanel : MonoBehaviour {
	int currentNumber;
	public UILabel label;

	void FixedUpdate () {
		currentNumber = LevelController.current.getFood();
		if (currentNumber <= LevelController.current.getMaxFoodNumber()) {
			countFruit ();
		}
	}

	void countFruit() {
		label.text = LevelController.current.getFood().ToString ()+ "/" + LevelController.current.getMaxFoodNumber();
	}

}