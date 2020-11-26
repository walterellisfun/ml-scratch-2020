using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : FreeSpeechEventSender<Food>
{
	private readonly float nutritionalValue = 0.2f;

	public float Consume()
	{
		SendEvent(this); //return to pool
		return nutritionalValue;
	}
}
