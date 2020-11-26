using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnTimer<T, U> : ObjectSpawner<T, U>
	where T : ObjectPool<U>
	where U : Component
{
	[SerializeField] private int initialAmount = 10;
	[SerializeField] private float intervalSeconds = 2f;

	private Coroutine coroutineReference;

	private void Start()
	{
		SpawnInitial();
		coroutineReference = StartCoroutine(SpawnTimerCoroutine());
	}

	private void SpawnInitial()
	{
		for (int i = 0; i < initialAmount; i++)
		{
			SpawnObject();
		}
	}

	private IEnumerator SpawnTimerCoroutine()
	{
		while (true)
		{
			SpawnObject();
			yield return new WaitForSeconds(2f);
		}
	}
}
