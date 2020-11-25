using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : MonoBehaviour
{
	private const float NUTRITION_FULL = 1f;
	private float nutritionLevel = 0.5f;

	private void OnTriggerEnter(Collider other)
	{
		nutritionLevel += other.gameObject.GetComponent<Food>()?.Consume() ?? 0;
	}

}
