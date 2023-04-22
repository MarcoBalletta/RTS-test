using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(StateManager))]
[RequireComponent(typeof(CommandHandler))]
[RequireComponent(typeof(Renderer))]
public abstract class Entity : MonoBehaviour, IPointerDownHandler
{

    protected CapsuleCollider colliderEntity;
    protected Rigidbody rigidbodyEntity;
    protected CommandHandler commandHandler;
    [SerializeField] protected float maxAltitude;
    [SerializeField] protected float minAltitude;
    [SerializeField] protected float deltaMinAltitude;
    [SerializeField] protected float deltaMaxAltitude;
    [SerializeField] protected float speed;
    [SerializeField] protected Renderer entityRenderer;
    public delegate void MoveTo(Vector3 destination);
    public MoveTo onMoveTo;

    private void Start()
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
}
