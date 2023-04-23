using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    public List<PickableItem> items = new List<PickableItem>();
    public uint maxItems;

}
