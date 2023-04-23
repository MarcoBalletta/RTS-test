using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : DestinationObject, ILeftClickable
{
    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
    }
}
