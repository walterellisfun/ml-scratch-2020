using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner<T, U> : MonoBehaviour 
	where T : ObjectPool<U>
	where U : Component
{
	[Header("Spawn Area")]
	[SerializeField] private Vector2 spawnAreaX = new Vector2(-40f, 40f);
	[SerializeField] private Vector2 spawnAreaZ = new Vector2(-40f, 40f);

	private T pool;

	private void Awake()
	{
		pool = GetComponent<T>();
	}

	protected void SpawnObject()
	{
		GameObject spawnedObject = pool.GetObject().gameObject;
		SetTransform(spawnedObject);
	}

	private void SetTransform(GameObject _spawnedObject)
	{
		_spawnedObject.transform.parent = transform;
		_spawnedObject.transform.position = GetNewRandomPosition();
		_spawnedObject.gameObject.SetActive(true);
	}

	private Vector3 GetNewRandomPosition()
	{
		Vector3 randomPosition = Vector3.zero;
		randomPosition.x = Random.Range(spawnAreaX.x, spawnAreaX.y);
		randomPosition.z = Random.Range(spawnAreaZ.x, spawnAreaZ.y);
		return randomPosition;
	}
}
