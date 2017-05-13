using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedIndicator : MonoBehaviour {

	public GameController 	player;
	public Texture2D 		frame;
	public Texture2D 		speedBar;
	public Texture2D 		boostBar;
	public float			height = 1;
	public float			width = 1;
	public float			barSizeBuffer;
	public float			barPosBuffer;

	private Rect			framePos;
	private Rect			speedBarPos;
	private Rect			boostBarPos;
	private float			_maxSpeed;
	private float			_speed;

	void Start()
	{
		framePos.height = height;
		framePos.width = width;
//		_speedBarPos.height = height;
//		_speedBarPos.width = width;
//		_boostBarPos.height = height;
//		_boostBarPos.width = width;

		_maxSpeed = player.maxSpeed;
	}

	void Update()
	{
	}

	void OnGUI()
	{
		//Set Boost || Speed bar according to their frames.
		framePos = calcPosition(framePos, 1f, "left", "center");
		drawTexture(framePos, frame);

		speedBarPos = framePos;
		_speed = player.currentSpeed();
		//Update height || width (depending on vertical or horizontal bar)
		if (_speed > 0 && _speed < _maxSpeed)
			speedBarPos.height *= (_speed / _maxSpeed) * barSizeBuffer;
		else if (_speed >= _maxSpeed)
			speedBarPos.height *= (_maxSpeed / _maxSpeed) * barSizeBuffer;
		else
			speedBarPos.height *= (0 / _maxSpeed);
		drawTexture(speedBarPos, speedBar);

		framePos = calcPosition(framePos, 1f, "right", "center");
		boostBarPos = framePos;
		drawTexture(framePos, frame);
	}

	// positionString NOTE: possible values: center, left, right, top, bottom
	Rect calcPosition(Rect position, float offsetTimes = 1f, string xPositionString = "center", string yPositionString = "center")
	{
		position.x = (Screen.width - position.width) / 2 * determineLocation(xPositionString) * offsetTimes;
		position.y = (Screen.height - position.height) / 2 * determineLocation(yPositionString) * offsetTimes;

		return (position);
	}

	void drawTexture(Rect texturePos, Texture2D imageTexture)
	{
		GUI.DrawTexture(texturePos, imageTexture);
	}

	float determineLocation(string positionString)
	{
		if (positionString.Equals("center"))
			return (1f);
		else if (positionString.Equals("left") || positionString.Equals("top"))
			return (0.5f);
		else if (positionString.Equals("right") || positionString.Equals("bottom"))
			return (1.5f);
		return (1);
	}
}
