using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public List<PickableItem> items = new List<PickableItem>();
    public uint maxItems;

    public bool CanPickup()
    {
        return items.Count < maxItems;
    }

    public void PickupItem(PickableItem item)
    {
        items.Add(item);
    }

    public void DropItem(PickableItem item)
    {
        items.Remove(item);
    }

}
