using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Entity))]
public class MovementComponent : MonoBehaviour
{

    protected Entity controller;
    protected NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent;}

    protected virtual void Awake()
    {
        controller = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void OnEnable()
    {
        controller.onMoveTo += ReachDestination;
    }

    public IEnumerator AdjustOffsetCoroutine(float offset, float timing)
    {
        do
        {
            AdjustBaseOffset(offset, timing);
            //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, randomBaseOffset, transform.position.z), timingLerp);
            yield return new WaitForSeconds(Time.deltaTime);
        } while (agent.remainingDistance > agent.stoppingDistance);
    }

    protected void AdjustBaseOffset(float offset, float timing)
    {
        agent.baseOffset = Mathf.Lerp(agent.baseOffset, offset, timing);
    }

    protected IEnumerator ReachDestination(Vector3 destination, float baseOffset)
    {
        agent.destination = destination;
        yield return StartCoroutine(AdjustOffsetCoroutine(baseOffset, controller.CalculateTimingLerpAdjustingHeight(baseOffset)));
    }
}
