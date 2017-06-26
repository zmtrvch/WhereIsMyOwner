using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MousesPanel : MonoBehaviour {

	public int numberOfNulls; 
	public UILabel label;
	int currentNumber;
	int number=0;

	void FixedUpdate () {
		if (SceneManager.GetActiveScene ().name != "Levels") {
			currentNumber = LevelController.current.getMouses ();
			Debug.Log (currentNumber);
			writeCoins (currentNumber);
		} else {
			number = (LevelController.collectedMouses);	
			Debug.Log ("number" + number);
			label.text = "000" +number.ToString();
		}

	}

	int getNumber(int number) {

		while (number != 0) {
			number /= 10;
		}
		return numberOfNulls - number;
	}

	void writeCoins(int currentNumber ) {
		string str = "";
		for (int i = 0; i < getNumber (currentNumber); ++i) {
			str += "0";
		}
		str += currentNumber.ToString ();
		label.text = str;
	}
}
