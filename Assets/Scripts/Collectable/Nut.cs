﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : Collectable {

	public Vector3 speed = new Vector3(2.0f, 0.0f, 0.0f);
	Rigidbody2D myBody = null;
	private float direction = 1.0f;
	public float lifeTime = 3.0f;
	void Start()
	{
		StartCoroutine(destroyLater());
	}

	public void launch(float direction)
	{
		this.direction = direction;
	}

	IEnumerator destroyLater()
	{
		yield return new WaitForSeconds(lifeTime);
		Destroy(this.gameObject);

	}


	public void FixedUpdate()
	{
		if (direction != 0)
		{
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			if (direction < 0)
			{
				sr.flipX = true;

			}
			else if (direction > 0)
			{
				sr.flipX = false;
			}

			if (Mathf.Abs(direction) > 0)
			{
				this.transform.position += direction * speed * Time.deltaTime;
			}
		}
	}  

	protected override void OnCatHit(Cat cat)
	{

		this.CollectedHide();

			cat.removeHealth (1);


	
}
}
