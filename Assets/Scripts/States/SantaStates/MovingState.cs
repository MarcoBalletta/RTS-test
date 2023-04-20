using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    public MovingState(StateManager sm) : base(sm)
    {
        nameOfState = Constants.MOVING_STATE;
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
