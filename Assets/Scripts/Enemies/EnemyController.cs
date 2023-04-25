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

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckOrAddComponent(out stateManager);
        onPatrol += Patrol;
        onChaseEnemy += Chase;
        GameManager.instance.onBefanaCaughtSanta += EnemyCaughtSanta;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {

    }

    #region Patrol
    public void Patrol()
    {
        Debug.Log("Patrol called");
        ClearValues();
        Debug.Log("Debug: " + movingComponent + " ---- " + movingComponent.Agent + "--- " + stateManager); ;
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
            yield return onMoveTo(SetRandomDestination(), CalculateRandomBaseOffset(movingComponent.Agent.baseOffset));
        }
    }

    private float CalculateRandomBaseOffset(float baseOffset)
    {
        
        float minBaseOffset = (baseOffset - deltaMinAltitude) >= minAltitude ? baseOffset - deltaMinAltitude : minAltitude;
        float maxBaseOffset = (baseOffset + deltaMaxAltitude) <= maxAltitude ? baseOffset + deltaMaxAltitude : maxAltitude;
        float randomBaseOffset = Random.Range(minBaseOffset, maxBaseOffset);
        return randomBaseOffset;
    }

    private Vector3 SetRandomDestination()
    {
        Vector2 offset;

        offset = Random.insideUnitCircle * distancePatrol;
        float altitude = Random.Range(1f, maxAltitude);
        return new Vector3(offset.x, 0, offset.y) + new Vector3(transform.position.x, 0, transform.position.z);

    }
    #endregion

    #region Chase

    public void Chase()
    {
        if(GameManager.instance?.onBefanaFoundSanta != null) GameManager.instance?.onBefanaFoundSanta(enemyDetected, this);
        StartCoroutine(ChaseCoroutine());
    }

    private IEnumerator ChaseCoroutine()
    {
        while (enemyDetected != null && movingComponent.Agent.remainingDistance < aggroLostDistance && stateManager.currentState.nameOfState == Constants.CHASE_STATE)
        {
            onMoveTo(enemyDetected.transform.position, enemyDetected.transform.position.y);
            if(movingComponent.Agent.remainingDistance < movingComponent.Agent.stoppingDistance)
            {
                GameManager.instance.onBefanaCaughtSanta(enemyDetected, this);
            }
            yield return new WaitForSeconds(Time.deltaTime);
        }
        
        SendMessage("OnChangeState", Constants.PATROL_STATE);
    }

    public void FoundPlayer(Entity player)
    {
        enemyDetected = player;
        stateManager.OnChangeState(Constants.CHASE_STATE);
    }

    #endregion

    private void EnemyCaughtSanta( Entity santa, EnemyController enemy)
    {
        Destroy(enemy.gameObject, 0.1f);
        Destroy(santa.gameObject, 0.1f);
    }
}
