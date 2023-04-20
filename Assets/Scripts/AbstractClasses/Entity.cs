using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StateManager))]

public abstract class Entity : MonoBehaviour, IPointerDownHandler
{

    public virtual void OnPointerDown(PointerEventData eventData)
    {

    }
}
