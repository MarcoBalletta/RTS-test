using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponentSanta : MovementComponent
{

    //new SantaController controller;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override void OnEnable()
    {
        controller = GetComponent<SantaController>();
        base.OnEnable();
        controller.onMoveTo += ReachDestination;
        (controller as SantaController).onStartMoving += MoveToDestination;
        //(controller as SantaController).onStartMoving
    }

    private void MoveToDestination(Vector3 position, float baseOffset, DestinationObject clickedEntity)
    {
        StartCoroutine(ReachDestinationSet(position, baseOffset, clickedEntity));
    }

    private IEnumerator ReachDestinationSet(Vector3 position, float baseOffset, DestinationObject clickedEntity)
    {
        yield return controller.onMoveTo(position, baseOffset);
        Debug.Log("ReachedDestination");
        (controller as SantaController).onReachedDestination();
    }
}
