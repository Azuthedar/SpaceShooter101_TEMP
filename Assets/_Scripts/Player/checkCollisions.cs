using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkCollisions : MonoBehaviour {


	public Object		_playerShip;

	void OnCollisionEnter(Collision collider)
	{
		//Destroy object if it enters another collision box
		if (collider.gameObject.tag == "Asteroids")
			Destroy(_playerShip);
		Debug.Log("You have hit something!");
	}
}
