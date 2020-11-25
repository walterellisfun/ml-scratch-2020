using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
	[Header("View Cone WIP")]
	[SerializeField] private float viewConeMaxRadius = 2f;
	[SerializeField] private float viewConeMaxDistance = 5f;
	[Range(0, 180f)]
	[SerializeField] private float viewConeAngle = 30f;

	[Header("Debug")]
	[SerializeField] private GameObject debugFoodSelectionIndicator;
	GameObject indicator;

	private Physics extendedPhysics;


	private Coroutine lookForFoodCoroutine;

	private GameObject newFoodTarget;

	private void Start()
	{
		lookForFoodCoroutine = StartCoroutine(DetectFood());

		indicator = Instantiate(debugFoodSelectionIndicator);
	}


	private IEnumerator DetectFood()
	{
		RaycastHit[] raycastHits;
		RaycastHit closestHit;
		while (true)
		{
			CastGaze(out raycastHits);
			if (raycastHits.Length > 0)
			{
				FindClosestHit(raycastHits, out closestHit);
				newFoodTarget = closestHit.collider.gameObject;
			}

			if (newFoodTarget != null) indicator.transform.position = newFoodTarget.transform.position;

			yield return new WaitForSeconds(0.25f);
		}
	}

	private void CastGaze(out RaycastHit[] raycastHits)
	{
		raycastHits = extendedPhysics.ConeCastAll(transform.position, viewConeMaxRadius, transform.forward, viewConeMaxDistance, viewConeAngle);
	}

	private void FindClosestHit(RaycastHit[] _raycastHits, out RaycastHit closestHit)
	{
		int closestHitIndex = 0;
		float squaredDistance;
		float closestHitDistance = viewConeMaxDistance;

		for (int i = 0; i < _raycastHits.Length; i++)
		{
			squaredDistance = CalculateDistance(_raycastHits[i]);
			if (squaredDistance < closestHitDistance * closestHitDistance)
			{
				closestHitDistance = squaredDistance;
				closestHitIndex = i;
			}
		}

		print(closestHitDistance);
		closestHit = _raycastHits[closestHitIndex];
	}

	private float CalculateDistance(RaycastHit raycastHit)
	{
		return (raycastHit.point - transform.position).sqrMagnitude;
	}
}
