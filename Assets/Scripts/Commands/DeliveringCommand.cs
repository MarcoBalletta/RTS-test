using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveringCommand : Command
{
    public override void Execute(SantaController player, Vector3 destination, DestinationObject destinationObject, float baseOffset)
    {
        Debug.Log("Delivering");
        destinationObject.EntityArrivedAtDestinationObject(player);
        GameManager.instance.onGiftDelivered();
    }
}
