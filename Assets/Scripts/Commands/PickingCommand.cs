using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingCommand : Command
{
    public override void Execute(SantaController player, Vector3 destination, DestinationObject destinationObject, float baseOffset)
    {
        Debug.Log("Picking");
        destinationObject.EntityArrivedAtDestinationObject(player);
    }

}
