using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class AgentScript : Agent
{
    public int Score = 0;
    public float breakRadio = .5f;
    public int Team = 0;
    Rigidbody AgentRB;
    public float maxSpawnRange = 45f;
    public float turnSpeed = 300;
    // Speed of agent movement.
    public float moveSpeed = 2f;
    public double actions = 0;
    public Vector3 spawn;
    public quaternion orientation;
    public GameObject Ball;

    public override void Initialize()
    {
        AgentRB = GetComponent<Rigidbody>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        var localVelocity = transform.InverseTransformDirection(AgentRB.velocity);
        sensor.AddObservation(localVelocity.x);
        sensor.AddObservation(localVelocity.z);
        sensor.AddObservation(AgentRB.transform.localPosition.x);
        sensor.AddObservation(AgentRB.transform.localPosition.z);
    }

    public void MoveAgent(ActionBuffers actionBuffers)
    {

        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;

        var continuousActions = actionBuffers.ContinuousActions;
        //var discreteActions = actionBuffers.DiscreteActions;

        var forward = Mathf.Clamp(continuousActions[0], -1f, 1f);
        var rotate = Mathf.Clamp(continuousActions[1], -1f, 1f);

        dirToGo = transform.forward * forward;
        rotateDir = transform.up * rotate;

        AgentRB.AddForce(dirToGo * moveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * turnSpeed);

        if (AgentRB.velocity.sqrMagnitude > 25f) // slow it down
        {
            AgentRB.velocity *= 0.95f;
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        float distance = Vector3.Distance(this.transform.position, Ball.transform.position);
        if(distance > 0)
            AddReward(distance / 1000000);
        MoveAgent(actionBuffers);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[1] = Input.GetAxisRaw("Horizontal");
        continuousActionsOut[0] = Input.GetAxisRaw("Vertical");
    }

    private void Start() {
        spawn = this.transform.position;
        orientation = this.transform.rotation;
        Ball = GameObject.FindGameObjectWithTag("Goal"); // goal == ball ? wierd mixup
    }

    public override void OnEpisodeBegin()
    {

        AgentRB.velocity = Vector3.zero;
        transform.position = new Vector3(UnityEngine.Random.Range(spawn.x , spawn.x + 2), 1f , UnityEngine.Random.Range(spawn.z, spawn.z + 2));
        transform.rotation = Quaternion.Euler(new Vector3(0f, UnityEngine.Random.Range(0, 360)));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            AddReward(.2f);
            Vector3 direction = (collision.transform.position - transform.position).normalized;
		    collision.rigidbody.AddForce(direction * 8, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("Red Goal") || collision.gameObject.CompareTag("Blue Goal"))
        {
            AddReward(-0.0025f);
        }
        if(collision.gameObject.CompareTag("Red team") && gameObject.tag == "Red team"){
            AddReward(-0.0025f);
        }
        if(collision.gameObject.CompareTag("Blue team") && gameObject.tag == "Blue team"){
            AddReward(-0.0025f);
        }
    }

}
