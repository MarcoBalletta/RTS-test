using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : DestinationObject, IPickable, ILeftClickable
{
    public virtual void Drop()
    {
    }

    public virtual void Pickup()
    {
    }

    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
    }
}
