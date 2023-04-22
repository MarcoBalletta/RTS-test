using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerCamera : StateManager
{

    protected override void InitializeStates()
    {
        listOfStates.Add(Constants.FREE_STATE_STATE, new FreeCameraState(this));
        listOfStates.Add(Constants.TACTICAL_VIEW_STATE, new TacticalViewState(this));
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        OnChangeState(Constants.FREE_STATE_STATE);
    }
}
