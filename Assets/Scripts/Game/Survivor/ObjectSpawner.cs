using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectSpawner : MonoBehaviour
{
	private void SpawnObject()
	{
		Vector3 randomPosition = GetNewRandomPosition();
		//var spawnedObject = typeof(T).Instance.GetObject();
		spawnedObject.transform.parent = transform;
		spawnedObject.transform.position = GetNewRandomPosition();
		spawnedObject.gameObject.SetActive(true);
	}

	private Vector3 GetNewRandomPosition()
	{
		Vector3 randomPosition = Vector3.zero;
		randomPosition.x = Random.Range(-40f, 40f);
		randomPosition.z = Random.Range(-40f, 40f);
		return randomPosition;
	}
}
