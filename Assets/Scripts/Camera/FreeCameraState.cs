using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class FreeCameraState : State
{

    private new StateManagerCamera stateManager;

    public FreeCameraState(StateManager sm) : base(sm)
    {
        stateManager = sm as StateManagerCamera;
        nameOfState = Constants.FREE_STATE_STATE;
    }

    public override void OnEnter()
    {
        GameManager.instance?.onFreeState();
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        
    }

}
