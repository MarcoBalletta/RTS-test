using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(MovingComponent))]
public class SantaController : Entity
{

    private StateManagerSanta stateManager;

    // Start is called before the first frame update
    void Start()
    {
        CheckOrAddComponent(out stateManager);
    }


    public override void OnPointerDown(PointerEventData eventData)
    {

    }
}
