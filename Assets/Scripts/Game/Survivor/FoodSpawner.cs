using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
	[SerializeField] private float intervalSeconds = 2f;

	private Coroutine spawnTimer;

	private void Start()
	{
		spawnTimer = StartCoroutine(SpawnTimerCoroutine());
	}

	private IEnumerator SpawnTimerCoroutine()
	{
		while (true)
		{

			yield return new WaitForSeconds(2f);
		}
	}
}
