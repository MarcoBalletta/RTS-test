using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : DestinationObject, ILeftClickable
{

    [SerializeField]private List<PickableItem> itemsToDeliver = new List<PickableItem>();
    [SerializeField] private int layerClickable;
    [SerializeField] private int layerNotClickable;

    public List<PickableItem> ItemsToDeliver { get => itemsToDeliver; }

    protected override void Start()
    {
        if (CheckIfHighlightable()) base.Start();
        else gameObject.layer = layerNotClickable;
    }

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
        base.RightClicked(player, position);
        //if (CheckIfHighlightable())   base.RightClicked(player, position);
    }

    public bool CheckIfItemIsInListItemsToDeliver(PickableItem item)
    {
        return itemsToDeliver.Contains(item);
    }

    public void RemoveItemFromList(PickableItem item)
    {
        itemsToDeliver.Remove(item);
        if (!CheckIfHighlightable()) RemoveFromTacticalView();
        Destroy(item.gameObject, 0.1f);
    }

    protected virtual void RemoveFromTacticalView()
    {
        GameManager.instance.onTacticalView -= Highlight;
        gameObject.layer = layerNotClickable;
    }

    private bool CheckIfHighlightable()
    {
        return itemsToDeliver.Count > 0;
    }
}
