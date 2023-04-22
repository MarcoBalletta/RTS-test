using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{

    new BefanaStateManager stateManager;

    public ChaseState(StateManager sm) : base(sm)
    {
        stateManager = sm as BefanaStateManager;
        nameOfState = Constants.CHASE_STATE;
    }

    public override void OnEnter()
    {
        stateManager.EnemyController.onChaseEnemy();
    }

    public override void OnExit()
    {

    }
    public override void OnUpdate()
    {
    }
}
