using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    [SerializeField] public string nameOfState;
    protected StateManager stateManager;

    public State(StateManager sm)
    {
        stateManager = sm;
    }

    public abstract void OnEnter();

    public abstract void OnUpdate();

    public abstract void OnExit();
}
