using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefanaStateManager : StateManager
{
    protected override void InitializeStates()
    {
        listOfStates.Add(Constants.PATROL_STATE, new PatrolState(this));
        listOfStates.Add(Constants.CHASE_STATE, new ChaseState(this));
        OnChangeState(Constants.PATROL_STATE);
    }
}
