using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyController : Entity
{

    private Entity enemyDetected;
    private BefanaStateManager stateManager;
    [SerializeField] protected float deltaMinAltitude;
    [SerializeField] protected float deltaMaxAltitude;
    [SerializeField] private float distancePatrol;
    [SerializeField] private float descendingVelocity;

    public delegate void OnPatrol();
    public OnPatrol onPatrol;

    public delegate void OnChaseEnemy();
    public OnChaseEnemy onChaseEnemy;
    [SerializeField] private float aggroLostDistance;

    private void OnEnable()
    {
        CheckOrAddComponent(out stateManager);
        onPatrol += Patrol;
        onChaseEnemy += Chase;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {

    }

    #region Patrol
    public void Patrol()
    {
        Debug.Log("Patrol called");
        ClearValues();
        StartCoroutine(Patrolling());
    }

    void ClearValues()
    {
        enemyDetected = null;
    }

    private IEnumerator Patrolling()
    {
        while (stateManager.currentState.nameOfState == Constants.PATROL_STATE)
        {
            //SetRandomDestination();
            yield return onMoveTo(SetRandomDestination(), CalculateRandomBaseOffset(movingComponent.Agent.baseOffset));
            //float timingLerp;
            //float randomBaseOffset = CalculateRandomBaseOffset(movingComponent.Agent.baseOffset);
            //timingLerp = CalculateTimingLerpAdjustingHeight(randomBaseOffset);

            //yield return StartCoroutine(movingComponent.AdjustOffsetCoroutine(randomBaseOffset, timingLerp));
        }
    }

    private float CalculateRandomBaseOffset(float baseOffset)
    {
        
        float minBaseOffset = (baseOffset - deltaMinAltitude) >= minAltitude ? baseOffset - deltaMinAltitude : minAltitude;
        float maxBaseOffset = (baseOffset + deltaMaxAltitude) <= maxAltitude ? baseOffset + deltaMaxAltitude : maxAltitude;
        float randomBaseOffset = Random.Range(minBaseOffset, maxBaseOffset);
        //timingLerp = movingComponent.Agent.remainingDistance / speed * Time.deltaTime;
        return randomBaseOffset;
    }

    private Vector3 SetRandomDestination()
    {
        Vector2 offset;
        //Vector3 newPosition;

        offset = Random.insideUnitCircle * distancePatrol;
        float altitude = Random.Range(1f, maxAltitude);
        return new Vector3(offset.x, 0, offset.y) + new Vector3(transform.position.x, 0, transform.position.z);

        //onMoveTo(newPosition, CalculateRandomBaseOffset(movingComponent.Agent.baseOffset));
    }
    #endregion

    #region Chase

    public void Chase()
    {
        StartCoroutine(ChaseCoroutine());
    }

    private IEnumerator ChaseCoroutine()
    {
        while (enemyDetected != null && movingComponent.Agent.remainingDistance < aggroLostDistance && stateManager.currentState.nameOfState == Constants.CHASE_STATE)
        {
            onMoveTo(enemyDetected.transform.position, enemyDetected.transform.position.y);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        Debug.Log("patrol");
        SendMessage("OnChangeState", Constants.PATROL_STATE);
    }

    public void FoundPlayer(Entity player)
    {
        enemyDetected = player;
        stateManager.OnChangeState(Constants.CHASE_STATE);
    }

    #endregion
}
