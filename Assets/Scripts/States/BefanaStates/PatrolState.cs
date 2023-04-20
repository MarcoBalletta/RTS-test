using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public PatrolState(StateManager sm) : base(sm)
    {
        nameOfState = Constants.IDLE_STATE;
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {

    }
    public override void OnUpdate()
    {
    }
}
