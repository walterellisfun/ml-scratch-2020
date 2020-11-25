using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

[RequireComponent(typeof(DecisionRequester), typeof(Movement))]
public class SurvivorPersonAgent : Agent
{
	private Movement movement;

	private void Start()
	{
		movement = GetComponent<Movement>();
	}

	public override void CollectObservations(VectorSensor sensor)
    {
		sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
	{
		movement.UpdateMovement(vectorAction);
	}

	public override void Heuristic(float[] actionsOut)
    {
		actionsOut[0] = Input.GetAxis("Horizontal");
		actionsOut[1] = Input.GetAxis("Vertical");
	}
}
