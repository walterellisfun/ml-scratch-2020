using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Survivor : FreeSpeechEventSender<Survivor>
{
	private const float IDEAL_NUTRITION_LEVEL = 1f;
	private float nutritionLevel;
	private bool fightOver;
	private float? fightHealthOutcome;

	private void OnEnable()
	{
		nutritionLevel = 0.6f;
		fightOver = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		UpdateHealth(other);
	}

	private void UpdateHealth(Collider other)
	{
		nutritionLevel += other.transform.parent?.GetComponent<Food>()?.Consume() ?? 0;
		nutritionLevel = other.transform.GetComponent<Survivor>()?.Fight(nutritionLevel, fightOver) ?? nutritionLevel;
		nutritionLevel = fightHealthOutcome ?? nutritionLevel;
		fightHealthOutcome = null;
		fightOver = true;
		
		if (nutritionLevel <= 0) Die();
		if (nutritionLevel >= 2f) SpawnOffspring();
	}

	public float? Fight(float opponentNutritionLevel, bool _fightOver)
	{
		if (_fightOver)
		{
			fightOver = true;
			fightHealthOutcome = null;
			return null;
		}
		fightHealthOutcome = nutritionLevel - opponentNutritionLevel;
		return opponentNutritionLevel - nutritionLevel;
	}

	private void Die()
	{
		SendEvent(this); //return to pool
	}

	private void SpawnOffspring()
	{
		nutritionLevel = 1f;
		//send event to spawn new survivor elsewhere
	}
}
