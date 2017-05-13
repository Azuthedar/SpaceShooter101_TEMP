using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization

	//Set PlayerGameObject
	public GameObject player;

	private Vector3 _offset;
	void Start ()
	{
		_offset = transform.position + player.transform.position;
	}

	void LateUpdate()
	{
		transform.position = _offset;
	}
}
