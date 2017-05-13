using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InstantiateRandomAsteroids : MonoBehaviour {

	List<GameObject> 	asteroidList;

	public int			asteroidSpeed;
	public int			maxAsteroids;
	public GameObject 	asteroid1;
	public GameObject	asteroid2;
	public GameObject	asteroid3;
	public GameController _player;
	public float		maxScale;
	public float		minScale;

	private Component	_playerScript;
	private float		_mapBoundary;
	private int			i;
	private GameObject	instantiatedObject;


	void Start ()
	{
		_mapBoundary = _player.maxBoundary;
		asteroidList = new List<GameObject>();
		i = 0;
		asteroidList.Add(asteroid1);
		asteroidList.Add(asteroid2);
		asteroidList.Add(asteroid3);

	}
	
	// Update is called once per frame
	void Update ()
	{
		while (i < maxAsteroids)
		{
			instantiatedObject = Instantiate(asteroidList[Random.Range(0, asteroidList.Count)],
				randomAsteroidPos(), randomAsteroidRotation(), this.transform);
			instantiatedObject.transform.localScale = randomAsteroidScale();
			instantiatedObject.tag = "Asteroids";
			i++;
		}
	}

	Vector3 randomAsteroidPos()
	{
		float x = Random.Range(-_mapBoundary, _mapBoundary);
		float y = Random.Range(-_mapBoundary, _mapBoundary);
		float z = Random.Range(-_mapBoundary, _mapBoundary);

		Vector3 randomPos = new Vector3(x, y, z);
		return (randomPos);
	}

	Quaternion randomAsteroidRotation()
	{
		float minRot, maxRot;

		minRot = -180;
		maxRot = 180;

		float x = Random.Range(minRot, maxRot);
		float y = Random.Range(minRot, maxRot);
		float z = Random.Range(minRot, maxRot);

		Quaternion randomRotation = new Quaternion(x, y, z, 1);
		return (randomRotation);
	}

	Vector3 randomAsteroidScale()
	{
		float unisonScale = Random.Range(minScale, maxScale);

		Vector3 objectScale = new Vector3(unisonScale, unisonScale, unisonScale);
		return (objectScale);
	}
}
