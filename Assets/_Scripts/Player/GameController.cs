using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	// Use this for initialization
	public int 				speedTimer;
	public float 			rollSpeed;
	public Transform 		plane;
	public float			rotateSpeed;
	public float			maxBoundary;
	public float			maxSpeed;
	public float			maxBoostTime;
	public float			boostCooldownRate;
	public float			chkBoundaryDeadZone;
	public float			minShake;
	public float			maxShake;
	public float			deadZone;
	public Camera			playerCamera;

	private float			_speed;
	private float			_tempMaxSpeed;
	private Rigidbody 		player;
	private float			currentBoostTime;
	private float			currentBoostCooldown;
	private Vector3			originalCameraPos;
	private bool			isShake;


	void Start ()
	{
		_speed = 0;
		player = GetComponent<Rigidbody>();
		currentBoostTime = maxBoostTime;
		currentBoostCooldown = boostCooldownRate;
		_tempMaxSpeed = maxSpeed;
		originalCameraPos = playerCamera.transform.position;
		isShake = false;
	}

	void Update()
	{
		Movement();
	}

	void FixedUpdate ()
	{
		ShipFacing();
	}

	void Movement()
	{
		//Speed Limitations 
		if (_speed >= maxSpeed)
			_speed = maxSpeed;
		else if (_speed <= 0)
			_speed = 0;			
		if (Input.GetKey(KeyCode.W))
			_speed += 2f * 4f;
		if (Input.GetKey(KeyCode.S))
			_speed -= 1f * 6f;
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.back * -rollSpeed * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.back * rollSpeed * Time.deltaTime, Space.Self);
		}
		if (Input.GetKey(KeyCode.LeftShift))
		{
			isShake = true;
			//When current boost time is maxBoost time
			if (currentBoostTime == maxBoostTime)
			{
				InvokeRepeating("invokeBoost", 0f, 1f);
				maxSpeed = _tempMaxSpeed * 1.5f;
			}
			// For as long as boost time is greater than zero and the key is being pressed make sure you increase speed / shake screen
			else if (currentBoostTime > 0)
			{
				if (isShake)
				{
					//TODO: Shaking camera - imitating that you're doing faster
				}
				//_speed = (_speed / 2) + _speed * 1.5f;
			}
			//Else invoke a cooldown rate and reset currentBoostTime
			else
			{
				if (currentBoostCooldown == boostCooldownRate)
					InvokeRepeating("invokeBoostCooldown", 0f, 1f);
				else
					Debug.Log("Current time to next boost: " + currentBoostCooldown);
			}
		}
		else
		{
			isShake = false;
		}
		if (_speed > 0)
			transform.Translate(Vector3.forward * _speed * Time.deltaTime * speedTimer);
		else
			transform.Translate(0f, 0f, 0f);
	}

	void ShipFacing()
	{
		Vector2 centerScreen = new Vector2(Screen.width / 2, Screen.height / 2);
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		float xDiff;
		float yDiff;

		yDiff = mousePos.y - centerScreen.y;
		xDiff = mousePos.x - centerScreen.x;

		//MouseOffset is inverted (y == x) && (x == y)
		Vector2 mouseOffset = new Vector2((yDiff) * -1, xDiff);

		//Check Deadzone near Center of screen
		if (xDiff >= deadZone || xDiff <= -deadZone || yDiff >= deadZone || yDiff <= -deadZone)
		{
			if (mouseOffset.x >= rotateSpeed)
				mouseOffset.x = rotateSpeed;
			else if (mouseOffset.x <= -rotateSpeed)
				mouseOffset.x = -rotateSpeed;
			if (mouseOffset.y >= rotateSpeed)
				mouseOffset.y = rotateSpeed;
			else if (mouseOffset.y <= -rotateSpeed)
				mouseOffset.y = -rotateSpeed;
			player.transform.Rotate(mouseOffset * Time.deltaTime * 0.2f);
		}  

	}

//	void worldBoundry(Vector3 playerPos)
//	{
//		// Check set boundry - player cannot move out of this space
//		if (Mathf.Ceil(playerPos.x) >= maxBoundary || Mathf.Ceil(playerPos.x) <= -maxBoundary ||
//			Mathf.Ceil(playerPos.y) >= maxBoundary || Mathf.Ceil(playerPos.y) <= -maxBoundary ||
//			Mathf.Ceil(playerPos.z) >= maxBoundary || Mathf.Ceil(playerPos.z) <= -maxBoundary)
//		{
//			TODO:  Shoot out Message + timer and explode ship
//		}
//	}

	public float currentSpeed()
	{
		return (_speed);
	}

	void invokeBoost()
	{
		currentBoostTime--;
		if (currentBoostTime == 0f)
		{
			
			currentBoostCooldown = boostCooldownRate;
			maxSpeed = _tempMaxSpeed;
			_speed = Mathf.Lerp(_speed, maxSpeed, 0.4f);
			CancelInvoke();
		}
	}

	void invokeBoostCooldown()
	{
		currentBoostCooldown--;
		if (currentBoostCooldown == 0f)
		{
			currentBoostTime = maxBoostTime;
			CancelInvoke();
		}
	}
}