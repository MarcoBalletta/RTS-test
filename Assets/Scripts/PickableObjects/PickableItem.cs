using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : DestinationObject, IPickable, ILeftClickable
{

    [SerializeField] private Building destinationBuilding;

    public Building DestinationBuilding { get => destinationBuilding;}

    public virtual void Drop()
    {
    }

    public virtual void Pickup()
    {
    }

    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
    }

    public override void EntityArrivedAtDestinationObject(SantaController santa)
    {
        santa.PickupItem(this);
    }
}
