using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(MovementComponent))]
public abstract class Entity : MonoBehaviour, IPointerDownHandler, ILeftClickable
{

    protected CapsuleCollider colliderEntity;
    protected Rigidbody rigidbodyEntity;
    protected MovementComponent movingComponent;

    [SerializeField] protected float maxAltitude;
    [SerializeField] protected float minAltitude;
    [SerializeField] protected float speed;
    [SerializeField] protected Renderer entityRenderer;
    
    public delegate IEnumerator MoveTo(Vector3 destination, float baseOffset);
    public MoveTo onMoveTo;

    protected virtual void Awake()
    {
        CheckOrAddComponent(out movingComponent);
        CheckOrAddComponent(out colliderEntity);
        CheckOrAddComponent(out rigidbodyEntity);
        movingComponent.Agent.baseOffset = transform.position.y;
    }

    protected virtual void Start()
    {
        GameManager.instance.onTacticalView += Highlight;
        GameManager.instance.onFreeState += BackToNormal;
    }

    public abstract void OnPointerDown(PointerEventData eventData);
    protected void CheckOrAddComponent<T>(out T destinationVariable) where T: Component
    {
        if(!TryGetComponent<T>(out destinationVariable))
        {
            destinationVariable = gameObject.AddComponent<T>();
        }
    }

    protected virtual void Highlight()
    {
        entityRenderer.material.SetFloat(Constants.SHADER_BOOLEAN_HIGHLIGHT_NAME, 1f);
    }

    protected virtual void BackToNormal()
    {
        entityRenderer.material.SetFloat(Constants.SHADER_BOOLEAN_HIGHLIGHT_NAME, 0.0f);
    }

    public float CalculateTimingLerpAdjustingHeight(float baseOffset)
    {
        return Vector3.Distance(transform.position, new Vector3(movingComponent.Agent.destination.x, baseOffset, movingComponent.Agent.destination.z)) / speed * Time.deltaTime;
    }

    public virtual void LeftClicked(PlayerController player, Vector3 position)
    {
        
    }
}
