using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager : MonoBehaviour
{
    [SerializeField] public State currentState;
    public Dictionary<string, State> listOfStates = new Dictionary<string, State>();

    protected virtual void Awake()
    {
        InitializeStates();
    }

    protected abstract void InitializeStates();

    private void Update()
    {
        currentState?.OnUpdate();
    }

    public void OnChangeState(string keyState)
    {
        if (currentState?.nameOfState == keyState) return;
        currentState?.OnExit();
        currentState = listOfStates[keyState];
        currentState?.OnEnter();
    }
}
