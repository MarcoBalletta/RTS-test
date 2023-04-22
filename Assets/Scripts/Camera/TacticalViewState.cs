using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticalViewState : State
{

    new StateManagerCamera stateManager;

    public TacticalViewState(StateManager sm) : base(sm) 
    {
        nameOfState = Constants.TACTICAL_VIEW_STATE;
        stateManager = sm as StateManagerCamera;
    }

    public override void OnEnter()
    {
        GameManager.instance?.onTacticalView();
    }

    public override void OnExit()
    {
    }

    public override void OnUpdate()
    {
    }
}
