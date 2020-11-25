using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SurvivorPersonAgent : Agent
{
	private const float NUTRITION_FULL = 1f;
	
	[Header("Movement")]
	[SerializeField] private float movementSpeedMultiplier = 0.2f;
	[SerializeField] private float rotationSpeedMultiplier = 4f;

	[Header("View Cone")]
	[SerializeField] private float viewConeMaxRadius = 10f;
	[SerializeField] private float viewConeMaxDistance = 10f;
	[Range(0, 180f)]
	[SerializeField] private float viewConeAngle = 60f;

	private float nutrition = 0.5f;
	private Physics extendedPhysics;

	private Coroutine lookForFoodCoroutine;

	private GameObject newFoodTarget;

	private void Start()
	{
		lookForFoodCoroutine = StartCoroutine(LookForFood());
	}

	public override void CollectObservations(VectorSensor sensor)
    {
		sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
		Vector3 movement = Vector3.zero;
		Vector3 turning = Vector3.zero;
		turning.y = vectorAction[0];
		movement.z = vectorAction[1];

		transform.Translate(movement * movementSpeedMultiplier);
		transform.Rotate(turning * rotationSpeedMultiplier);

	}

	public override void Heuristic(float[] actionsOut)
    {
		actionsOut[0] = Input.GetAxis("Horizontal");
		actionsOut[1] = Input.GetAxis("Vertical");
	}

	private void OnTriggerEnter(Collider other)
	{
		nutrition += other.gameObject.GetComponent<Food>()?.Consume() ?? 0;
	}

	private IEnumerator LookForFood()
	{
		RaycastHit closestHit;
		while (true)
		{
			if (FindClosestHit(CastGaze(), out closestHit)) newFoodTarget = closestHit.collider.gameObject;
			yield return new WaitForSeconds(0.25f);
		}
	}

	private RaycastHit[] CastGaze()
	{
		return extendedPhysics.ConeCastAll(transform.position, viewConeMaxRadius, transform.forward, viewConeMaxDistance, viewConeAngle);
	}

	private bool FindClosestHit(RaycastHit[] raycastHits, out RaycastHit closestHit)
	{

		int closestHitIndex = 0;
		float pointDistance;
		float closestHitDistance = viewConeMaxDistance;
		
		for (int i = 0; i < raycastHits.Length; i++)
		{
			pointDistance = CalculateDistance(raycastHits[i]);
			if (pointDistance < closestHitDistance)
			{
				closestHitDistance = pointDistance;
				closestHitIndex = i;
			}
		}
		closestHit = raycastHits[closestHitIndex];

		return (raycastHits.Length > 0) ? true : false;
	}

	private float CalculateDistance(RaycastHit raycastHit)
	{
		float squaredDistance = (raycastHit.point - transform.position).sqrMagnitude;
		return squaredDistance * squaredDistance;
	}
}
