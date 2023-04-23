using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(InventoryComponent))]
[RequireComponent(typeof(CommandHandler))]
public class SantaController : Entity
{

    private StateManagerSanta stateManager;
    private CommandHandler commandHandler;
    private InventoryComponent inventoryComponent;

    public delegate void OnAddMovementCommand(Vector3 position, float baseOffset, DestinationObject clickedEntity);
    public OnAddMovementCommand onAddMovementCommand;

    public delegate void OnStartMoving(Vector3 position, float baseOffset, DestinationObject clickedEntity);
    public OnStartMoving onStartMoving;

    public delegate void ReachedDestination();
    public ReachedDestination onReachedDestination;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        CheckOrAddComponent(out stateManager);
        CheckOrAddComponent(out commandHandler);
        CheckOrAddComponent(out inventoryComponent);
    }



    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("santa Selected movement");
            GameManager.instance?.onSantaSelectedMovement(this);
        }
    }

    public override void LeftClicked(PlayerController player, Vector3 position)
    {
        base.LeftClicked(player, position);
        Debug.Log("Clicked on Santa : " + this.name);
        if(GameManager.instance?.onSantaSelectedInfos != null) GameManager.instance?.onSantaSelectedInfos(this);
    }

    public void CheckGiftForBuilding(Building building)
    {
        foreach(var item in inventoryComponent.items)
        {
            if(item is PickableItem && building.CheckIfItemIsInListItemsToDeliver(item))
            {
                inventoryComponent.DropItem(item);
                building.RemoveItemFromList(item);
            }
        }
    }

    public void PickupItem(PickableItem item)
    {
        if (inventoryComponent.CanPickup())
        {
            inventoryComponent.PickupItem(item);
            item.gameObject.SetActive(false);
        }
    }
}
