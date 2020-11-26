using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
	[SerializeField] private T prefab;

	private Queue<T> objects = new Queue<T>();

	public T GetObject()
	{
		if (objects.Count == 0) AddObjects(1);
		return objects.Dequeue();
	}

	protected void ReturnToPool(T objectToReturn)
	{
		objectToReturn.gameObject.SetActive(false);
		objects.Enqueue(objectToReturn);
	}

	private void AddObjects(int count)
	{
		for (int i = 0; i < count; i++)
		{
			var newObject = Instantiate(prefab);
			newObject.gameObject.SetActive(false);
			objects.Enqueue(newObject);
		}
	}
}
