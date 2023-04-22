using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class BefanaStateManager : StateManager
{

    private EnemyController enemyController;

    public EnemyController EnemyController { get => enemyController;}

    protected override void InitializeStates()
    {
        listOfStates.Add(Constants.PATROL_STATE, new PatrolState(this));
        listOfStates.Add(Constants.CHASE_STATE, new ChaseState(this));
    }

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        enemyController = GetComponent<EnemyController>();
        OnChangeState(Constants.PATROL_STATE);
    }
}
