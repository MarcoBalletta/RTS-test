using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : DestinationObject
{
    public override void RightClicked(PlayerController player, Vector3 position)
    {
        GameManager.instance.onSelected2DPosition(position);
    }
}
