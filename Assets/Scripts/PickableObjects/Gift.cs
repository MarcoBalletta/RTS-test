using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : PickableItem, ILeftClickable
{

    public override void EntityArrivedAtDestinationObject(SantaController santa)
    {
        base.EntityArrivedAtDestinationObject(santa);
        //santa.
    }

    public override void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
        base.LeftClicked(player, clickPosition);
    }
}
