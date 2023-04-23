using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : DestinationObject, ILeftClickable
{

    [SerializeField]private List<PickableItem> itemsToDeliver = new List<PickableItem>();
    
    public override void EntityArrivedAtDestinationObject(SantaController santa)
    {
        santa.CheckGiftForBuilding(this);
    }

    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
    }

    public bool CheckIfItemIsInListItemsToDeliver(PickableItem item)
    {
        return itemsToDeliver.Contains(item);
    }

    public void RemoveItemFromList(PickableItem item)
    {
        itemsToDeliver.Remove(item);
        Destroy(item.gameObject, 0.1f);
    }

    
}
