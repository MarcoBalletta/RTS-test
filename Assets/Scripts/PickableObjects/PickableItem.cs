using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : DestinationObject, ILeftClickable
{

    [SerializeField] private Building destinationBuilding;

    public Building DestinationBuilding { get => destinationBuilding; set => destinationBuilding = value; }

    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
        destinationBuilding.TemporaryHighlight();
    }

    public override void EntityArrivedAtDestinationObject(SantaController santa)
    {
        santa.PickupItem(this);
    }
}
