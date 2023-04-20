using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerSanta : StateManager
{
    protected override void InitializeStates()
    {
        listOfStates.Add(Constants.IDLE_STATE, new IdleState(this));
        listOfStates.Add(Constants.MOVING_STATE, new MovingState(this));
        listOfStates.Add(Constants.PICKING_STATE, new PickingState(this));
    }
}
