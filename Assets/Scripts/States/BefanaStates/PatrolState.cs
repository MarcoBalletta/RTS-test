using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{

    new BefanaStateManager stateManager;

    public PatrolState(StateManager sm) : base(sm)
    {
        stateManager = sm as BefanaStateManager;
        nameOfState = Constants.PATROL_STATE;
    }

    public override void OnEnter()
    {
        stateManager.EnemyController.onPatrol();
    }

    public override void OnExit()
    {

    }
    public override void OnUpdate()
    {
    }
}
