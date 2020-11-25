using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	[Header("Movement")]
	[SerializeField] private float movementSpeedMultiplier = 0.2f;
	[SerializeField] private float rotationSpeedMultiplier = 4f;

	private void UpdateMovement(float[] vectorAction)
	{
		Vector3 movement = Vector3.zero;
		Vector3 turning = Vector3.zero;
		turning.y = vectorAction[0];
		movement.z = vectorAction[1];

		transform.Translate(movement * movementSpeedMultiplier);
		transform.Rotate(turning * rotationSpeedMultiplier);
	}
}
