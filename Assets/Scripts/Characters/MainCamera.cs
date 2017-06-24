using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

	public MainCamera cat;

	void Update()
	{
		Transform cat_transform = cat.transform;
		Transform camera_transform = this.transform;

		Vector3 cat_position = cat_transform.position;
		Vector3 camera_position = camera_transform.position;

		camera_position.x = cat_position.x;
		camera_position.y = cat_position.y;

		camera_transform.position = camera_position;
	}
}