﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class MyButton : MonoBehaviour {
	private void Awake()
	{

	}
	public UnityEvent signalOnClick = new UnityEvent();


	public void _onClick() {
		this.signalOnClick.Invoke ();
	}
}