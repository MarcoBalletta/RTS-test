using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GiftIcon : MonoBehaviour, IPointerClickHandler
{

    private Building destinationBuilding;

    public void Setup(Building building)
    {
        destinationBuilding = building;
    }

    public void HighlightBuilding()
    {
        destinationBuilding.TemporaryHighlight();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HighlightBuilding();
    }
}
