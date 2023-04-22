using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    public virtual void Drop()
    {
    }

    public virtual void Pickup()
    {
    }
}
