using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Entity))]
public class MovingComponent : MonoBehaviour
{

    private Entity controller;
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; set => agent = value; }

    private void Awake()
    {
        controller = GetComponent<Entity>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        controller.onMoveTo += ReachDestination;
    }

    private void ReachDestination(Vector3 destination)
    {
        agent.destination = destination;
    }
}
