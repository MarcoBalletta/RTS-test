using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    public State currentState;
    public Dictionary<string, State> listOfStates;

    private void Awake()
    {
        InitializeStates();
    }

    protected abstract void InitializeStates();

    private void Update()
    {
        currentState.OnUpdate();
    }

    public void OnChangeState(string keyState)
    {
        if (currentState.nameOfState == keyState) return;
        currentState?.OnExit();
        currentState = listOfStates[keyState];
        currentState?.OnEnter();
    }
}
