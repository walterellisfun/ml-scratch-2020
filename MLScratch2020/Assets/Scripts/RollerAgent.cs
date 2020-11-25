using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;


public class SurvivorAgent : Agent
{
    [SerializeField] private Transform Target;
    [SerializeField] private float forceMultiplier;

    private Rigidbody agentRigidBody;
    private Rigidbody bigBallRigidbody;
    [SerializeField] GameObject bigBall;

    private float storedDistanceToTarget;

    private bool fellOff;

    void Awake()
    {
        agentRigidBody = GetComponent<Rigidbody>();
        bigBallRigidbody = bigBall.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        if (fellOff)
        {
            fellOff = false;
            // If the Agent fell, zero its momentum
            agentRigidBody.angularVelocity = Vector3.zero;
            agentRigidBody.velocity = Vector3.zero;
            transform.localPosition = new Vector3(0, 5.501f, 0);

            bigBallRigidbody.angularVelocity = Vector3.zero;
            bigBallRigidbody.velocity = Vector3.zero;
            bigBall.transform.localPosition = new Vector3(0, 2.5f, 0);
        }

		// Move the target to a new spot
		Target.localPosition = new Vector3(Random.Range(5f, 15f), 0.5f, Random.Range(5f, 15f));

        storedDistanceToTarget = GetDistanceToTarget();
	}

    public override void CollectObservations(VectorSensor sensor)
    {
        // positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(bigBall.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(agentRigidBody.velocity.x);
        sensor.AddObservation(agentRigidBody.velocity.z);

        // BigBall velocity
        sensor.AddObservation(bigBall.GetComponent<Rigidbody>().velocity.x);
        sensor.AddObservation(bigBall.GetComponent<Rigidbody>().velocity.z);
    }

    public override void OnActionReceived(float[] vectorAction)
	{
		// Actions, size = 2
		Vector3 controlSignal = Vector3.zero;
		controlSignal.x = vectorAction[0];
		controlSignal.z = vectorAction[1];
		agentRigidBody.AddTorque(controlSignal * forceMultiplier);

		// Rewards
        
        // got closer
        if (GetDistanceToTarget() < storedDistanceToTarget)
        {
            AddReward(0.05f);
            storedDistanceToTarget = GetDistanceToTarget();
        }
		else
		{
            AddReward(-0.02f);
		}

        // Reached target
        if (GetDistanceToTarget() < 2.55f)
		{
			SetReward(1.0f);
			EndEpisode();
		}

		// Fell off platform
		if (this.transform.localPosition.y < 1f)
		{
			SetReward(-1.0f);
            fellOff = true;
			EndEpisode();
		}
		// still on ball
		else
		{
            AddReward(0.01f);
		}
	}

	private float GetDistanceToTarget()
	{
		return Vector3.Distance(bigBall.transform.localPosition, Target.localPosition);
	}

	public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Horizontal");
        actionsOut[1] = Input.GetAxis("Vertical");
    }

}
