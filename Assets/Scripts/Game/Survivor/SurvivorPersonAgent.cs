using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class SurvivorPersonAgent : Agent
{
	public override void CollectObservations(VectorSensor sensor)
    {
		sensor.AddObservation(transform.localPosition);
    }

    public override void OnActionReceived(float[] vectorAction)
	{
		UpdateMovement(vectorAction);
	}

	public override void Heuristic(float[] actionsOut)
    {
		actionsOut[0] = Input.GetAxis("Horizontal");
		actionsOut[1] = Input.GetAxis("Vertical");
	}
}
