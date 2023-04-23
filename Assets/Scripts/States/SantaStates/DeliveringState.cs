using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveringState : State
{
    public DeliveringState(StateManager sm) : base(sm)
    {
        nameOfState = Constants.DELIVERING_STATE;
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
