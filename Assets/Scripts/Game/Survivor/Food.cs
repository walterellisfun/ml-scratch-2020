using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
	private readonly float nutritionalValue = 0.2f;

	public float Consume()
	{
		FoodPool.Instance.ReturnToPool(this);
		return nutritionalValue;
	}
}
