using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerSanta : StateManager
{

    private SantaController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponent<SantaController>();
    }

    private void OnEnable()
    {
        controller.onStartMoving += MoveToDestinationStateChange;
        OnChangeState(Constants.IDLE_STATE);
    }

    protected override void InitializeStates()
    {
        listOfStates.Add(Constants.IDLE_STATE, new IdleState(this));
        listOfStates.Add(Constants.MOVING_STATE, new MovingState(this));
        listOfStates.Add(Constants.PICKING_STATE, new PickingState(this));
    }

    private void MoveToDestinationStateChange(Vector3 position, float baseOffset, DestinationObject clickedEntity)
    {
        OnChangeState(Constants.MOVING_STATE);
        //StartCoroutine(ReachDestination(position, baseOffset, clickedEntity));
    }


}
