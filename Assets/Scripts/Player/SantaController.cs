using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(MovingComponent))]
[RequireComponent(typeof(CommandHandler))]
public class SantaController : Entity
{

    private StateManagerSanta stateManager;
    public delegate void OnSelectedSanta(SantaController santa);

    // Start is called before the first frame update
    void Start()
    {
        CheckOrAddComponent(out stateManager);
    }


    public override void OnPointerDown(PointerEventData eventData)
    {

    }

    public override void ClickedOn(PlayerController player)
    {
        base.ClickedOn(player);
        player.SantaSelected(this);
    }
}
