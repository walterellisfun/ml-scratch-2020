using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
	[SerializeField] private GameObject foodPrefab;
	private List<GameObject> foodList = new List<GameObject>();

	private static Coroutine spawnTimer;

	private void Start()
	{
		spawnTimer = StartCoroutine(SpawnTimerCoroutine());
	}

	private IEnumerator SpawnTimerCoroutine()
	{
		while (true)
		{
			Vector3 randomPosition = GetNewRandomPosition();
			foodList.Add(Instantiate(foodPrefab, randomPosition, Quaternion.identity, transform));
			yield return new WaitForSeconds(2f);
		}
	}

	private Vector3 GetNewRandomPosition()
	{
		Vector3 randomPosition = Vector3.zero;
		randomPosition.x = Random.Range(-40f, 40f);
		randomPosition.z = Random.Range(-40f, 40f);
		return randomPosition;
	}
}
