using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : DestinationObject, ILeftClickable
{

    [SerializeField]private List<PickableItem> itemsToDeliver = new List<PickableItem>();
    

    public List<PickableItem> ItemsToDeliver { get => itemsToDeliver; }

    public override void EntityArrivedAtDestinationObject(SantaController santa)
    {
        santa.CheckGiftForBuilding(this);
    }

    public virtual void LeftClicked(PlayerController player, Vector3 clickPosition)
    {
        if(itemsToDeliver.Count > 0)
        {
            foreach(var item in itemsToDeliver)
            {
                item.TemporaryHighlight();
            }
        }
    }

    public override void RightClicked(PlayerController player, Vector3 position)
    {
        if (itemsToDeliver.Count > 0)   base.RightClicked(player, position);
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
